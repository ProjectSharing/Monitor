using JQCore.Serialization;

namespace JQCore.MQ.Serialization
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MQJsonBinarySerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：json序列化
    /// 创建标识：yjq 2017/10/31 19:56:44
    /// </summary>
    public sealed class MQJsonBinarySerializer : NewtonsoftJsonBinarySerializer, IMQBinarySerializer
    {
        public MQJsonBinarySerializer(IJsonSerializer jsonSerializer) : base(jsonSerializer)
        {
        }
    }
}