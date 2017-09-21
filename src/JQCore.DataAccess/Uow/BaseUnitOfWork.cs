using JQCore.DataAccess.DbClient;
using JQCore.Utils;
using System;
using System.Data;
using System.Threading.Tasks;

namespace JQCore.DataAccess.Uow
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BaseUnitOfWork.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：工作单元基础实现类
    /// 创建标识：yjq 2017/9/5 20:42:26
    /// </summary>
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        private readonly IDataAccessFactory _dataAccessFactory;
        private readonly string _configName;
        private IDataAccess _dataAccess;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="dataAccessFactory"></param>
        /// <param name="configName"></param>
        public BaseUnitOfWork(IDataAccessFactory dataAccessFactory, string configName)
        {
            EnsureUtil.NotNullAndNotEmpty(configName, "configName");
            _dataAccessFactory = dataAccessFactory;
            _configName = configName;
        }

        /// <summary>
        /// 数据库访问接口
        /// </summary>
        protected IDataAccess DataAccess
        {
            get
            {
                return _dataAccess ?? (_dataAccess = _dataAccessFactory.GetDataAccess(_configName));
            }
        }

        /// <summary>
        /// 开始
        /// </summary>
        public virtual void BeginTran()
        {
            DataAccess.BeginTran();
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="il">事务隔离级别</param>
        public virtual void BeginTran(IsolationLevel il)
        {
            DataAccess.BeginTran(il);
        }

        /// <summary>
        /// 提交
        /// </summary>
        public virtual bool CommitTran(bool isAutoRollback = false)
        {
            try
            {
                DataAccess.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                if (isAutoRollback)
                {
                    Rollback();
                    LogUtil.Error(ex, memberName: "BaseUnitOfWork-Commit");
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public virtual void Rollback()
        {
            DataAccess.RollbackTran();
        }

        /// <summary>
        /// 执行事务,失败时自动回滚,有异常时回滚后抛出
        /// </summary>
        /// <param name="action"></param>
        public virtual bool ExecuteTranWithTrue(Func<bool> action)
        {
            try
            {
                BeginTran();
                var isSuccess = action();
                if (isSuccess)
                {
                    CommitTran();
                }
                else
                {
                    Rollback();
                }
                return isSuccess;
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        /// <summary>
        /// 执行事务,失败时自动回滚,有异常时回滚后抛出
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual bool ExecuteAsyncTranWithTrue(Func<Task<bool>> action)
        {
            try
            {
                BeginTran();
                var task = action();
                task.Wait();
                if (task.Result)
                {
                    CommitTran();
                }
                else
                {
                    Rollback();
                }
                return task.Result;
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        /// <summary>
        /// 执行事务，有异常时回滚后抛出
        /// </summary>
        /// <param name="action"></param>
        public virtual void ExecuteTran(Action action)
        {
            try
            {
                BeginTran();
                action();
                CommitTran();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        /// <summary>
        /// 执行事务，有异常时回滚后抛出
        /// </summary>
        /// <param name="action"></param>
        public virtual void ExecuteAsyncTran(Func<Task> action)
        {
            try
            {
                BeginTran();
                var task = action();
                task.Wait();
                CommitTran();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        /// <summary>
        /// 执行事务，有异常时回滚后抛出
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual T ExecuteTran<T>(Func<T> action)
        {
            try
            {
                BeginTran();
                var result = action();
                CommitTran();
                return result;
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        /// <summary>
        /// 执行事务，有异常时回滚后抛出
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual T ExecuteAsyncTran<T>(Func<Task<T>> action)
        {
            try
            {
                BeginTran();
                var task = action();
                task.Wait();
                CommitTran();
                return task.Result;
            }
            catch
            {
                Rollback();
                throw;
            }
        }
    }
}