using Monitor.Constant;
using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RuntimeLogModel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：运行日志对象
    /// 创建标识：yjq 2017/10/2 19:58:08
    /// </summary>
    public class RuntimeLogModel
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel FLogLevel { get; set; }

        /// <summary>
        /// 项目名字
        /// </summary>
        public string FProjectName { get; set; }

        /// <summary>
        /// 服务器Mac地址
        /// </summary>
        public string FServerMac { get; set; }

        /// <summary>
        /// 调用方法名字
        /// </summary>
        public string FCallMemberName { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string FContent { get; set; }

        /// <summary>
        /// 日志来源【1:前端,2:后台,3:IOS,4:Android,5:API,6:其它】
        /// </summary>
        public Source FSource { get; set; }

        /// <summary>
        /// 请求的GUID标识，同一个GUID表明是同一次请求
        /// </summary>
        public string FRequestGuid { get; set; }

        /// <summary>
        /// 日志生成时间
        /// </summary>
        public DateTime FExecuteTime { get; set; }
    }
}