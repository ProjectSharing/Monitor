using System.Text;
using System.Web;

namespace JQCore.Extensions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：UrlEncodeExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：Url加解密帮助类
    /// 创建标识：yjq 2017/9/5 15:05:44
    /// </summary>
    public static class UrlEncodeExtension
    {
        /// <summary>
        /// 对字符串进行Url编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <param name="encodeName">编码格式</param>
        /// <returns>Url编码后的字符</returns>
        public static string UrlEncode(this string str, string encodeName)
        {
            return HttpUtility.UrlEncode(str, Encoding.GetEncoding(encodeName));
        }

        /// <summary>
        /// 对字符串进行Url编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 对字符串进行Url解码
        /// </summary>
        /// <param name="str">需要解码的字符串</param>
        /// <param name="decodeName">编码格式</param>
        /// <returns>Url解码后的字符串</returns>
        public static string UrlDecode(this string str, string decodeName)
        {
            return HttpUtility.UrlDecode(str, Encoding.GetEncoding(decodeName));
        }

        /// <summary>
        /// 对字符串进行Url解码
        /// </summary>
        /// <param name="str">需要解码的字符串</param>
        /// <returns>Url解码后的字符串</returns>
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }
    }
}