using JQCore.Extensions;
using JQCore.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace JQCore.Mvc.Extensions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SelectItemExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/9 18:54:48
    /// </summary>
    public static class SelectItemUtil
    {
        /// <summary>
        /// 由列表转为下拉框选项列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="textFunc">获取文本的方法</param>
        /// <param name="valueFunc">获取值的方法</param>
        /// <param name="currentValue">当前被选中的选中值</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToListItems<T>(this IEnumerable<T> list, Func<T, string> textFunc, Func<T, string> valueFunc, string currentValue = null)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in list)
            {
                var value = valueFunc(item);
                bool isSelect = false;
                if (currentValue != null && currentValue.IsNotNullAndNotEmptyWhiteSpace())
                {
                    if (currentValue.Equals(value))
                    {
                        isSelect = true;
                    }
                }
                selectList.Add(new SelectListItem { Text = textFunc(item), Value = valueFunc(item), Selected = isSelect });
            }
            return selectList;
        }

        /// <summary>
        /// 将枚举类型转为下拉列表
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToListItems<TEnum>(string currentValue = null)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            var dic = typeof(TEnum).GetDesc();
            foreach (var item in dic.Keys)
            {
                bool isSelect = false;
                if (currentValue != null && currentValue.IsNotNullAndNotEmptyWhiteSpace())
                {
                    if (currentValue.Equals(item))
                    {
                        isSelect = true;
                    }
                }
                selectList.Add(new SelectListItem { Text = dic[item], Value = item, Selected = isSelect });
            }
            return selectList;
        }
    }
}