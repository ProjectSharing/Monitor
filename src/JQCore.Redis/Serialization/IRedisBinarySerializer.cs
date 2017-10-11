using System.Threading.Tasks;

namespace JQCore.Redis.Serialization
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IRedisBinarySerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Redis序列化接口接口
    /// 创建标识：yjq 2017/7/15 14:47:37
    /// </summary>
    public interface IRedisBinarySerializer
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serializedObject">字节数组</param>
        /// <returns>对象</returns>
        T Deserialize<T>(string serializedObject);

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serializedObject">字节数组</param>
        /// <returns>对象</returns>
        Task<T> DeserializeAsync<T>(string serializedObject);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="item">对象值</param>
        /// <returns>字节数组</returns>
		string Serialize<T>(T item);

        /// <summary>
        /// 异步序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="item">对象值</param>
        /// <returns>字节数组</returns>
        Task<string> SerializeAsync<T>(T item);
    }
}