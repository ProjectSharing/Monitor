using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：ProjectInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：项目信息
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class ProjectInfo
    {
        /// <summary>
        /// 项目ID(主键、自增)
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 项目名字(唯一、不重复)
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 项目说明
        /// </summary>
        public string FComment { get; set; }

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