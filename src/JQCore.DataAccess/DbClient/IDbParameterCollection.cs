using System.Collections.Generic;
using System.Data;

namespace JQCore.DataAccess.DbClient
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IDbParameterCollection.cs
    /// 类属性：局部类（非静态）
    /// 类功能描述：参数集合
    /// 创建标识：yjq 2017/9/5 19:23:14
    /// </summary>
    internal interface IDbParameterCollection
    {
        /// <summary>
        /// 参数值（用于和数据库交互）
        /// </summary>
        object ParaItems { get; }

        /// <summary>
        /// 获取所有参数的名字
        /// </summary>
        IEnumerable<string> ParameterNames { get; }

        /// <summary>
        /// 获取当前参数信息列表(不用于和数据库交互)
        /// </summary>
        IEnumerable<DbParameterInfo> ParameterList { get; }

        /// <summary>
        /// 将对象添加到参数中（不支持数组和ParameterInfo），支持一般对象
        /// </summary>
        /// <param name="param">对象值</param>
        /// <param name="prefix">参数名字前缀</param>
        /// <param name="ignoreFields">忽略的字段</param>
        void AddObjectParam(object param, string prefix = null, string[] ignoreFields = null);

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        void AddParameter(string parameterName, object value);

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        void AddParameter(string parameterName, object value, DbType? dbType);

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="size">长度</param>
        void AddParameter(string parameterName, object value, DbType? dbType, int? size);

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="size">长度</param>
        /// <param name="direction">Dataset参数类型</param>
        /// <param name="scale">参数值的精度</param>
        void AddParameter(string parameterName, object value, DbType? dbType, int? size, ParameterDirection? direction, byte? scale = null);

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="info">参数信息</param>
        void AddParameter(DbParameterInfo info);

        /// <summary>
        /// 添加参数列表
        /// </summary>
        /// <param name="infoList">参数信息</param>
        void AddParameter(IEnumerable<DbParameterInfo> infoList);

        /// <summary>
        /// 根据名字获取值
        /// </summary>
        /// <typeparam name="T">值得类型</typeparam>
        /// <param name="name">参数名字</param>
        /// <returns></returns>
        T GetValue<T>(string name);
    }
}