using System;

namespace JQCore.DataAccess.Attributes
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IdentityAttribute.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/27 17:25:13
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class IdentityAttribute : Attribute
    {
    }
}