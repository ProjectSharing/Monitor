using System.Linq;
using System.Text.RegularExpressions;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RegexUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/13 11:42:45
    /// </summary>
    public static class RegexUtil
    {
        /// <summary>
        /// 判断是否匹配
        /// </summary>
        /// <param name="input">要判断的字符串</param>
        /// <param name="pattern">匹配规则</param>
        /// <returns>有匹配上的返回true</returns>
        public static bool IsMatch(this string input, string pattern)
        {
            var regexedList = Regex.Matches(input, pattern);
            return regexedList.Any();
        }
    }
}