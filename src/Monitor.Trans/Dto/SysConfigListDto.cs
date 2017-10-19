using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SysConfigDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：配置列表传输对象
    /// 创建标识：yjq 2017/10/12 14:27:46
    /// </summary>
    public class SysConfigListDto
    {
        /// <summary>
		/// 主键、自增
		/// </summary>
		public int FID { get; set; }

        /// <summary>
        /// 键名(唯一不重复)
        /// </summary>
        public string FKey { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string FValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FComment { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime FLastModifyTime { get; set; }
    }
}