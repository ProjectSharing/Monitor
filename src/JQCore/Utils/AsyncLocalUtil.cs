using JQCore.Web;
using System;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AsyncLocalUtil.cs
    /// 类属性：公共类（静态）
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
                if (WebHttpContext.Current != null)
                {
                    if (WebHttpContext.Current.TraceIdentifier == null)
                    {
                        WebHttpContext.Current.TraceIdentifier = Guid.NewGuid().ToString("N");
                    }
                    return WebHttpContext.Current.TraceIdentifier;
                }
                return Guid.NewGuid().ToString("N");
            }
        }
    }
}