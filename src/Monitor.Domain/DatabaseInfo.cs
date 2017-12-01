using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：DatabaseInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：数据库信息
    /// 创建标识：template 2017-12-01 13:29:26
    /// </summary>
    public sealed class DatabaseInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 数据库名字
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string FDbType { get; set; }

        /// <summary>
        /// 备注
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

        public string GetOperateDesc()
        {
            return $"FID:{FID.ToString()}】FName:{FName}】FDbType:{FDbType}】FIsDeleted:{(FIsDeleted ? "是" : "否")}】";
        }
    }
}