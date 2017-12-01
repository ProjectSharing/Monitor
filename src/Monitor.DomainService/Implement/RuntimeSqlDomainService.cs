using JQCore.Extensions;
using JQCore.Utils;
using Monitor.Cache;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using Monitor.Trans;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Monitor.Constant.IgnoreConstant;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：RuntimeSqlDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：执行SQL信息业务逻辑处理
    /// 创建标识：template 2017-11-29 22:17:30
    /// </summary>
    public sealed class RuntimeSqlDomainService : IRuntimeSqlDomainService
    {
        private readonly IProjectDomainService _projectDomainService;
        private readonly IServicerDomainService _servicerDomainService;
        private readonly IRuntimeSqlRepository _runtimeSqlRepository;
        private readonly ISqlParameterRepository _sqlParameterRepository;
        private readonly ISysConfigCache _sysConfigCache;
        private readonly IWarningSqlDomainService _warningSqlDomainService;
        private readonly IWarningSqlRepository _warningSqlRepository;
        private readonly IEmailSendedRecordDomainService _emailSendedRecordDomainService;
        private readonly IDatabaseDomainService _databaseDomainService;

        public RuntimeSqlDomainService(IProjectDomainService projectDomainService, IServicerDomainService servicerDomainService, IRuntimeSqlRepository runtimeSqlRepository, ISqlParameterRepository sqlParameterRepository, ISysConfigCache sysConfigCache, IWarningSqlDomainService warningSqlDomainService, IWarningSqlRepository warningSqlRepository, IEmailSendedRecordDomainService emailSendedRecordDomainService, IDatabaseDomainService databaseDomainService)
        {
            _projectDomainService = projectDomainService;
            _servicerDomainService = servicerDomainService;
            _runtimeSqlRepository = runtimeSqlRepository;
            _sqlParameterRepository = sqlParameterRepository;
            _sysConfigCache = sysConfigCache;
            _warningSqlDomainService = warningSqlDomainService;
            _warningSqlRepository = warningSqlRepository;
            _emailSendedRecordDomainService = emailSendedRecordDomainService;
            _databaseDomainService = databaseDomainService;
        }

        /// <summary>
        /// 添加运行sql信息
        /// </summary>
        /// <param name="runtimeSqlModel"></param>
        /// <returns></returns>
        public async Task<RuntimeSqlInfo> AddRuntimeSqlAsync(RuntimeSqlModel runtimeSqlModel)
        {
            runtimeSqlModel.NotNull("运行SQL不能为空");
            var runtimeSqlInfo = await CreateAsync(runtimeSqlModel);
            var runtimeSqlID = (await _runtimeSqlRepository.InsertOneAsync(runtimeSqlInfo, keyName: "FID", ignoreFields: FID)).ToSafeInt32(0);
            runtimeSqlInfo.FID = runtimeSqlID;
            var sqlParameterList = CreateSqlParameterList(runtimeSqlModel, runtimeSqlID);
            if (sqlParameterList.IsNotEmpty())
            {
                await _sqlParameterRepository.BulkInsertAsync(sqlParameterList);
            }
            return runtimeSqlInfo;
        }

        /// <summary>
        /// 创建运行sql信息
        /// </summary>
        /// <param name="runtimeSqlModel"></param>
        /// <returns></returns>
        private async Task<RuntimeSqlInfo> CreateAsync(RuntimeSqlModel runtimeSqlModel)
        {
            var runtimeSqlInfo = new RuntimeSqlInfo
            {
                FCreateTime = DateTimeUtil.Now,
                FExecutedTime = runtimeSqlModel.FExecutedTime,
                FIsDeleted = false,
                FIsSuccess = runtimeSqlModel.FIsSuccess,
                FProjectName = runtimeSqlModel.FProjectName,
                FRequestGuid = runtimeSqlModel.FRequestGuid,
                FServicerMac = runtimeSqlModel.FServerMac,
                FSqlDbType = runtimeSqlModel.FSqlDbType,
                FSqlText = runtimeSqlModel.FSqlText,
                FTimeElapsed = runtimeSqlModel.FTimeElapsed,
                FMemberName = runtimeSqlModel.FMemberName,
                FSource = runtimeSqlModel.FSource,
                FDatabaseName = runtimeSqlModel.FDatabaseName
            };
            if (runtimeSqlInfo.FProjectName.IsNotNullAndNotEmptyWhiteSpace())
            {
                runtimeSqlInfo.FProjectID = await _projectDomainService.GetProjectIDAsync(runtimeSqlInfo.FProjectName);
            }
            if (runtimeSqlInfo.FServicerMac.IsNotNullAndNotEmptyWhiteSpace())
            {
                runtimeSqlInfo.FServicerID = await _servicerDomainService.GetServerIDAsync(runtimeSqlInfo.FServicerMac);
            }
            if (runtimeSqlModel.FDatabaseName.IsNotNullAndNotEmptyWhiteSpace())
            {
                runtimeSqlInfo.FDatabeseID = await _databaseDomainService.GetDatabaseIDAsync(runtimeSqlModel.FDatabaseName, runtimeSqlModel.FSqlDbType);
            }
            return runtimeSqlInfo;
        }

        /// <summary>
        /// 创建sql参数信息
        /// </summary>
        /// <param name="runtimeSqlModel"></param>
        /// <param name="sqlID"></param>
        /// <returns></returns>
        private List<SqlParameterInfo> CreateSqlParameterList(RuntimeSqlModel runtimeSqlModel, int sqlID)
        {
            var paramList = new List<SqlParameterInfo>();
            if (runtimeSqlModel != null && runtimeSqlModel.ParameterList.IsNotEmpty())
            {
                foreach (var item in runtimeSqlModel.ParameterList)
                {
                    paramList.Add(new SqlParameterInfo
                    {
                        FCreateTime = DateTimeUtil.Now,
                        FIsDeleted = false,
                        FName = item.FName,
                        FRuntimeSqlID = sqlID,
                        FValue = item.FValue,
                        FSize = item.FSize
                    });
                }
            }
            return paramList;
        }

        /// <summary>
        /// 分析运行的sql信息
        /// </summary>
        /// <param name="runtimeSqlInfo"></param>
        /// <returns></returns>
        public async Task AnalysisRuntimeSqlAsync(RuntimeSqlInfo runtimeSqlInfo)
        {
            await Task.Delay(1);
            if (await IsNeedWarningAsync(runtimeSqlInfo))
            {
                var warningSqlInfo = _warningSqlDomainService.Create(runtimeSqlInfo);
                //判断一小时之内有没有超出五条同样sql标识的记录,假如超出则不发送邮件,
                int waitDealCount = await _warningSqlRepository.QueryCountAsync(m => m.FSqlSign == warningSqlInfo.FSqlSign && m.FCreateTime >= DateTimeUtil.Now.AddHours(-1) && m.FNoticeState == NoticeState.WaitNotice && m.FIsDeleted == false);
                if (waitDealCount <= 5)
                {
                    //发送邮件
                    await _emailSendedRecordDomainService.SendEmailAsync($"你有来自项目【{runtimeSqlInfo.FProjectName}】新的警告邮件", runtimeSqlInfo.FSqlText, runtimeSqlInfo.FProjectID);
                    warningSqlInfo.FNoticeState = NoticeState.Noticed;
                }
                await _warningSqlRepository.InsertOneAsync(warningSqlInfo, keyName: "FID", ignoreFields: FID);
            }
        }

        /// <summary>
        /// 判断是否需要预警
        /// </summary>
        /// <param name="runtimeSqlInfo"></param>
        /// <returns></returns>
        private async Task<bool> IsNeedWarningAsync(RuntimeSqlInfo runtimeSqlInfo)
        {
            if (runtimeSqlInfo != null)
            {
                if (!runtimeSqlInfo.FIsSuccess)
                {
                    return true;
                }
                var minWarningTimeElapsed = (await _sysConfigCache.GetValueAsync(SysConfigKey.SqlWarningMinTimeElapsed)).ToSafeDouble(2000);
                return runtimeSqlInfo.FTimeElapsed >= minWarningTimeElapsed;
            }
            return false;
        }
    }
}