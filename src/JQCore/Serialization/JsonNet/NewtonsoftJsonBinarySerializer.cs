using JQCore.Extensions;
using System.Threading.Tasks;

namespace JQCore.Serialization
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：NewtonsoftJsonBinarySerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/31 19:55:00
    /// </summary>
    public class NewtonsoftJsonBinarySerializer : IBinarySerializer
    {
        private readonly IJsonSerializer _jsonSerializer;

        public NewtonsoftJsonBinarySerializer(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serializedObject">字节数组</param>
        /// <returns>对象</returns>
        public virtual T Deserialize<T>(byte[] serializedObject)
        {
            var jsonValue = serializedObject.ToStr();
            return _jsonSerializer.Deserialize<T>(jsonValue);
        }

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serializedObject">字节数组</param>
        /// <returns>对象</returns>
        public virtual Task<T> DeserializeAsync<T>(byte[] serializedObject)
        {
            var jsonValue = serializedObject.ToStr();
            return _jsonSerializer.DeserializeAsync<T>(jsonValue);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="item">对象值</param>
        /// <returns>字节数组</returns>
        public virtual byte[] Serialize<T>(T item)
        {
            var jsonValue = _jsonSerializer.Serialize(item);
            return jsonValue.ToBytes();
        }

        /// <summary>
        /// 异步序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="item">对象值</param>
        /// <returns>字节数组</returns>
        public async virtual Task<byte[]> SerializeAsync<T>(T item)
        {
            var jsonValue = await _jsonSerializer.SerializeAsync(item);
            return jsonValue.ToBytes();
        }
    }
}