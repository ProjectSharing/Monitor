using JQCore.DataAccess;
using JQCore.DataAccess.Uow;
using Monitor.Constant;

namespace Monitor.UnitOfWork.Implement
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MonitorUnitOfWork.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：MonitorUnitOfWork
    /// 创建标识：yjq 2017/9/21 22:14:35
    /// </summary>
    public sealed class MonitorUnitOfWork : BaseUnitOfWork, IMonitorUnitOfWork
    {
        public MonitorUnitOfWork(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, DbConnConstant.Conn_Name_Monitor)
        {
        }
    }
}