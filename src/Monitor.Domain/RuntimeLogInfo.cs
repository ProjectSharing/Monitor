using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：RuntimeLogInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：运行日志信息
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class RuntimeLogInfo
    {
        /// <summary>
        /// 主键、自增
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public int FLogLevel { get; set; }

        /// <summary>
        /// 所属项目
        /// </summary>
        public int FProjectID { get; set; }

        /// <summary>
        /// 项目名字
        /// </summary>
        public string FProjectName { get; set; }

        /// <summary>
        /// 部署服务器ID
        /// </summary>
        public int FServerID { get; set; }

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
        public int FSource { get; set; }

        /// <summary>
        /// 日志生成时间
        /// </summary>
        public DateTime FExecuteTime { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime FCreateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool FIsDeleted { get; set; }
    }
}