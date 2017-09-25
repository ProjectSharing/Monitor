using Monitor.Constant;
using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：AdminInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public sealed class AdminInfo
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string FEmail { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 密码盐值
        /// </summary>
        public string FPwdSalt { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string FPwd { get; set; }

        /// <summary>
        /// 管理员状态(1:正常,10:禁用,20:注销)
        /// </summary>
        public UserState FState { get; set; }

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