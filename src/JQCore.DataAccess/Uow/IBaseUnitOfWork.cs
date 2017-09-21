using System;
using System.Data;
using System.Threading.Tasks;

namespace JQCore.DataAccess.Uow
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IBaseUnitOfWork.cs
    /// 接口属性：公共
    /// 类功能描述：基础工作单元接口
    /// 创建标识：yjq 2017/9/5 20:41:41
    /// </summary>
    public interface IBaseUnitOfWork
    {
        /// <summary>
        /// 开始
        /// </summary>
        void BeginTran();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="il">事务隔离级别</param>
        void BeginTran(IsolationLevel il);

        /// <summary>
        /// 提交
        /// </summary>
        bool CommitTran(bool isAutoRollback = false);

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();

        /// <summary>
        /// 执行事务,失败时自动回滚,有异常时回滚后抛出
        /// </summary>
        /// <param name="action"></param>
        bool ExecuteTranWithTrue(Func<bool> action);

        /// <summary>
        /// 执行事务,失败时自动回滚,有异常时回滚后抛出
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ExecuteAsyncTranWithTrue(Func<Task<bool>> action);

        /// <summary>
        /// 执行事务，有异常时回滚后抛出
        /// </summary>
        /// <param name="action"></param>
        void ExecuteTran(Action action);

        /// <summary>
        /// 执行事务，有异常时回滚后抛出
        /// </summary>
        /// <param name="action"></param>
        void ExecuteAsyncTran(Func<Task> action);

        /// <summary>
        /// 执行事务，有异常时回滚后抛出
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        T ExecuteTran<T>(Func<T> action);

        /// <summary>
        /// 执行事务，有异常时回滚后抛出
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        T ExecuteAsyncTran<T>(Func<Task<T>> action);
    }
}