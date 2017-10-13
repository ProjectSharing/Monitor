namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SysConfigPageQueryWhere.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：系统配置分页查询对象
    /// 创建标识：yjq 2017/10/12 14:18:27
    /// </summary>
    public sealed class SysConfigPageQueryWhere : BasePageQueryWhere
    {
        /// <summary>
        /// 配置的键
        /// </summary>
        public string ConfigKey { get; set; }
    }
}