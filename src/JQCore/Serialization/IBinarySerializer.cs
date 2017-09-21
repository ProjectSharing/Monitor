using System.Threading.Tasks;

namespace JQCore.Serialization
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IBinarySerializer.cs
    /// 接口属性：接口
    /// 类功能描述：字节序列化接口
    /// 创建标识：yjq 2017/9/4 22:09:20
    /// </summary>
    public interface IBinarySerializer
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serializedObject">字节数组</param>
        /// <returns>对象</returns>
        T Deserialize<T>(byte[] serializedObject);

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serializedObject">字节数组</param>
        /// <returns>对象</returns>
        Task<T> DeserializeAsync<T>(byte[] serializedObject);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="item">对象值</param>
        /// <returns>字节数组</returns>
		byte[] Serialize<T>(T item);

        /// <summary>
        /// 异步序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="item">对象值</param>
        /// <returns>字节数组</returns>
        Task<byte[]> SerializeAsync<T>(T item);
    }
}