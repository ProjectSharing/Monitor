using Monitor.Constant;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IOperateLogDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：管理员操作记录领域服务接口
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public interface IOperateLogDomainService
    {
        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="operateModule">操作模块</param>
        /// <param name="operateType">操作类型</param>
        /// <param name="operateContent">操作内容</param>
        void AddOperateLog(int userID, OperateModule operateModule, OperateModuleNode operateType, string operateContent);
    }
}