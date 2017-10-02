using JQCore.Mvc.Extensions;
using JQCore.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        /// 返回JsonResult
        /// </summary>
        /// <typeparam name="TController">控制器类型</typeparam>
        /// <param name="controller">当前控制器</param>
        /// <param name="operateResultAction">操作方法</param>
        /// <returns></returns>
        public static IActionResult JsonResult<TController>(this TController controller, Func<OperateResult> operateResultAction) where TController : Controller
        {
            if (!controller.ModelState.IsValid)
            {
                return Failed(controller.ModelState.GetFirstErrorMsg());
            }
            else
            {
                var operateResult = operateResultAction();
                return operateResult.ToJsonResult();
            }
        }

        /// <summary>
        /// 异步返回JsonResult
        /// </summary>
        /// <typeparam name="TController">控制器类型</typeparam>
        /// <param name="controller">当前控制器</param>
        /// <param name="operateResultActionTask">操作方法</param>
        /// <returns></returns>
        public static async Task<IActionResult> JsonResultAsync<TController>(this TController controller, Func<Task<OperateResult>> operateResultActionTask) where TController : Controller
        {
            if (!controller.ModelState.IsValid)
            {
                return Failed(controller.ModelState.GetFirstErrorMsg());
            }
            else
            {
                var operateResult = await operateResultActionTask();
                return operateResult.ToJsonResult();
            }
        }

        /// <summary>
        /// 返回视图结果
        /// </summary>
        /// <typeparam name="TController">控制器类型</typeparam>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="controller">当前控制器</param>
        /// <param name="operateResult">操作结果</param>
        /// <param name="redirectViewName">指定的返回页面</param>
        /// <returns></returns>
        public static IActionResult ViewResult<TController, T>(this TController controller, OperateResult<T> operateResult, string redirectViewName = null) where TController : Controller
        {
            if (!operateResult.SuccessAndValueNotNull)
            {
                return controller.View(redirectViewName ?? "/Views/Shared/NotFind.cshtml");
            }
            else
            {
                return controller.View(operateResult.Value);
            }
        }

        /// <summary>
        /// 返回视图结果
        /// </summary>
        /// <typeparam name="TController">控制器类型</typeparam>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="controller">当前控制器</param>
        /// <param name="operateResultAction">操作结果</param>
        /// <param name="redirectViewName">指定的返回页面</param>
        /// <returns></returns>
        public static IActionResult ViewResult<TController, T>(this TController controller, Func<OperateResult<T>> operateResultAction, string redirectViewName = null) where TController : Controller
        {
            var operateResult = operateResultAction();
            return ViewResult(controller, operateResult, redirectViewName: redirectViewName);
        }

        /// <summary>
        /// 异步返回视图结果
        /// </summary>
        /// <typeparam name="TController">控制器类型</typeparam>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="controller">当前控制器</param>
        /// <param name="operateResultTask">操作结果任务</param>
        /// <param name="redirectViewName">指定的返回页面</param>
        /// <returns></returns>
        public static async Task<IActionResult> ViewResultAsync<TController, T>(this TController controller, Task<OperateResult<T>> operateResultTask, string redirectViewName = null) where TController : Controller
        {
            var operateResult = await operateResultTask;
            return ViewResult(controller, operateResult, redirectViewName: redirectViewName);
        }

        /// <summary>
        /// 异步返回视图结果
        /// </summary>
        /// <typeparam name="TController">控制器类型</typeparam>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="controller">当前控制器</param>
        /// <param name="operateResultActionTask">操作结果任务</param>
        /// <param name="redirectViewName">指定的返回页面</param>
        /// <returns></returns>
        public static async Task<IActionResult> ViewResultAsync<TController, T>(this TController controller, Func<Task<OperateResult<T>>> operateResultActionTask, string redirectViewName = null) where TController : Controller
        {
            var operateResult = await operateResultActionTask();
            return ViewResult(controller, operateResult, redirectViewName: redirectViewName);
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