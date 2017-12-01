using Monitor.Constant;
using System;
using System.Collections.Generic;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RuntimeSqlModel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/29 22:32:08
    /// </summary>
    public class RuntimeSqlModel
    {
        /// <summary>
        /// 项目名字
        /// </summary>
        public string FProjectName { get; set; }

        /// <summary>
        /// 服务器Mac地址
        /// </summary>
        public string FServerMac { get; set; }

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
        /// 数据库名字
        /// </summary>
        public string FDatabaseName { get; set; }

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

        /// <summary>
        /// 参数列表
        /// </summary>
        public List<SqlParameterModel> ParameterList { get; set; }
    }

    /// <summary>
    /// 参数信息
    /// </summary>
    public class SqlParameterModel
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string FValue { get; set; }

        /// <summary>
        /// 参数长度
        /// </summary>
        public int FSize { get; set; }
    }
}