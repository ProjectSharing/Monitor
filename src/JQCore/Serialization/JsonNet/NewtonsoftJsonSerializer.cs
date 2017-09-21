using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Threading.Tasks;

namespace JQCore.Serialization
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：NewtonsoftJsonSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：NewtonsoftJsonSerializer
    /// 创建标识：yjq 2017/9/4 22:01:19
    /// </summary>
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public static JsonSerializerSettings Settings { get; private set; }

        static NewtonsoftJsonSerializer()
        {
            Settings = new JsonSerializerSettings
            {
                ContractResolver = new CustomContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
        }

        /// <summary>
        /// 根据json格式字符串获取对象
        /// </summary>
        /// <typeparam name="T">需要获取的对象</typeparam>
        /// <param name="value">json格式的字符串</param>
        /// <returns></returns>
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, Settings);
        }

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="value">json格式字符串</param>
        /// <returns>反序列化对象</returns>
        public Task<T> DeserializeAsync<T>(string value)
        {
            return Task.Factory.StartNew(() => Deserialize<T>(value));
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns>json格式的字符串</returns>
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }

        /// <summary>
        /// 异步序列化
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns>json格式的字符串</returns>
        public Task<string> SerializeAsync(object obj)
        {
            return Task.Factory.StartNew(() => Serialize(obj));
        }

        private class CustomContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var jsonProperty = base.CreateProperty(member, memberSerialization);
                if (jsonProperty.Writable) return jsonProperty;
                var property = member as PropertyInfo;
                if (property == null) return jsonProperty;
                var hasPrivateSetter = property.GetSetMethod(true) != null;
                jsonProperty.Writable = hasPrivateSetter;
                return jsonProperty;
            }
        }
    }
}