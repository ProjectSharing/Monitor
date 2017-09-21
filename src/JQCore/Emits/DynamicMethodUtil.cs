using System;
using System.Collections.Concurrent;
using System.Reflection.Emit;

namespace JQCore.Emits
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DynamicMethodUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：动态方法工具类
    /// 创建标识：yjq 2017/7/12 18:30:53
    /// </summary>
    public static class DynamicMethodUtil
    {
        #region 根据类型获取将object转为ParamList的方法

        private static ConcurrentDictionary<RuntimeTypeHandle, ConcurrentDictionary<RuntimeTypeHandle, DynamicMethod>> _ObjectToParamListMethodCache = new ConcurrentDictionary<RuntimeTypeHandle, ConcurrentDictionary<RuntimeTypeHandle, DynamicMethod>>();

        /// <summary>
        /// 根据类型获取将object转为ParamList的方法
        /// </summary>
        /// <typeparam name="TParam">DbParameter类型</typeparam>
        /// <param name="objType">object的类型</param>
        /// <returns>将object转为ParamList的方法</returns>
        public static DynamicMethod GetObjectToParamListMethod<TParam>(Type objType)
        {
            ConcurrentDictionary<RuntimeTypeHandle, DynamicMethod> objectToParamListMethod;
            _ObjectToParamListMethodCache.TryGetValue(objType.TypeHandle, out objectToParamListMethod);
            if (objectToParamListMethod != null)
            {
                DynamicMethod method;
                objectToParamListMethod.TryGetValue(typeof(TParam).TypeHandle, out method);
                if (method == null)
                {
                    method = EmitUtil.CreateObjectToParamListMethod<TParam>(objType);
                    objectToParamListMethod.TryAdd(typeof(TParam).TypeHandle, method);
                    _ObjectToParamListMethodCache[objType.TypeHandle] = objectToParamListMethod;
                }
                return method;
            }
            else
            {
                objectToParamListMethod = new ConcurrentDictionary<RuntimeTypeHandle, DynamicMethod>();
                DynamicMethod method = EmitUtil.CreateObjectToParamListMethod<TParam>(objType);
                objectToParamListMethod.TryAdd(typeof(TParam).TypeHandle, method);
                _ObjectToParamListMethodCache[objType.TypeHandle] = objectToParamListMethod;
                return method;
            }
        }

        #endregion 根据类型获取将object转为ParamList的方法
    }
}