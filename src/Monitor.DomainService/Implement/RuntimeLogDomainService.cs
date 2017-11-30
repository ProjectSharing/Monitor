using JQCore.Extensions;
using JQCore.Utils;
using Monitor.Cache;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using Monitor.Trans;
using System.Threading.Tasks;
using static Monitor.Constant.IgnoreConstant;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：RuntimeLogDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：运行日志信息业务逻辑处理
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class RuntimeLogDomainService : IRuntimeLogDomainService
    {
        private readonly IProjectCache _projectCache;
        private readonly IProjectDomainService _projectDomainService;
        private readonly IServicerCache _servicerCache;
        private readonly IServicerDomainService _servicerDomainService;
        private readonly IWarningLogDomainService _warningLogDomainService;
        private readonly IWarningLogRepository _warningLogRepository;
        private readonly IEmailSendedRecordDomainService _emailSendedRecordDomainService;

        public RuntimeLogDomainService(IWarningLogDomainService warningLogDomainService, IProjectCache projectCache, IProjectDomainService projectDomainService, IServicerCache servicerCache, IServicerDomainService servicerDomainService, IWarningLogRepository warningLogRepository, IEmailSendedRecordDomainService emailSendedRecordDomainService)
        {
            _projectCache = projectCache;
            _projectDomainService = projectDomainService;
            _servicerCache = servicerCache;
            _servicerDomainService = servicerDomainService;
            _warningLogDomainService = warningLogDomainService;
            _warningLogRepository = warningLogRepository;
            _emailSendedRecordDomainService = emailSendedRecordDomainService;
        }

        /// <summary>
        /// 创建运行日志信息
        /// </summary>
        /// <param name="runtimeLogModel">运行日志</param>
        /// <returns>运行日志信息</returns>
        public async Task<RuntimeLogInfo> CreateAsync(RuntimeLogModel runtimeLogModel)
        {
            runtimeLogModel.NotNull("运行日志不能为空");
            var runtimeLogInfo = new RuntimeLogInfo
            {
                FCallMemberName = runtimeLogModel.FCallMemberName,
                FContent = runtimeLogModel.FContent,
                FCreateTime = DateTimeUtil.Now,
                FExecuteTime = runtimeLogModel.FExecuteTime,
                FIsDeleted = false,
                FLogLevel = runtimeLogModel.FLogLevel,
                FProjectName = runtimeLogModel.FProjectName,
                FServicerMac = runtimeLogModel.FServerMac,
                FSource = runtimeLogModel.FSource,
                FRequestGuid = runtimeLogModel.FRequestGuid
            };
            if (runtimeLogInfo.FProjectName.IsNotNullAndNotEmptyWhiteSpace())
            {
                runtimeLogInfo.FProjectID = await _projectDomainService.GetProjectIDAsync(runtimeLogInfo.FProjectName);
            }
            if (runtimeLogInfo.FServicerMac.IsNotNullAndNotEmptyWhiteSpace())
            {
                runtimeLogInfo.FServicerID = await _servicerDomainService.GetServerIDAsync(runtimeLogInfo.FServicerMac);
            }
            return runtimeLogInfo;
        }

        /// <summary>
        /// 分析日志
        /// </summary>
        /// <param name="runtimeLogInfo">运行日志</param>
        /// <returns></returns>
        public async Task AnalysisLogAsync(RuntimeLogInfo runtimeLogInfo)
        {
            if (runtimeLogInfo != null && runtimeLogInfo.IsNeedWarning())
            {
                var warningLogInfo = _warningLogDomainService.Create(runtimeLogInfo);
                //判断一小时之内有没有超出五条同样日志标识的记录,假如超出则不发送邮件,
                int waitDealCount = await _warningLogRepository.QueryCountAsync(m => m.FLogSign == warningLogInfo.FLogSign && m.FCreateTime >= DateTimeUtil.Now.AddHours(-1) && m.FNoticeState == NoticeState.WaitNotice && m.FIsDeleted == false);
                if (waitDealCount <= 5)
                {
                    //发送邮件
                    await _emailSendedRecordDomainService.SendEmailAsync($"你有来自项目【{runtimeLogInfo.FProjectName}】新的警告邮件", runtimeLogInfo.FContent, runtimeLogInfo.FProjectID);
                    warningLogInfo.FNoticeState = NoticeState.Noticed;
                }
                await _warningLogRepository.InsertOneAsync(warningLogInfo, keyName: "FID", ignoreFields: FID);

            }
        }
    }
}