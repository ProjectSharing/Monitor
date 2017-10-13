using System.ComponentModel.DataAnnotations;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SysConfigModel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：系统配置编辑对象
    /// 创建标识：yjq 2017/10/12 15:23:08
    /// </summary>
    public class SysConfigModel : BaseOperateModel
    {
        /// <summary>
        /// 主键、自增
        /// </summary>
        public int? FID { get; set; }

        /// <summary>
        /// 键名(唯一不重复)
        /// </summary>
        [Required(ErrorMessage = "请输入键名"), StringLength(64, ErrorMessage = "键名过长")]
        public string FKey { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "请输入值"), StringLength(256, ErrorMessage = "值过长")]
        public string FValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256, ErrorMessage = "备注过长")]
        public string FComment { get; set; }
    }
}