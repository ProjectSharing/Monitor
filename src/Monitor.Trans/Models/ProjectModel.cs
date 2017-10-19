using System.ComponentModel.DataAnnotations;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ProjectModel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：项目编辑信息
    /// 创建标识：yjq 2017/10/19 15:18:08
    /// </summary>
    public class ProjectModel : BaseOperateModel
    {
        /// <summary>
        /// 项目ID(主键、自增)
        /// </summary>
        public int? FID { get; set; }

        /// <summary>
        /// 项目名字(唯一、不重复)
        /// </summary>
        [Required(ErrorMessage = "请输入项目名字"), StringLength(50, ErrorMessage = "名字过长")]
        public string FName { get; set; }

        /// <summary>
        /// 项目说明
        /// </summary>
        [StringLength(200, ErrorMessage = "说明过长")]
        public string FComment { get; set; }
    }
}