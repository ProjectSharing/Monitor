using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：AdminDetailInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员详情信息
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public sealed class AdminDetailInfo
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
        /// 最后一次登录时间
        /// </summary>
        public DateTime? FlastLoginTime { get; set; }

        /// <summary>
        /// 最后一次登录IP
        /// </summary>
        public string FLastLoginIP { get; set; }

        /// <summary>
        /// 最后一次修改密码时间
        /// </summary>
        public DateTime? FLastChangePwdTime { get; set; }

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