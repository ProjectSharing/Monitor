using Monitor.Constant;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：WarningLogPageQueryWhere.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：警告日志分页查询条件
    /// 创建标识：yjq 2017/10/19 20:10:27
    /// </summary>
    public class WarningLogPageQueryWhere : BasePageQueryWhere
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectID { get; set; }

        /// <summary>
        /// 服务器ID
        /// </summary>
        public int? ServicerID { get; set; }

        /// <summary>
        /// 调用方法
        /// </summary>
        public string CallMethodName { get; set; }

        /// <summary>
        /// 日志来源
        /// </summary>
        public Source? Source { get; set; }

        /// <summary>
        /// 请求的GUID标识，同一个GUID表明是同一次请求
        /// </summary>
        public string RequestGuid { get; set; }

        /// <summary>
        /// 通知状态
        /// </summary>
        public NoticeState? NoticeState { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public DealState? DealState { get; set; }
    }
}