using System.Collections.Generic;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：TableStructureDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：表结构信息
    /// 创建标识：yjq 2017/12/4 11:41:34
    /// </summary>
    public class TableStructureDto
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 列信息列表
        /// </summary>
        public IEnumerable<TableColumnStructureDto> ColumnList { get; set; }
    }
}