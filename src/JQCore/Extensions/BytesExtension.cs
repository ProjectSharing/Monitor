using System;
using System.Text;

namespace JQCore.Extensions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BytesExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：字节数组扩展类
    /// 创建标识：yjq 2017/9/4 17:40:41
    /// </summary>
    public static partial class BytesExtensionm
    {
        /// <summary>
        /// 将字节数组转为字符串类型（UTF8编码转换）
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>转换后的字符串</returns>
        public static string ToStr(this byte[] bytes)
        {
            return ToStr(bytes, Encoding.UTF8);
        }

        /// <summary>
        /// 将字节数组转为字符串类型
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="encoder">编码格式</param>
        /// <returns>转换后的字符串</returns>
        public static string ToStr(this byte[] bytes, Encoding encoder)
        {
            if (encoder == null)
            {
                throw new ArgumentNullException("Encoding");
            }
            return encoder.GetString(bytes);
        }

        /// <summary>
        /// 将字符串转为字节数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] ToBytes(this string str)
        {
            return ToBytes(str, Encoding.UTF8);
        }

        /// <summary>
        /// 将字符串转为字节数组
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="encodeName">转换编码名字</param>
        /// <returns>字节数组</returns>
        public static byte[] ToBytes(this string str, string encodeName)
        {
            return ToBytes(str, Encoding.GetEncoding(encodeName));
        }

        /// <summary>
        /// 将字符串转为字节数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoder">编码格式</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] ToBytes(this string str, Encoding encoder)
        {
            if (encoder == null)
            {
                throw new Exception("编码信息不能为空");
            }
            return encoder.GetBytes(str);
        }
    }
}