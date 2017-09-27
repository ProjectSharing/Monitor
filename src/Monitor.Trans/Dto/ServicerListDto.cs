using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ServicerListDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器列表传输对象
    /// 创建标识：yjq 2017/9/27 17:41:26
    /// </summary>
    public class ServicerListDto
    {
        /// <summary>
        /// 服务器ID(主键、自增)
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string FMacAddress { get; set; }

        /// <summary>
        /// 服务器IP(多个用逗号隔开)
        /// </summary>
        public string FIP { get; set; }

        /// <summary>
        /// 服务器名字
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 服务器说明
        /// </summary>
        public string FComment { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime FLastModifyTime { get; set; }
    }
}