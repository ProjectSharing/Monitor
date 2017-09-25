using Monitor.Constant;
using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：OperateLogInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员操作记录
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class OperateLogInfo
    {
        /// <summary>
        /// 主键、自增
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int FAdminID { get; set; }

        /// <summary>
        /// 操作模块类型(枚举)
        /// </summary>
        public OperateModule FModuleType { get; set; }

        /// <summary>
        /// 操作模块名称
        /// </summary>
        public string FModuleName { get; set; }

        /// <summary>
        /// 操作模块节点类型
        /// </summary>
        public OperateModuleNode FModuleNodeType { get; set; }

        /// <summary>
        /// 操作模块节点类型名称
        /// </summary>
        public string FModuleNodeName { get; set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        public string FOperateIP { get; set; }

        /// <summary>
        /// 操作IP地址
        /// </summary>
        public string FOperateIPAddress { get; set; }

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