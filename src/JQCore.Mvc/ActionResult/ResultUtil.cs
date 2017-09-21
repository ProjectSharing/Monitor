using JQCore.Result;
using Microsoft.AspNetCore.Mvc;

namespace JQCore.Mvc.ActionResult
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ResultUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：JsonResult扩展类
    /// 创建标识：yjq 2017/9/11 21:16:44
    /// </summary>
    public static class ResultUtil
    {
        /// <summary>
        /// 将操作结果转为Json返回类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operateResult">操作结果</param>
        /// <returns>Json返回类</returns>
        public static IActionResult ToJsonResult<T>(this OperateResult<T> operateResult)
        {
            return new JsonResult(operateResult.ToAjaxResult());
        }

        /// <summary>
        /// 将操作结果转为Json返回类
        /// </summary>
        /// <param name="operateResult">操作结果</param>
        /// <returns>Json返回类</returns>
        public static IActionResult ToJsonResult(this OperateResult operateResult)
        {
            return new JsonResult(operateResult.ToAjaxResult());
        }

        /// <summary>
        /// 未登录
        /// </summary>
        /// <param name="redirectUrl">跳转地址</param>
        /// <returns>Json返回类</returns>
        public static IActionResult NoLogin(string redirectUrl = null)
        {
            return new JsonResult(AjaxResult.NoLogin(redirectUrl ?? "/Account/Index"));
        }

        /// <summary>
        /// 无权限
        /// </summary>
        /// <returns>Json返回类</returns>
        public static IActionResult NoPerssion()
        {
            return new JsonResult(AjaxResult.NoPerssion());
        }

        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="msg">错误内容</param>
        /// <returns>Json返回类</returns>
        public static IActionResult Failed(string msg)
        {
            return new JsonResult(AjaxResult.Failed(msg));
        }

        /// <summary>
        /// 请求成功
        /// </summary>
        /// <param name="data">结果</param>
        /// <param name="msg">消息内容</param>
        /// <returns>Json返回类</returns>
        public static IActionResult Success(object data, string msg)
        {
            return new JsonResult(AjaxResult.Success(data, msg));
        }

        /// <summary>
        /// 请求成功
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns>Json返回类</returns>
        public static IActionResult Success(string msg)
        {
            return new JsonResult(AjaxResult.Success(null, msg));
        }
    }
}