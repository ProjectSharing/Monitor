namespace JQCore.DataAccess.Expressions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DataMember.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/12 15:54:03
    /// </summary>
    public sealed class DataMember
    {
        public DataMember(string name, object value, DataMemberType memberType)
        {
            Name = name;
            Value = value;
            MemberType = memberType;
        }

        public string Name { get; set; }

        public object Value { get; set; }

        public DataMemberType MemberType { get; set; }
    }
}