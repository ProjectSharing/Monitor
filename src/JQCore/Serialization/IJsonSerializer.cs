using System.Threading.Tasks;

namespace JQCore.Serialization
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IJsonSerializer.cs
    /// 接口属性：json序列化接口
    /// 类功能描述：IJsonSerializer
    /// 创建标识：yjq 2017/9/4 21:58:26
    /// </summary>
    public interface IJsonSerializer
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="value">json格式字符串</param>
        /// <returns>反序列化对象</returns>
        T Deserialize<T>(string value);

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="value">json格式字符串</param>
        /// <returns>反序列化对象</returns>
        Task<T> DeserializeAsync<T>(string value);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns>json格式的字符串</returns>
        string Serialize(object obj);

        /// <summary>
        /// 异步序列化
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns>json格式的字符串</returns>
        Task<string> SerializeAsync(object obj);
    }
}