using JQCore.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace JQCore.Configuration
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ConfigurationManage.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：配置管理
    /// 创建标识：yjq 2017/9/4 16:55:57
    /// </summary>
    public class ConfigurationManage
    {
        private ConfigurationManage()
        {
        }

        /// <summary>
        /// .ctor
        /// </summary>
        static ConfigurationManage()
        {
            Instance = new ConfigurationManage();
        }

        /// <summary>
        /// 当前配置信息
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// 配置管理实例
        /// </summary>
        public static ConfigurationManage Instance { get; set; }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ConfigurationManage SetConfiguration(IConfiguration configuration)
        {
            Instance.Configuration = configuration;
            return Instance;
        }

        /// <summary>
        /// 获取配置连接的值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            return Instance.Configuration.GetConnectionString(key);
        }

        /// <summary>
        /// 获取option的值
        /// </summary>
        /// <typeparam name="TOptions">option类型</typeparam>
        /// <returns></returns>
        public static TOptions GetOption<TOptions>() where TOptions : class, new()
        {
            using (var scope = ContainerManager.BeginLeftScope())
            {
                var option = scope.Resolve<IOptions<TOptions>>();
                if (option != null)
                {
                    return option.Value;
                }
                return default(TOptions);
            }
        }

        /// <summary>
        /// 获取配置对应的值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">配置的key</param>
        /// <returns>配置对应的值</returns>
        public static T GetValue<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return default(T);
            }
            return Instance.Configuration.GetValue<T>(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T">值得类型</typeparam>
        /// <returns>配置值</returns>
        public static T Get<T>()
        {
            return Instance.Configuration.Get<T>();
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">配置的key</param>
        /// <returns>配置值</returns>
        public static string GetValue(string key)
        {
            return Instance.Configuration[key];
        }
    }
}