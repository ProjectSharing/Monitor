using JQCore.Dependency;
using JQCore.Web;
using System;
using System.Diagnostics;
using System.Threading;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AsyncLocalUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/30 20:42:24
    /// </summary>
    public static class AsyncLocalUtil
    {
        /// <summary>
        /// 获取当前的请求随机GID，同一次请求唯一
        /// </summary>
        public static string CurrentGID
        {
            get
            {
                return Activity.Current?.Id ?? WebHttpContext.Current?.TraceIdentifier??Guid.NewGuid().ToString("N");
            }
        }

        /// <summary>
        /// 使用callcontext
        /// </summary>
        /// <param name="containerManager"></param>
        /// <returns></returns>
        public static ContainerManager UseCallContext(this ContainerManager containerManager)
        {
            return containerManager.AddSingleton<ICallContextProvider, CallContextProvider>()
                              ;
        }
    }

    public interface ICallContextProvider
    {
        AsyncLocalInfo Current { get; set; }
    }

    internal class CallContextProvider : ICallContextProvider
    {
        private AsyncLocal<AsyncLocalInfo> _asyncLocal = new AsyncLocal<AsyncLocalInfo>();

        public AsyncLocalInfo Current
        {
            get
            {
                return _asyncLocal.Value = (_asyncLocal.Value ?? new AsyncLocalInfo());
            }
            set { _asyncLocal.Value = value; }
        }
    }

    public class AsyncLocalInfo
    {
        private Guid _gID;

        public Guid GID
        {
            get
            {
                if (_gID == Guid.Empty)
                {
                    _gID = Guid.NewGuid();
                }
                return _gID;
            }
        }
    }
}