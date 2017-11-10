using JQCore.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using JQCore.Web;
namespace JQCore.Mvc.TagHelpers
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：PagerTagHelper.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：分页
    /// 创建标识：yjq 2017/9/27 21:24:29
    /// </summary>
    [HtmlTargetElement("pager")]
    public class PagerTagHelper : TagHelper
    {
        private IDictionary<string, string> _routeValues;
        private readonly IUrlHelperFactory _urlHelperFactory;

        public PagerTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        /// <summary>
        /// 分页结果类
        /// </summary>
        [HtmlAttributeName("jqcore-page")]
        public IPageResult PageResult { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        [HtmlAttributeName("jqcore-page-route")]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                if (_routeValues == null)
                {
                    _routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                return _routeValues;
            }
            set
            {
                _routeValues = value;
            }
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (PageResult == null)
            {
                output.Content.SetHtmlContent(string.Empty);
                return;
            }
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<div class='col-sm-6'>")
                       .AppendFormat("<div class='dataTables_info'>显示 {0} 到 {1} 总共 {2} 条记录</div>", PageResult.CurrentMinPosition.ToString(), PageResult.CurrentMaxPosition.ToString(), PageResult.TotalCount.ToString())
                       .Append("</div>");
            htmlBuilder.Append("<div class='col-sm-6'><div class='dataTables_paginate paging_simple_numbers'>");
            if (PageResult.PageCount > 0)
            {
                htmlBuilder.Append("<ul class='pagination'>");

                string urlFormat = GetUrlFormat().Replace("PageIndex=%7B0%7D", "PageIndex={0}");

                AppendPageButton(htmlBuilder, 1, "首页", 1 == PageResult.PageIndex, urlFormat);
                AppendPageButton(htmlBuilder, PageResult.PrevPageIndex, "上一页", PageResult.PageIndex == PageResult.PrevPageIndex, urlFormat);

                int startPageIndex = GetStartPageIndex();
                int endPageIndex = GetEndPageIndex();
                if (startPageIndex > 1)
                {
                    AppendEnablePageButton(htmlBuilder, "...");
                }
                for (int i = startPageIndex; i <= endPageIndex; i++)
                {
                    AppendPageButton(htmlBuilder, i, i.ToString(), i == PageResult.PageIndex, urlFormat, selfClass: i == PageResult.PageIndex ? "active" : "");
                }
                if (endPageIndex < PageResult.PageCount)
                {
                    AppendEnablePageButton(htmlBuilder, "...");
                }

                AppendPageButton(htmlBuilder, PageResult.NextPageIndex, "下一页", PageResult.PageIndex >= PageResult.NextPageIndex, urlFormat);
                AppendPageButton(htmlBuilder, PageResult.PageCount, "尾页", PageResult.PageIndex == PageResult.PageCount, urlFormat);

                htmlBuilder.Append("</ul>");
            }
            htmlBuilder.Append("</div></div>");

            output.Content.SetHtmlContent(htmlBuilder.ToString());
        }

        /// <summary>
        /// 拼接不可点击分页按钮
        /// </summary>
        /// <param name="htmlBuilder"></param>
        /// <param name="desc">按钮内容</param>
        /// <param name="selfClass">自定义样式</param>
        /// <returns></returns>
        private StringBuilder AppendEnablePageButton(StringBuilder htmlBuilder, string desc, string selfClass = null)
        {
            htmlBuilder.AppendFormat(" <li class='paginate_button disabled {0}' ><a href='javascript:void(0);'>{1}</a></li>", selfClass, desc);
            return htmlBuilder;
        }

        /// <summary>
        /// 添加分页按钮
        /// </summary>
        /// <param name="htmlBuilder"></param>
        /// <param name="pageIndex">要拼接的页面ID</param>
        /// <param name="desc">按钮内容</param>
        /// <param name="isDisabled">是否可点击</param>
        /// <param name="routeUrlFormat">路由格式</param>
        /// <param name="selfClass">自定义样式</param>
        /// <returns></returns>
        private StringBuilder AppendPageButton(StringBuilder htmlBuilder, int pageIndex, string desc, bool isDisabled, string routeUrlFormat, string selfClass = null)
        {
            if (isDisabled)
            {
                return AppendEnablePageButton(htmlBuilder, desc, selfClass: selfClass);
            }
            else
            {
                string url = string.Format(routeUrlFormat, pageIndex.ToString());
                htmlBuilder.AppendFormat(" <li class='paginate_button {0}' ><a href='{1}'>{2}</a></li>", selfClass, url, desc);
                return htmlBuilder;
            }
        }

        /// <summary>
        /// 获取分页的起始页码
        /// </summary>
        /// <returns>起始页码</returns>
        private int GetStartPageIndex()
        {
            if (PageResult == null)
            {
                return 1;
            }
            int startPageIndex = PageResult.PageIndex - 4;
            if (startPageIndex <= 0)
            {
                return startPageIndex = 1;
            }
            if (startPageIndex > PageResult.PageCount)
            {
                return PageResult.PageCount;
            }
            return startPageIndex;
        }

        /// <summary>
        /// 获取分页终止页码
        /// </summary>
        /// <returns>终止页码</returns>
        private int GetEndPageIndex()
        {
            if (PageResult == null)
            {
                return 1;
            }
            int endPageIndex = PageResult.PageIndex + 5;
            if (endPageIndex > PageResult.PageCount)
            {
                return PageResult.PageCount;
            }
            if (endPageIndex <= 0)
            {
                return 1;
            }
            return endPageIndex;
        }

        /// <summary>
        /// 获取地址格式
        /// </summary>
        /// <returns></returns>
        private string GetUrlFormat()
        {
            RouteValueDictionary routeValueDictionary;
            if (ViewContext.RouteData.Values != null)
            {
                routeValueDictionary = new RouteValueDictionary(ViewContext.RouteData.Values);
            }
            else
            {
                routeValueDictionary = new RouteValueDictionary();
            }
            var queryItems = ViewContext.HttpContext.Request.Query;
            if (queryItems != null)
            {
                foreach (var item in queryItems.Keys)
                {
                    routeValueDictionary[item] = queryItems[item];
                }
            }
            if (ViewContext.HttpContext.Request.IsPost())
            {
                var formItems = ViewContext.HttpContext.Request.Form;
                if (formItems != null)
                {
                    foreach (var item in formItems.Keys)
                    {
                        routeValueDictionary[item] = formItems[item];
                    }
                }
            }
            if (RouteValues != null)
            {
                foreach (var item in RouteValues.Keys)
                {
                    routeValueDictionary[item] = RouteValues[item];
                }
            }
            routeValueDictionary["PageIndex"] = "{0}";
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            return urlHelper.RouteUrl(routeValueDictionary);
        }
    }
}