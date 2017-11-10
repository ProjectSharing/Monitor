using Monitor.Constant;
using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RuntimeLogListDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：运行日志列表传输对象
    /// 创建标识：yjq 2017/11/10 11:39:51
    /// </summary>
    public class RuntimeLogListDto
    {
        /// <summary>
        /// 主键、自增
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel FLogLevel { get; set; }

        /// <summary>
        /// 项目名字
        /// </summary>
        public string FProjectName { get; set; }

        /// <summary>
        /// 服务器名字
        /// </summary>
        public string FServicerName { get; set; }

        /// <summary>
        /// 调用方法名字
        /// </summary>
        public string FCallMemberName { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string FContent { get; set; }

        /// <summary>
        /// 日志来源
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

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime FCreateTime { get; set; }

        /// <summary>
        /// 判断是否需要警告
        /// </summary>
        /// <returns>true表示需要提示</returns>
        public bool IsNeedWarning()
        {
            return FLogLevel == LogLevel.Error || FLogLevel == LogLevel.Warn || FLogLevel == LogLevel.Fatal;
        }
    }
}