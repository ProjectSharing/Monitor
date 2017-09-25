using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：WarningInfoOperateLogInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：预警信息操作记录
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class WarningInfoOperateLogInfo
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 预警信息类型(1:日志)
        /// </summary>
        public int FType { get; set; }

        /// <summary>
        /// 预警信息ID
        /// </summary>
        public int FWarningInfoID { get; set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        public string FOperateIP { get; set; }

        /// <summary>
        /// 操作地址
        /// </summary>
        public string FOperateUrl { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string FOperateContent { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool FIsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FCreateTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int FCreateUserID { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? FLastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        public int? FLastModifyUserID { get; set; }
    }
}