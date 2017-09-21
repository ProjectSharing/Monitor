using JQCore.Extensions;
using Newtonsoft.Json;

namespace JQCore.Result
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AjaxResult.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Ajax返回值
    /// 创建标识：yjq 2017/9/11 21:00:01
    /// </summary>
    public class AjaxResult
    {
        #region .ctor

        /// <summary>
        /// .ctor
        /// </summary>
        public AjaxResult()
        {
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="state">状态</param>
        public AjaxResult(AjaxState state)
            : this()
        {
            State = state;
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="msg">备注</param>
        public AjaxResult(AjaxState state, string msg)
            : this(state)
        {
            Message = msg;
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="msg">备注</param>
        /// <param name="data">内容</param>
        public AjaxResult(AjaxState state, string msg, object data)
            : this(state, msg)
        {
            Data = data;
        }

        #endregion .ctor

        /// <summary>
        /// 结果返回状态
        /// 0失败1成功
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public AjaxState State { get; set; }

        /// <summary>
        /// 附加说明
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        [JsonProperty(PropertyName = "redirectUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string RedirectUrl { get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg">失败信息</param>
        /// <returns>ajax结果</returns>
        public static AjaxResult Failed(string msg)
        {
            return new AjaxResult(AjaxState.Failed, msg);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg">备注</param>
        /// <returns>ajax结果</returns>
        public static AjaxResult Success(object data, string msg = "")
        {
            return new AjaxResult(AjaxState.Success, msg, data);
        }

        /// <summary>
        /// 未登录
        /// </summary>
        /// <param name="redirectUrl">跳转地址</param>
        /// <returns>ajax结果</returns>
        public static AjaxResult NoLogin(string redirectUrl)
        {
            return new AjaxResult(AjaxState.NoLogin, "请先登录") { RedirectUrl = redirectUrl };
        }

        /// <summary>
        /// 无权限
        /// </summary>
        /// <returns>ajax结果</returns>
        public static AjaxResult NoPerssion()
        {
            return new AjaxResult(AjaxState.NoPerssion, "无权限");
        }
    }
}