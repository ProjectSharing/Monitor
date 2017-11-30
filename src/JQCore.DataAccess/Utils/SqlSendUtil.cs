using JQCore.Configuration;
using JQCore.DataAccess.DbClient;
using JQCore.MQ;
using JQCore.Utils;
using JQCore.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JQCore.DataAccess.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SqlSendUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/30 14:18:13
    /// </summary>
    internal class SqlSendUtil
    {
        internal static BufferQueue<RuntimeSqlModel> _MessageQueue = new BufferQueue<RuntimeSqlModel>(20000, MessageHandle, HaveNoCountHandle);
        private const string _EXCHANGENAME = "Monitor.Message";

        /// <summary>
        /// 消息列表
        /// </summary>
        private static List<RuntimeSqlModel> _MessageList = new List<RuntimeSqlModel>();

        /// <summary>
        /// 处理发送的方法
        /// </summary>
        /// <param name="message"></param>
        private static void MessageHandle(RuntimeSqlModel message)
        {
            if (_MessageList.Count > 50)
            {
                SendMessage(_MessageList);
            }
            _MessageList.Add(message);
        }

        /// <summary>
        /// 没有更多的日志时
        /// </summary>
        private static void HaveNoCountHandle()
        {
            SendMessage(_MessageList);
            _MessageList.Clear();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="messageList">消息列表</param>
        private static void SendMessage(List<RuntimeSqlModel> messageList)
        {
            MonitorSendUtil.SendMessage(messageList, _EXCHANGENAME, "Monitor.Sql", "Monitor.Sql.*");
        }

        /// <summary>
        /// 添加sql运行信息
        /// </summary>
        /// <param name="runtimeSqlModel"></param>
        public static void AddMessage(RuntimeSqlModel runtimeSqlModel)
        {
            _MessageQueue.EnqueueMessage(runtimeSqlModel);
        }

        /// <summary>
        /// 抓取sql运行信息
        /// </summary>
        /// <param name="memberName">调用方法名字</param>
        /// <param name="timeElapsed">消耗时间</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="dbType">数据库类型</param>
        public static void GrabSql(string memberName, double timeElapsed, bool isSuccess, string dbType = null)
        {
            try
            {
                if (!IsSend)
                {
                    return;
                }
                var runtimeSqlModel = Create(memberName, timeElapsed, isSuccess, dbType: dbType);
                AddMessage(runtimeSqlModel);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, "GrabSql", SqlMonitorUtil._LOGGER_SQL);
            }
        }

        /// <summary>
        /// 抓取sql运行信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="timeElapsed">消耗时间</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="dbType">数据库类型</param>
        public static void GrabSql(SqlQuery sqlQuery, double timeElapsed, bool isSuccess, string dbType = null)
        {
            try
            {
                if (!IsSend)
                {
                    return;
                }
                var runtimeSqlModel = Create(sqlQuery, timeElapsed, isSuccess, dbType: dbType);
                AddMessage(runtimeSqlModel);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, "GrabSql", SqlMonitorUtil._LOGGER_SQL);
            }
        }

        /// <summary>
        /// 是否需要发送SQL
        /// </summary>
        private static bool IsSend
        {
            get
            {
                try
                {
                    return ConfigurationManage.GetValue<bool>($"MQMonitor:ProjectInfo:IsSendSql");
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 创建运行SQL信息
        /// </summary>
        /// <param name="MemberName">调用方法名字</param>
        /// <param name="timeElapsed">消耗时间</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        private static RuntimeSqlModel Create(string MemberName, double timeElapsed, bool isSuccess, string dbType = null)
        {
            var runtimeSqlModel = new RuntimeSqlModel
            {
                FExecutedTime = DateTimeUtil.Now,
                FIsSuccess = isSuccess,
                FProjectName = ConfigurationManage.GetValue($"MQMonitor:ProjectInfo:ProjectName"),
                FRequestGuid = AsyncLocalUtil.CurrentGID,
                FServerMac = ComputerUtil.MacAddress,
                FSource = ConfigurationManage.GetValue<int>($"MQMonitor:ProjectInfo:FSource"),
                FSqlDbType = dbType,
                FSqlText = MemberName,
                FTimeElapsed = timeElapsed,
                FMemberName = WebHttpContext.AbsoluteUrl
            };
            return runtimeSqlModel;
        }

        /// <summary>
        /// 创建运行SQL信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="timeElapsed">消耗时间</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        private static RuntimeSqlModel Create(SqlQuery sqlQuery, double timeElapsed, bool isSuccess, string dbType = null)
        {
            var runtimeSqlModel = new RuntimeSqlModel
            {
                FExecutedTime = DateTimeUtil.Now,
                FIsSuccess = isSuccess,
                FProjectName = ConfigurationManage.GetValue($"MQMonitor:ProjectInfo:ProjectName"),
                FRequestGuid = AsyncLocalUtil.CurrentGID,
                FServerMac = ComputerUtil.MacAddress,
                FSource = ConfigurationManage.GetValue<int>($"MQMonitor:ProjectInfo:FSource"),
                FSqlDbType = dbType,
                FSqlText = sqlQuery.CommandText,
                FTimeElapsed = timeElapsed,
                FMemberName = WebHttpContext.AbsoluteUrl
            };
            if (sqlQuery.ParameterList != null && sqlQuery.ParameterList.Any())
            {
                runtimeSqlModel.ParameterList = new List<SqlParameterModel>();
                foreach (var item in sqlQuery.ParameterList)
                {
                    runtimeSqlModel.ParameterList.Add(new SqlParameterModel
                    {
                        FName = item.ParameterName,
                        FSize = item.Size,
                        FValue = item.Value?.ToString()
                    });
                }
            }
            return runtimeSqlModel;
        }
    }

    /// <summary>
    /// 运行sql信息
    /// </summary>
    internal class RuntimeSqlModel
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
        public int FSource { get; set; } = 2;

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
    internal class SqlParameterModel
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