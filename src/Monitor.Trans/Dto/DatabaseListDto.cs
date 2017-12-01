using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DatabaseListDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：数据库传输对象
    /// 创建标识：yjq 2017/12/1 13:45:59
    /// </summary>
    public class DatabaseListDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 数据库名字
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string FDbType { get; set; }

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