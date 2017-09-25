using System.ComponentModel.DataAnnotations;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：LoginModel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：登录校验Model
    /// 创建标识：yjq 2017/9/24 22:03:51
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 请输入用户名
        /// </summary>
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 请输入密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Pwd { get; set; }

        /// <summary>
        /// 请输入验证码
        /// </summary>
        [Required(ErrorMessage = "请输入验证码")]
        public string ValidateCode { get; set; }
    }
}