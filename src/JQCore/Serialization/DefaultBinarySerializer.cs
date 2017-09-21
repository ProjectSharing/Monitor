using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace JQCore.Serialization
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DefaultBinarySerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：DefaultBinarySerializer
    /// 创建标识：yjq 2017/9/4 22:11:27
    /// </summary>
    public class DefaultBinarySerializer : IBinarySerializer
    {
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serializedObject">字节数组</param>
        /// <returns>对象</returns>
        public T Deserialize<T>(byte[] serializedObject)
        {
            using (var stream = new MemoryStream(serializedObject))
            {
                return (T)_binaryFormatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serializedObject">字节数组</param>
        /// <returns>对象</returns>
        public Task<T> DeserializeAsync<T>(byte[] serializedObject)
        {
            return Task.FromResult(Deserialize<T>(serializedObject));
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="item">对象值</param>
        /// <returns>字节数组</returns>
        public byte[] Serialize<T>(T item)
        {
            using (var stream = new MemoryStream())
            {
                _binaryFormatter.Serialize(stream, item);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 异步序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="item">对象值</param>
        /// <returns>字节数组</returns>
        public Task<byte[]> SerializeAsync<T>(T item)
        {
            return Task.FromResult(Serialize<T>(item));
        }
    }
}