using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：Source.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：来源枚举类
    /// 创建标识：yjq 2017/10/2 20:11:26
    /// </summary>
    [DisplayName("来源端口")]
    public enum Source
    {
        /// <summary>
        /// 前端
        /// </summary>
        [Description("前端")]
        Js = 1,

        /// <summary>
        /// 网站
        /// </summary>
        [Description("网站")]
        Web = 2,

        /// <summary>
        /// IOS
        /// </summary>
        [Description("IOS")]
        Ios = 3,

        /// <summary>
        /// Android
        /// </summary>
        [Description("Android")]
        Android = 4,

        /// <summary>
        /// 接口
        /// </summary>
        [Description("接口")]
        Api = 5,

        /// <summary>
        /// 管理后台
        /// </summary>
        [Description("管理后台")]
        WebManage = 6,

        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other = 7
    }
}