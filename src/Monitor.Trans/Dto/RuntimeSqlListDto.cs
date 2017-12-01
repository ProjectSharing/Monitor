using Monitor.Constant;
using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RuntimeSqlListDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：运行SQL传输对象
    /// 创建标识：yjq 2017/11/30 15:37:48
    /// </summary>
    public class RuntimeSqlListDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 项目名字
        /// </summary>
        public string FProjectName { get; set; }

        /// <summary>
        /// 服务器名字
        /// </summary>
        public string FServicerName { get; set; }

        /// <summary>
        /// 数据库名字
        /// </summary>
        public string FDatabaseName { get; set; }

        /// <summary>
        /// SQL数据库类型
        /// </summary>
        public string FSqlDbType { get; set; }

        /// <summary>
        /// SQL文本
        /// </summary>
        public string FSqlText { get; set; }

        /// <summary>
        /// 调用方法名字或地址
        /// </summary>
        public string FMemberName { get; set; }

        /// <summary>
        /// 请求标识(同一次请求中值相同)
        /// </summary>
        public string FRequestGuid { get; set; }

        /// <summary>
        /// 执行消耗时间
        /// </summary>
        public double FTimeElapsed { get; set; }

        /// <summary>
        /// 日志来源【1:前端,2:网站,3:IOS,4:Android,5:API,6:管理后台,7:其它】
        /// </summary>
        public Source FSource { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool FIsSuccess { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime FExecutedTime { get; set; }
    }
}