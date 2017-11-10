using Monitor.Domain;
using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IWarningInfoOperateLogDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：预警信息操作记录领域服务接口
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public interface IWarningInfoOperateLogDomainService
    {
        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="keyID">主键</param>
        /// <param name="content">操作内容</param>
        /// <param name="operateUserID">操作人</param>
        /// <returns></returns>
        Task AddLogAsync(int type, int keyID, string content, int operateUserID);

        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="keyID">主键</param>
        /// <param name="content">操作内容</param>
        /// <param name="operateUserID">操作人</param>
        void AddLog(int type, int keyID, string content, int operateUserID);

        /// <summary>
        /// 创建操作记录信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="keyID">主键</param>
        /// <param name="content">操作内容</param>
        /// <param name="operateUserID">操作人</param>
        /// <returns>操作记录信息</returns>
        WarningInfoOperateLogInfo Create(int type, int keyID, string content, int operateUserID);
    }
}