using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ProjectListDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/19 15:08:32
    /// </summary>
    public class ProjectListDto
    {
        /// <summary>
        /// 项目ID(主键、自增)
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 项目名字(唯一、不重复)
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 项目说明
        /// </summary>
        public string FComment { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime FLastModifyTime { get; set; }
    }
}