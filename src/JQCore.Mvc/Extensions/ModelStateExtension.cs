using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Text;

namespace JQCore.Mvc.Extensions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ModelStateExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：Model校验扩展类
    /// 创建标识：yjq 2017/9/12 11:13:31
    /// </summary>
    public static class ModelStateExtension
    {
        /// <summary>
        /// 获取第一个错误信息(没有返回空)
        /// </summary>
        /// <param name="modelState">模型绑定状态</param>
        /// <returns>第一个错误信息(没有返回空)</returns>
        public static string GetFirstErrorMsg(this ModelStateDictionary modelState)
        {
            if (modelState == null) return string.Empty;
            if (!modelState.IsValid)
            {
                string error = string.Empty;
                foreach (var key in modelState.Keys)
                {
                    var state = modelState[key];
                    if (state.Errors.Any())
                    {
                        error = state.Errors.First().ErrorMessage;
                        break;
                    }
                }
                return error;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取全部错误信息(没有返回空)
        /// </summary>
        /// <param name="modelState">模型绑定状态</param>
        /// <returns>全部错误信息(没有返回空)</returns>
        public static string GetAllErrorMsg(this ModelStateDictionary modelState)
        {
            if (modelState == null) return string.Empty;
            if (!modelState.IsValid)
            {
                StringBuilder errorBuilder = new StringBuilder();
                //获取每一个key对应的ModelStateDictionary
                foreach (var value in modelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errorBuilder.Append(error.ErrorMessage).Append(",");
                    }
                }
                errorBuilder.Remove(errorBuilder.Length - 1, 1);
                return errorBuilder.ToString();
            }
            return string.Empty;
        }
    }
}