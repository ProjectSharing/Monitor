using System;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ParameterCheckUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：主要用于业务的校验
    /// 创建标识：yjq 2017/9/4 17:49:52
    /// </summary>
    public static class ParameterCheckUtil
    {
        /// <summary>
        /// 判断值不为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="argument">错误后要提示的信息</param>
        public static T NotNull<T>(this T value, string argument)
        {
            if (value == null)
            {
                throw new BizException(argument);
            }
            return value;
        }

        #region 比较是否大于、小于、等于

        /// <summary>
        /// 比较是否相等
        /// </summary>
        /// <typeparam name="T">比较的类型</typeparam>
        /// <param name="compareValue">要判断的值</param>
        /// <param name="targetValue">被比较的值</param>
        /// <param name="msg">错误内容</param>
        public static T Equal<T>(this T @compareValue, T targetValue, string msg) where T : IComparable<T>
        {
            if (!(@compareValue.CompareTo(targetValue) == 0))
            {
                throw new BizException(msg);
            }
            return @compareValue;
        }

        /// <summary>
        /// 比较是否大于等于
        /// </summary>
        /// <typeparam name="T">比较的类型</typeparam>
        /// <param name="compareValue">要判断的值</param>
        /// <param name="targetValue">被比较的值</param>
        /// <param name="msg">错误内容</param>
        public static T GreaterThanOrEqual<T>(this T @compareValue, T targetValue, string msg) where T : IComparable<T>
        {
            if (@compareValue.CompareTo(targetValue) < 0)
            {
                throw new BizException(msg);
            }
            return @compareValue;
        }

        /// <summary>
        /// 比较是否大于
        /// </summary>
        /// <typeparam name="T">比较的类型</typeparam>
        /// <param name="compareValue">要判断的值</param>
        /// <param name="targetValue">被比较的值</param>
        /// <param name="msg">错误内容</param>
        public static T GreaterThan<T>(this T @compareValue, T targetValue, string msg) where T : IComparable<T>
        {
            if (@compareValue.CompareTo(targetValue) <= 0)
            {
                throw new BizException(msg);
            }
            return @compareValue;
        }

        #endregion 比较是否大于、小于、等于

        #region 字符串判断

        /// <summary>
        /// 判断字符串不是 null、空、不是由空白字符组成。
        /// </summary>
        /// <param name="input">要判断的字符</param>
        /// <param name="msg">错误信息</param>
        public static object NotNullAndNotEmptyWhiteSpace(this object input, string msg)
        {
            if (input == null)
            {
                throw new BizException(msg);
            }
            if (string.IsNullOrWhiteSpace(input.ToString()))
            {
                throw new BizException(msg);
            }
            return input;
        }

        #endregion 字符串判断
    }
}