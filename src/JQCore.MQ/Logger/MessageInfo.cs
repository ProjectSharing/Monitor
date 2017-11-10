using JQCore.Utils;
using JQCore.Web;
using System;

namespace JQCore.MQ.Logger
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MessageInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/6 20:52:09
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public MessageType FLogLevel { get; set; }

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
        /// 日志来源【1:前端,2:网站,3:IOS,4:Android,5:API,6:管理后台,7:其它】
        /// </summary>
        public int FSource { get; set; } = 2;

        /// <summary>
        /// 请求的GUID标识，同一个GUID表明是同一次请求
        /// </summary>
        public string FRequestGuid { get; set; }

        /// <summary>
        /// 日志生成时间
        /// </summary>
        public DateTime FExecuteTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建消息信息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="projectName">项目名字</param>
        /// <param name="source">来源</param>
        /// <returns>消息信息</returns>
        public static MessageInfo Create(string message, MessageType messageType, string projectName, int source)
        {
            return new MessageInfo
            {
                FContent = message,
                FLogLevel = messageType,
                FProjectName = projectName,
                FSource = source,
                FRequestGuid = AsyncLocalUtil.CurrentGID,
                FServerMac = ComputerUtil.MacAddress,
                FCallMemberName = WebHttpContext.AbsoluteUrl

            };
        }
    }
}