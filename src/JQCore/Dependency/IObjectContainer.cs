namespace JQCore.Dependency
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IObjectContainer.cs
    /// 接口属性：公共
    /// 类功能描述：IObjectContainer接口
    /// 创建标识：yjq 2017/9/4 11:18:00
    /// </summary>
    public interface IObjectContainer : IIocResolve, IIocRegister
    {
        /// <summary>
        /// 开始一个作用域请求，与其它请求相互独立
        /// </summary>
        /// <returns>IIocScopeResolve</returns>
        IIocScopeResolve BeginLeftScope();
    }
}