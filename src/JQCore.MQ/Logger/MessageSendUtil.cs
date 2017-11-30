using JQCore.Utils;
using System.Collections.Generic;

namespace JQCore.MQ.Logger
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MessageSendUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：消息发送类
    /// 创建标识：yjq 2017/11/6 21:13:55
    /// </summary>
    public class MessageSendUtil
    {
        internal static BufferQueue<MessageInfo> _MessageQueue = new BufferQueue<MessageInfo>(20000, MessageHandle, HaveNoCountHandle);
        private const string _EXCHANGENAME = "Monitor.Message";

        /// <summary>
        /// 消息列表
        /// </summary>
        private static Dictionary<MessageType, List<MessageInfo>> _MessageDic = new Dictionary<MessageType, List<MessageInfo>>();

        /// <summary>
        /// 处理发送的方法
        /// </summary>
        /// <param name="message"></param>
        private static void MessageHandle(MessageInfo message)
        {
            if (_MessageDic.ContainsKey(message.FLogLevel))
            {
                if (_MessageDic[message.FLogLevel].Count > 50)
                {
                    SendMessage(message.FLogLevel, _MessageDic[message.FLogLevel]);
                    _MessageDic[message.FLogLevel].Clear();
                }
                _MessageDic[message.FLogLevel].Add(message);
            }
            else
            {
                _MessageDic.Add(message.FLogLevel, new List<MessageInfo>() { message });
            }
        }

        /// <summary>
        /// 没有更多的日志时
        /// </summary>
        private static void HaveNoCountHandle()
        {
            foreach (var item in _MessageDic)
            {
                SendMessage(item.Key, item.Value);
                item.Value?.Clear();
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="messageList">消息列表</param>
        private static void SendMessage(MessageType messageType, List<MessageInfo> messageList)
        {
            MonitorSendUtil.SendMessage(messageList, _EXCHANGENAME, "Monitor.Message", string.Concat("Monitor.LoggerMessage.", messageType.ToString()));
        }
    }
}