using System;

namespace JQCore.Extensions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DateTimeExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：时间扩展类
    /// 创建标识：yjq 2017/10/19 14:36:56
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 已默认的时间格式输出
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDefaultFormat(this DateTime dateTime)
        {
            const string defaultFormat = "yyyy-MM-dd HH:mm:sss";
            return dateTime.ToString(defaultFormat);
        }
    }
}