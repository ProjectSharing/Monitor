namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ServicePageQueryWhere.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器分页查询
    /// 创建标识：yjq 2017/9/27 17:16:37
    /// </summary>
    public class ServicePageQueryWhere : BasePageQueryWhere
    {
        /// <summary>
        /// Mac地址
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
    }
}