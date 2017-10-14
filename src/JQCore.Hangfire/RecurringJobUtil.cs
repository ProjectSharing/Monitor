using Hangfire;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JQCore.Hangfire
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RecurringJobUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：定时任务
    /// 创建标识：yjq 2017/10/14 14:56:02
    /// </summary>
    public partial class TaskScheldulingUtil
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="methodCall"></param>
        /// <param name="cronExpression"></param>
        /// <param name="timeZone"></param>
        /// <param name="queue"></param>
        public static void CreateRecurringJob(Expression<Action> methodCall, Func<string> cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recurringJobId"></param>
        /// <param name="methodCall"></param>
        /// <param name="cronExpression"></param>
        /// <param name="timeZone"></param>
        /// <param name="queue"></param>
        public static void CreateRecurringJob<T>(string recurringJobId, Expression<Func<T, Task>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="recurringJobId"></param>
        /// <param name="methodCall"></param>
        /// <param name="cronExpression"></param>
        /// <param name="timeZone"></param>
        /// <param name="queue"></param>
        public static void CreateRecurringJob(string recurringJobId, Expression<Func<Task>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recurringJobId"></param>
        /// <param name="methodCall"></param>
        /// <param name="cronExpression"></param>
        /// <param name="timeZone"></param>
        /// <param name="queue"></param>
        public static void CreateRecurringJob<T>(string recurringJobId, Expression<Func<T, Task>> methodCall, Func<string> cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="recurringJobId"></param>
        /// <param name="methodCall"></param>
        /// <param name="cronExpression"></param>
        /// <param name="timeZone"></param>
        /// <param name="queue"></param>
        public static void CreateRecurringJob(string recurringJobId, Expression<Func<Task>> methodCall, Func<string> cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodCall"></param>
        /// <param name="cronExpression"></param>
        /// <param name="timeZone"></param>
        /// <param name="queue"></param>
        public static void CreateRecurringJob<T>(Expression<Func<T, Task>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <param name="methodCall">返回类型</param>
        /// <param name="cronExpression">执行方法</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob(Expression<Func<Task>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="methodCall">执行方法</param>
        /// <param name="cronExpression">corn表达式</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob<T>(Expression<Func<T, Task>> methodCall, Func<string> cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <param name="methodCall">执行方法</param>
        /// <param name="cronExpression">corn表达式</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob(Expression<Func<Task>> methodCall, Func<string> cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="recurringJobId">任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <param name="cronExpression">corn表达式</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob<T>(string recurringJobId, Expression<Action<T>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <param name="recurringJobId">任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <param name="cronExpression">corn表达式</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob(string recurringJobId, Expression<Action> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="recurringJobId">任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <param name="cronExpression">corn表达式</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob<T>(string recurringJobId, Expression<Action<T>> methodCall, Func<string> cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <param name="recurringJobId">任务ID</param>
        /// <param name="methodCall">执行方法</param>
        /// <param name="cronExpression">corn表达式</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob(string recurringJobId, Expression<Action> methodCall, Func<string> cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="methodCall">执行方法</param>
        /// <param name="cronExpression">corn表达式</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob<T>(Expression<Action<T>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <param name="methodCall">执行方法</param>
        /// <param name="cronExpression">corn表达式</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob(Expression<Action> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="methodCall">执行方法</param>
        /// <param name="cronExpression">corn表达式</param>
        /// <param name="timeZone"></param>
        /// <param name="queue">对列名字</param>
        public static void CreateRecurringJob<T>(Expression<Action<T>> methodCall, Func<string> cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, timeZone: timeZone, queue: queue);
        }

        /// <summary>
        /// 存在定时任务时就移除
        /// </summary>
        /// <param name="recurringJobId">任务ID</param>
        public static void RemoveRecurringJobIfExists(string recurringJobId)
        {
            RecurringJob.RemoveIfExists(recurringJobId);
        }

        /// <summary>
        /// 触发定时任务
        /// </summary>
        /// <param name="recurringJobId">任务ID</param>
        public static void TriggerRecurringJob(string recurringJobId)
        {
            RecurringJob.Trigger(recurringJobId);
        }
    }
}