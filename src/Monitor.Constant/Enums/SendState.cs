using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SendState.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：发送状态
    /// 创建标识：yjq 2017/10/10 12:01:32
    /// </summary>
    [DisplayName("发送状态")]
    public enum SendState
    {
        /// <summary>
        /// 待发送
        /// </summary>
        [Description("待发送")]
        WaitSend = 1,

        /// <summary>
        /// 已发送
        /// </summary>
        [Description("已发送")]
        Sended = 2,

        /// <summary>
        /// 发送失败
        /// </summary>
        [Description("发送失败")]
        SendFailed = 3,

        /// <summary>
        /// 不发送
        /// </summary>
        [Description("不发送")]
        NotSend = 4
    }
}