using JQCore.DataAccess.DbClient;
using JQCore.DataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JQCore.DataAccess.Expressions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SqlExpressionVisitor.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/12 16:12:00
    /// </summary>
    internal sealed class SqlExpressionVisitor : BaseParamExpressionVisitor<DbParameterInfo>
    {
        private int _currentParamIndex = 0;//当前已有参数个数
        private string _paramertNamePrefix = string.Empty;
        private DatabaseType _dbType;

        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix">表明前缀 如：A.</param>
        /// <param name="paramertNamePrefix">参数名字前缀 如：W_</param>
        ///  <param name="dbType">数据库类型</param>
        public SqlExpressionVisitor(string prefix = "", string paramertNamePrefix = "", DatabaseType dbType = DatabaseType.MSSQLServer) : base(prefix: prefix, dbType: dbType)
        {
            _paramertNamePrefix = paramertNamePrefix;
            _dbType = dbType;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType
        {
            get
            {
                return _dbType;
            }
        }

        /// <summary>
        /// 根据值获取参数的方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override ValueTuple<string, DbParameterInfo> GetParameter(object value)
        {
            string parameterName = $"{SqlQueryUtil.GetSign(DatabaseType)}{_paramertNamePrefix}Parameter_{_currentParamIndex.ToString()}";
            _currentParamIndex++;
            return ValueTuple.Create(parameterName, new DbParameterInfo
            {
                Value = value,
                ParameterName = parameterName
            });
        }

        /// <summary>
        /// 获取sql条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="expression">表达式树</param>
        /// <param name="prefix">表名前缀</param>
        /// <param name="paramertNamePrefix">参数名字前缀</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static ValueTuple<string, List<DbParameterInfo>> GetSqlWhere<T>(Expression<Func<T, bool>> expression, string prefix = "", string paramertNamePrefix = "", DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var resolver = new SqlExpressionVisitor(prefix: prefix, paramertNamePrefix: paramertNamePrefix, dbType: dbType);
            return resolver.GetSqlWhere(expression.Body);
        }
    }
}