using JQCore.Emits;
using System;
using System.Collections.Generic;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DbParamUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：sql参数工具类
    /// 创建标识：yjq 2017/9/5 19:30:45
    /// </summary>
    public static class DbParamUtil
    {
        /// <summary>
        /// 将object转为DbParameterList
        /// </summary>
        /// <typeparam name="TParam">DbParameter</typeparam>
        /// <param name="obj">要转换的值</param>
        /// <param name="sign">参数符号</param>
        /// <param name="prefix">参数前缀</param>
        /// <returns>DbParameterList</returns>
        public static List<TParam> ToDbParam<TParam>(this object obj, string sign, string prefix)
        {
            if (obj == null) return default(List<TParam>);
            var convertMethod = DynamicMethodUtil.GetObjectToParamListMethod<TParam>(obj.GetType());
            var action = (Func<object, string, string, List<TParam>>)convertMethod.CreateDelegate(typeof(Func<object, string, string, List<TParam>>));
            return action(obj, sign, prefix);
        }
    }
}