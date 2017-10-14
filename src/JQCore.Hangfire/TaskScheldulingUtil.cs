using Hangfire;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JQCore.Hangfire
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：TaskScheldulingUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：任务调度攻击类
    /// 创建标识：yjq 2017/10/14 13:05:49
    /// </summary>
    public partial class TaskScheldulingUtil
    {
        /// <summary>
        /// 添加一个后台执行任务
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="methodCall">执行方法</param>
        /// <returns>任务ID</returns>
        public static string BackGroundJob<T>(Expression<Func<T, Task>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        /// <summary>
        /// 添加一个后台执行任务
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="methodCall">执行方法</param>
        /// <returns>任务ID</returns>
        public static string BackGroundJob<T>(Expression<Action<T>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        /// <summary>
        /// 添加一个后台执行任务
        /// </summary>
        /// <param name="methodCall">执行方法</param>
        /// <returns>任务ID</returns>
        public static string BackGroundJob(Expression<Func<Task>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        /// <summary>
        /// 添加一个后台执行任务
        /// </summary>
        /// <param name="methodCall">执行方法</param>
        /// <returns>任务ID</returns>
        public static string BackGroundJob(Expression<Action> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        /// <summary>
        /// 添加一个延迟的任务
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="methodCall">执行方法</param>
        /// <param name="enqueueAt">执行时间</param>
        /// <returns>任务ID</returns>
        public static string DelayedJob<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt)
        {
            return BackgroundJob.Schedule(methodCall, enqueueAt);
        }

        /// <summary>
        /// 添加一个延迟的任务
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="methodCall">执行方法</param>
        /// <param name="delay">延迟时间</param>
        /// <returns>任务ID</returns>
        public static string DelayedJob<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

        /// <summary>
        /// 添加一个延迟的任务
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="methodCall">执行方法</param>
        /// <param name="delay">延迟时间</param>
        /// <returns>任务ID</returns>
        public static string DelayedJob<T>(Expression<Action<T>> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

        /// <summary>
        /// 添加一个延迟的任务
        /// </summary>
        /// <param name="methodCall">执行方法</param>
        /// <param name="enqueueAt">执行时间</param>
        /// <returns>任务ID</returns>
        public static string DelayedJob(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt)
        {
            return BackgroundJob.Schedule(methodCall, enqueueAt);
        }

        /// <summary>
        /// 添加一个延迟的任务
        /// </summary>
        /// <param name="methodCall">执行方法</param>
        /// <param name="enqueueAt">执行时间</param>
        /// <returns>任务ID</returns>
        public static string DelayedJob(Expression<Action> methodCall, DateTimeOffset enqueueAt)
        {
            return BackgroundJob.Schedule(methodCall, enqueueAt);
        }

        /// <summary>
        /// 添加一个延迟的任务
        /// </summary>
        /// <param name="methodCall">执行方法</param>
        /// <param name="delay">延迟时间</param>
        /// <returns>任务ID</returns>
        public static string DelayedJob(Expression<Func<Task>> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

        /// <summary>
        /// 添加一个延迟的任务
        /// </summary>
        /// <param name="methodCall">执行方法</param>
        /// <param name="delay">延迟时间</param>
        /// <returns>任务ID</returns>
        public static string DelayedJob(Expression<Action> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

        /// <summary>
        /// 添加一个延迟的任务
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="methodCall">执行方法</param>
        /// <param name="enqueueAt">执行时间</param>
        /// <returns>任务ID</returns>
        public static string DelayedJob<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt)
        {
            return BackgroundJob.Schedule(methodCall, enqueueAt);
        }

        /// <summary>
        /// 删除一个后台执行的任务
        /// </summary>
        /// <param name="jobId">任务ID</param>
        public static void DeleteBackGroundJob(string jobId)
        {
            BackgroundJob.Delete(jobId);
        }

        /// <summary>
        /// 删除一个后台执行的任务
        /// </summary>
        /// <param name="jobId">任务ID</param>
        /// <param name="fromState">任务状态</param>
        public static void DeleteBackGroundJob(string jobId, string fromState)
        {
            BackgroundJob.Delete(jobId, fromState);
        }

        /// <summary>
        /// 继续执行任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentId">上级任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <param name="options">配置</param>
        /// <returns>当前任务ID</returns>
        public static string ContinueWith<T>(string parentId, Expression<Func<T, Task>> methodCall, JobExecuteState options = JobExecuteState.OnlyOnSucceededState)
        {
            return BackgroundJob.ContinueWith(parentId, methodCall, options: (JobContinuationOptions)options);
        }

        /// <summary>
        /// 继续执行任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentId">上级任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <param name="options">配置</param>
        /// <returns>当前任务ID</returns>
        public static string ContinueWith<T>(string parentId, Expression<Action<T>> methodCall, JobExecuteState options)
        {
            return BackgroundJob.ContinueWith(parentId, methodCall, options: (JobContinuationOptions)options);
        }

        /// <summary>
        /// 继续执行任务
        /// </summary>
        /// <param name="parentId">上级任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <param name="options">配置</param>
        /// <returns>当前任务ID</returns>
        public static string ContinueWith(string parentId, Expression<Func<Task>> methodCall, JobExecuteState options = JobExecuteState.OnlyOnSucceededState)
        {
            return BackgroundJob.ContinueWith(parentId, methodCall, options: (JobContinuationOptions)options);
        }

        /// <summary>
        /// 继续执行任务
        /// </summary>
        /// <param name="parentId">上级任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <param name="options">配置</param>
        /// <returns>当前任务ID</returns>
        public static string ContinueWith(string parentId, Expression<Action> methodCall, JobExecuteState options)
        {
            return BackgroundJob.ContinueWith(parentId, methodCall, options: (JobContinuationOptions)options);
        }

        /// <summary>
        /// 继续执行任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentId">上级任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <returns>当前任务ID</returns>
        public static string ContinueWith<T>(string parentId, Expression<Action<T>> methodCall)
        {
            return BackgroundJob.ContinueWith(parentId, methodCall);
        }

        /// <summary>
        /// 继续执行任务
        /// </summary>
        /// <param name="parentId">上级任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <returns>当前任务ID</returns>
        public static string ContinueWith(string parentId, Expression<Action> methodCall)
        {
            return BackgroundJob.ContinueWith(parentId, methodCall);
        }

        /// <summary>
        /// 重新执行一个后台任务
        /// </summary>
        /// <param name="jobId">任务ID</param>
        /// <returns></returns>
        public static bool ReQueue(string jobId)
        {
            return BackgroundJob.Requeue(jobId);
        }

        /// <summary>
        /// 重新执行一个后台任务
        /// </summary>
        /// <param name="jobId">任务ID</param>
        /// <param name="fromState">任务状态</param>
        public static bool ReQueue(string jobId, string fromState)
        {
            return BackgroundJob.Requeue(jobId, fromState);
        }
    }
}