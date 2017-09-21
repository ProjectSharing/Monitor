using System.Security.Cryptography;
using System.Text;

namespace JQCore.Extensions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MD5EncryptExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：加解密扩展类
    /// 创建标识：yjq 2017/9/5 14:59:41
    /// </summary>
    public static partial class MD5EncryptExtension
    {
        #region md5加密

        /// <summary>
        /// 将字节数组转为md5加密后的字符信息
        /// </summary>
        /// <param name="input">要加密的字节数组</param>
        /// <param name="defaultFormat">MD5格式，默认32位MD5</param>
        /// <returns>加密后的md5信息</returns>
        public static string ToMd5(this byte[] input, string defaultFormat = "x2")
        {
            StringBuilder hashBuilder = new StringBuilder();
            MD5 md5 = MD5.Create();
            md5.ComputeHash(input).ForEach(b =>
            {
                hashBuilder.AppendFormat("{0:" + defaultFormat + "}", b);
            });
            return hashBuilder.ToString();
        }

        /// <summary>
        /// 将字符转为md5加密后的字符信息
        /// </summary>
        /// <param name="input">要加密的字符信息</param>
        /// <param name="defaultFormat">MD5格式，默认32位MD5</param>
        /// <returns>加密后的md5信息</returns>
        public static string ToMd5(this string input, string defaultFormat = "x2")
        {
            return input.ToMd5(Encoding.UTF8, defaultFormat: defaultFormat);
        }

        /// <summary>
        /// 将字符转为md5加密后的字符信息
        /// </summary>
        /// <param name="input">要加密的字符信息</param>
        /// <param name="encode">加密编码格式</param>
        /// <param name="defaultFormat">MD5格式，默认32位MD5</param>
        /// <returns>加密后的md5信息</returns>
        public static string ToMd5(this string input, Encoding encode, string defaultFormat = "x2")
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            if (encode == null) return string.Empty;
            return encode.GetBytes(input).ToMd5(defaultFormat: defaultFormat);
        }

        #endregion md5加密
    }
}