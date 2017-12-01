using System.ComponentModel.DataAnnotations;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DatabaseModel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/12/1 14:06:50
    /// </summary>
    public sealed class DatabaseModel : BaseOperateModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? FID { get; set; }

        /// <summary>
        /// 数据库名字
        /// </summary>
        [Required(ErrorMessage = "请输入数据库名字"), StringLength(128, ErrorMessage = "名字过长")]
        public string FName { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        [Required(ErrorMessage = "请输入数据库类型"), StringLength(64, ErrorMessage = "类型过长")]
        public string FDbType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128, ErrorMessage = "说明过长")]
        public string FComment { get; set; }
    }
}