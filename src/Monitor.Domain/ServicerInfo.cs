using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：ServicerInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器信息
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class ServicerInfo
    {
        /// <summary>
        /// 服务器ID(主键、自增)
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string FMacAddress { get; set; }

        /// <summary>
        /// 服务器IP(多个用逗号隔开)
        /// </summary>
        public string FIP { get; set; }

        /// <summary>
        /// 服务器名字
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 服务器说明
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