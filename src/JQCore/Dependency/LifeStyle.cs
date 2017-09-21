namespace JQCore.Dependency
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：LifeStyle.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：生命周期枚举
    /// 创建标识：yjq 2017/9/6 16:17:13
    /// </summary>
    public enum LifeStyle
    {
        /// <summary>
        /// 默认
        /// </summary>
        Transient = 1,

        /// <summary>
        /// 单例
        /// </summary>
        Singleton = 2,

        /// <summary>
        /// 在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，且每一个不同的生命周期域，实例是唯一的，不共享的。
        /// </summary>
        PerLifetimeScope = 3
    }
}