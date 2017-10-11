using StackExchange.Redis;
using System;

namespace JQCore.Redis
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：RedisSaveTypeExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/7/15 15:51:57
    /// </summary>
    internal static class RedisSaveTypeExtension
    {
        public static SaveType ToSveType(this RedisSaveType redisSaveType)
        {
            switch (redisSaveType)
            {
                case RedisSaveType.BackgroundRewriteAppendOnlyFile:
                    return SaveType.BackgroundRewriteAppendOnlyFile;

                case RedisSaveType.BackgroundSave:
                    return SaveType.BackgroundSave;

                default:
                    throw new NotSupportedException(redisSaveType.ToString());
            }
        }
    }
}