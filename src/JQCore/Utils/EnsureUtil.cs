using System;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：EnsureUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：断言工具类
    /// 创建标识：yjq 2017/9/5 14:15:44
    /// </summary>
    public static class EnsureUtil
    {
        /// <summary>
        /// 断言值不为NULL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument">要判断的值</param>
        /// <param name="argumentName">参数名称</param>
        public static void NotNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName + " 不能为空.");
        }

        /// <summary>
        /// 断言值不为NULL且不为空
        /// </summary>
        /// <param name="argument">要判断的值</param>
        /// <param name="argumentName">参数名称</param>
        public static void NotNullAndNotEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
                throw new ArgumentNullException(argumentName + " 不能为空.");
        }

        /// <summary>
        /// 断言字符串长度
        /// </summary>
        /// <param name="argument">要判断的值</param>
        /// <param name="length">要判断的长度</param>
        /// <param name="argumentName">错误信息</param>
        public static void Length(string argument, int length, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
                throw new ArgumentNullException(argumentName + " 不能为空.");
            if (argument.Length != length)
            {
                throw new NotSupportedException(argumentName + " 长度必须为" + length.ToString() + "位.");
            }
        }

        /// <summary>
        /// 断言值位于两者之间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="errorMsg">错误信息</param>
        public static void Between<T>(T value, T min, T max, string errorMsg) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            {
                throw new NotSupportedException(errorMsg);
            }
        }

        /// <summary>
        /// 断言值大于
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="compareValue">比较的值</param>
        /// <param name="errorMsg">错误信息</param>
        public static void MoreThan<T>(T value, T compareValue, string errorMsg) where T : IComparable<T>
        {
            if (value.CompareTo(compareValue) <= 0)
            {
                throw new NotSupportedException(errorMsg);
            }
        }

        /// <summary>
        /// 断言值大于等于
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="compareValue">比较的值</param>
        /// <param name="errorMsg"></param>
        public static void MoreThanOrEqual<T>(T value, T compareValue, string errorMsg) where T : IComparable<T>
        {
            if (value.CompareTo(compareValue) < 0)
            {
                throw new NotSupportedException(errorMsg);
            }
        }

        /// <summary>
        /// 断言值小于
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="compareValue">比较的值</param>
        /// <param name="errorMsg">错误信息</param>
        public static void LessThan<T>(T value, T compareValue, string errorMsg) where T : IComparable<T>
        {
            if (value.CompareTo(compareValue) >= 0)
            {
                throw new NotSupportedException(errorMsg);
            }
        }

        /// <summary>
        /// 断言值小于等于
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="compareValue">比较的值</param>
        /// <param name="errorMsg">错误信息</param>
        public static void LessThanOrEqual<T>(T value, T compareValue, string errorMsg) where T : IComparable<T>
        {
            if (value.CompareTo(compareValue) > 0)
            {
                throw new NotSupportedException(errorMsg);
            }
        }
    }
}