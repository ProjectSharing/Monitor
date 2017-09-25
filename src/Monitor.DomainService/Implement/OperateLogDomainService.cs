using JQCore.Dependency;
using JQCore.Utils;
using JQCore.Web;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：OperateLogDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员操作记录业务逻辑处理
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class OperateLogDomainService : IOperateLogDomainService
    {
        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="operateModule">操作模块</param>
        /// <param name="operateType">操作类型</param>
        /// <param name="operateContent">操作内容</param>
        public void AddOperateLog(int userID, OperateModule operateModule, OperateModuleNode operateType, string operateContent)
        {
            var currentIP = WebHttpContext.RealIP;
            OperateLogInfo operateLogInfo = new OperateLogInfo
            {
                FCreateTime = DateTimeUtil.Now,
                FIsDeleted = false,
                FOperateContent = operateContent,
                FOperateIP = currentIP,
                FModuleType = operateModule,
                FModuleNodeType = operateType,
                FAdminID = userID,
                FModuleName = operateModule.Desc(),
                FModuleNodeName = operateType.Desc(),
                FCreateUserID = userID,
                FOperateUrl = WebHttpContext.AbsoluteUrl
            };
            _OperateQueue.EnqueueMessage(operateLogInfo);
        }

        #region 批量处理新增

        private static BufferQueue<OperateLogInfo> _OperateQueue = new BufferQueue<OperateLogInfo>(20000, MessageHandle, HaveNoCountHandle);

        /// <summary>
        /// 消息列表
        /// </summary>
        private static List<OperateLogInfo> _OperateList = new List<OperateLogInfo>();

        /// <summary>
        /// 信息处理的方法
        /// </summary>
        /// <param name="message">要处理的信息</param>
        private static void MessageHandle(OperateLogInfo message)
        {
            message.FOperateIPAddress = IPSerializerUtil.SearchLocation(message?.FOperateIP)?.ToString();
            if (_OperateList.Count > 200)
            {
                //插入数据库
                AddOperateListAsync().Wait();
                _OperateList.Clear();
            }
            _OperateList.Add(message);
        }

        /// <summary>
        /// 没有新消息的时候处理方法
        /// </summary>
        private static void HaveNoCountHandle()
        {
            AddOperateListAsync().Wait();
            _OperateList.Clear();
        }

        private async static Task AddOperateListAsync()
        {
            await ExceptionUtil.LogExceptionAsync(async () =>
            {
                using (var scope = ContainerManager.BeginLeftScope())
                {
                    var adminOperateLogRepository = scope.Resolve<IOperateLogRepository>();
                    await adminOperateLogRepository.BulkInsertAsync(_OperateList);
                }
            }, memberName: "OperateLogDomainService-AddOperateListAsync");
        }

        #endregion 批量处理新增
    }
}