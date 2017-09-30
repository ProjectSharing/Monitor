using System.ComponentModel.DataAnnotations;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ServicerModel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器编辑对象
    /// 创建标识：yjq 2017/9/29 16:19:28
    /// </summary>
    public class ServicerModel
    {
        /// <summary>
        /// 服务器ID(主键、自增)
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        [Required(ErrorMessage = "请输入MAC地址"), StringLength(64, ErrorMessage = "MAC地址过长")]
        public string FMacAddress { get; set; }

        /// <summary>
        /// 服务器IP(多个用逗号隔开)
        /// </summary>
        [Required(ErrorMessage = "请输入IP"),StringLength(64, ErrorMessage = "IP过长")]
        public string FIP { get; set; }

        /// <summary>
        /// 服务器名字
        /// </summary>
        [Required(ErrorMessage = "请输入名字"), StringLength(64, ErrorMessage = "名字过长")]
        public string FName { get; set; }

        /// <summary>
        /// 服务器说明
        /// </summary>
        [StringLength(200, ErrorMessage = "说明过长")]
        public string FComment { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateUserID { get; set; }
    }
}