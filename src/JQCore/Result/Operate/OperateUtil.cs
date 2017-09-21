using JQCore.Extensions;
using JQCore.Utils;
using System;
using System.Collections.Concurrent;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace JQCore.Result
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：OperateUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：操作结果工具类
    /// 创建标识：yjq 2017/9/5 14:33:36
    /// </summary>
    public static class OperateUtil
    {
        /// <summary>
        /// 异常操作结果
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult Exception(Exception ex)
        {
            return new OperateResult(ex);
        }

        /// <summary>
        /// 异常操作结果
        /// </summary>
        /// <typeparam name="T">结果值类型</typeparam>
        /// <param name="ex">异常信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Exception<T>(Exception ex)
        {
            return new OperateResult<T>(ex);
        }

        /// <summary>
        /// 参数错误
        /// </summary>
        /// <param name="msg">信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult ParamError(string msg)
        {
            return new OperateResult(OperateState.ParamError, msg);
        }

        /// <summary>
        /// 参数错误
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="msg">信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> ParamError<T>(string msg)
        {
            return new OperateResult<T>(OperateState.ParamError, msg);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg">失败信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult Failed(string msg)
        {
            return new OperateResult(OperateState.Failed, msg);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="msg">失败信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Failed<T>(string msg)
        {
            return new OperateResult<T>(OperateState.Failed, msg);
        }

        /// <summary>
        /// 创建成功的操作结果
        /// </summary>
        /// <returns>操作结果</returns>
        public static OperateResult Success()
        {
            return new OperateResult(OperateState.Success);
        }

        /// <summary>
        /// 创建成功的操作结果
        /// </summary>
        /// <param name="msg">信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult Success(string msg)
        {
            return new OperateResult(OperateState.Success, msg);
        }

        /// <summary>
        /// 创建成功的操作结果
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="value">值类型</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Success<T>(T value)
        {
            return Success(value, null);
        }

        /// <summary>
        /// 创建成功的操作结果
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="value">值类型</param>
        /// <param name="msg">信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Success<T>(T value, string msg)
        {
            return new OperateResult<T>(OperateState.Success, msg)
            {
                Value = value
            };
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="executeActionAsync">方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static OperateResult Execute(Func<OperateResult> executeAction, string callMemberName = null)
        {
            try
            {
                return executeAction();
            }
            catch (BizException ex)
            {
                LogUtil.Info($"{callMemberName}:{ex.Message}");
                return ParamError(ex.Message);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: callMemberName);
                return Failed("操作失败");
            }
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="executeAction">方法</param>
        /// <param name="successMessage">成功提示信息</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static OperateResult Execute(Action executeAction, string successMessage = null, string callMemberName = null)
        {
            try
            {
                executeAction();
                return Success(successMessage);
            }
            catch (BizException ex)
            {
                LogUtil.Info($"{callMemberName}:{ex.Message}");
                return ParamError(ex.Message);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: callMemberName);
                return Failed("操作失败");
            }
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <param name="executeActionAsync">异步方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static async Task<OperateResult> ExecuteAsync(Func<Task<OperateResult>> executeActionAsync, string callMemberName = null)
        {
            try
            {
                return await executeActionAsync();
            }
            catch (BizException ex)
            {
                LogUtil.Info($"{callMemberName}:{ex.Message}");
                return ParamError(ex.Message);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: callMemberName);
                return Failed("操作失败");
            }
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <param name="executeActionAsync">异步方法</param>
        /// <param name="successMessage">成功提示信息</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static async Task<OperateResult> ExecuteAsync(Func<Task> executeActionAsync, string successMessage = null, string callMemberName = null)
        {
            try
            {
                await executeActionAsync();
                return Success(successMessage);
            }
            catch (BizException ex)
            {
                LogUtil.Info($"{callMemberName}:{ex.Message}");
                return ParamError(ex.Message);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: callMemberName);
                return Failed("操作失败");
            }
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="executeAction">执行方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Execute<T>(Func<OperateResult<T>> executeAction, string callMemberName = null)
        {
            try
            {
                return executeAction();
            }
            catch (BizException ex)
            {
                LogUtil.Info($"{callMemberName}:{ex.Message}");
                return ParamError<T>(ex.Message);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: callMemberName);
                return Failed<T>("操作失败");
            }
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="executeAction">执行方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Execute<T>(Func<T> executeAction, string callMemberName = null)
        {
            try
            {
                var value = executeAction();
                return Success(value);
            }
            catch (BizException ex)
            {
                LogUtil.Info($"{callMemberName}:{ex.Message}");
                return ParamError<T>(ex.Message);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: callMemberName);
                return Failed<T>("操作失败");
            }
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="executeAction">异步方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static async Task<OperateResult<T>> ExecuteAsync<T>(Func<Task<OperateResult<T>>> executeActionAsync, string callMemberName = null)
        {
            try
            {
                return await executeActionAsync();
            }
            catch (BizException ex)
            {
                LogUtil.Info($"{callMemberName}:{ex.Message}");
                return ParamError<T>(ex.Message);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: callMemberName);
                return Failed<T>("操作失败");
            }
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="executeAction">异步方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static async Task<OperateResult<T>> ExecuteAsync<T>(Func<Task<T>> executeActionAsync, string callMemberName = null)
        {
            try
            {
                var value = await executeActionAsync();
                return Success(value);
            }
            catch (BizException ex)
            {
                LogUtil.Info($"{callMemberName}:{ex.Message}");
                return ParamError<T>(ex.Message);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: callMemberName);
                return Failed<T>("操作失败");
            }
        }

        #region Emit方法创建OperateResult

        /// <summary>
        /// 创建OperateResult的Emit方法缓存
        /// </summary>
        private static ConcurrentDictionary<RuntimeTypeHandle, DynamicMethod> _CreateOperateResultCache = new ConcurrentDictionary<RuntimeTypeHandle, DynamicMethod>();

        /// <summary>
        /// 创建OperateResult的Emit方法
        /// </summary>
        /// <param name="operateType">操作结果类型</param>
        /// <param name="operateState">操作结果状态</param>
        /// <param name="msg">操作结果信息</param>
        /// <returns>OperateResult</returns>
        public static OperateResult EmitCreate(Type operateType, OperateState operateState, string msg)
        {
            var returnType = operateType;
            if (operateType.BaseType == typeof(Task))
            {
                returnType = operateType.GenericTypeArguments[0];
            }
            var method = _CreateOperateResultCache.GetValue(returnType.TypeHandle, () =>
            {
                return EmitMethodCreate(returnType);
            });
            var fuc = (Func<OperateState, string, OperateResult>)method.CreateDelegate(typeof(Func<OperateState, string, OperateResult>));
            return fuc(operateState, msg);
        }

        /// <summary>
        /// 根绝类型创建Emit方法
        /// </summary>
        /// <param name="operateType">操作结果类型</param>
        /// <returns>emit创建操作结果的方法</returns>
        private static DynamicMethod EmitMethodCreate(Type operateType)
        {
            DynamicMethod createMethod = new DynamicMethod("CreateOperateResult" + operateType.Name, operateType, new Type[] { typeof(OperateState), typeof(string) }, true);
            ILGenerator il = createMethod.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Newobj, operateType.GetConstructor(new Type[] { typeof(OperateState), typeof(string) }));
            il.Emit(OpCodes.Ret);
            return createMethod;
        }

        #endregion Emit方法创建OperateResult
    }
}