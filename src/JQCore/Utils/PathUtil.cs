using JQCore.Dependency;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：PathUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：路径工具类
    /// 创建标识：yjq 2017/9/13 17:33:08
    /// </summary>
    public static class PathUtil
    {
        /// <summary>
        /// 获取当前项目的基础路径
        /// </summary>
        /// <returns>当前项目的基础路径</returns>
        public static string GetCurrentDirectory()
        {
            var host = ContainerManager.Resolve<IHostingEnvironment>();
            return host.ContentRootPath;
        }

        /// <summary>
        /// 拼接多个路径
        /// </summary>
        /// <param name="paths">路径列表</param>
        /// <returns>路径</returns>
        public static string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        /// <summary>
        /// 组合路径
        /// </summary>
        /// <param name="path">基础路径</param>
        /// <param name="paths">要组合的路径</param>
        /// <returns>路径</returns>
        public static string CombinePath(this string path, params string[] paths)
        {
            if (paths != null && paths.Any())
            {
                foreach (var item in paths)
                {
                    path = Path.Combine(path, item);
                }
            }
            return path;
        }
    }
}