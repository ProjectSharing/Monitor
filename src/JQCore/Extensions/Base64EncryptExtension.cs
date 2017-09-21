using System;
using System.Text;

namespace JQCore.Extensions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：Base64EncryptExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：base64
    /// 创建标识：yjq 2017/9/5 15:04:21
    /// </summary>
    public static class Base64EncryptExtension
    {
        /// <summary>
        /// 将字符串转为base64字符串
        /// </summary>
        /// <param name="input">要转化的字符串</param>
        /// <param name="encode">编码格式默认Utf8</param>
        /// <returns>base64字符串</returns>
        public static string ToBase64(this string input, Encoding encode = null)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            if (encode == null)
            {
                encode = Encoding.UTF8;
            };
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// 将字节数组转为base64字符串
        /// </summary>
        /// <param name="input">要转化的字节数组</param>
        /// <returns>base64字符串</returns>
        public static string ToBase64(this byte[] input)
        {
            if (input != null) return string.Empty;
            return Convert.ToBase64String(input);
        }

        /// <summary>
        /// base64字符解密
        /// </summary>
        /// <param name="input">需要解密的字符信息</param>
        /// <param name="encode">编码格式默认Utf8</param>
        /// <returns>解密后的字符信息</returns>
        public static string DecodeBase64(this string input, Encoding encode)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            if (encode == null)
            {
                encode = Encoding.UTF8;
            };
            byte[] strBytes = Convert.FromBase64String(input);
            return encode.GetString(strBytes);
        }

        /// <summary>
        /// base64字符解密
        /// </summary>
        /// <param name="input">需要解密的字符信息</param>
        /// <returns>解密后的字节数组</returns>
        public static byte[] DecodeBase64(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            return Convert.FromBase64String(input); ;
        }
    }
}