using Monitor.Constant;
using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：WarningLogListDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：警告日志列表传输对象
    /// 创建标识：yjq 2017/11/9 13:52:17
    /// </summary>
    public class WarningLogListDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 处理建议
        /// </summary>
        public string FOperateAdvice { get; set; }

        /// <summary>
        /// 通知状态(1:未通知 2:已通知 3:通知失败)
        /// </summary>
        public NoticeState FNoticeState { get; set; }

        /// <summary>
        /// 处理状态(1:待处理,2:已处理,3:不处理)
        /// </summary>
        public DealState FDealState { get; set; }

        /// <summary>
        /// 处理方案
        /// </summary>
        public string FTreatmentScheme { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime FLastModifyTime { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel FLogLevel { get; set; }

        /// <summary>
        /// 项目名字
        /// </summary>
        public string FProjectName { get; set; }

        /// <summary>
        /// 服务器名字
        /// </summary>
        public string FServicerName { get; set; }

        /// <summary>
        /// 调用方法名字
        /// </summary>
        public string FCallMemberName { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string FContent { get; set; }

        /// <summary>
        /// 日志来源
        /// </summary>
        public Source FSource { get; set; }

        /// <summary>
        /// 日志生成时间
        /// </summary>
        public DateTime FExecuteTime { get; set; }

        /// <summary>
        /// 请求的GUID标识，同一个GUID表明是同一次请求
        /// </summary>
        public string FRequestGuid { get; set; }
    }
}