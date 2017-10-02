using JQCore.DataAccess.DbClient;
using JQCore.DataAccess.Expressions;
using JQCore.Extensions;
using JQCore.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JQCore.DataAccess.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SqlQueryUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：SQL拼接工具
    /// 创建标识：yjq 2017/9/5 20:13:36
    /// </summary>
    public static class SqlQueryUtil
    {
        /// <summary>
        /// 拼接单个插入的SqlQuery
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体值</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>单个插入的SqlQuery</returns>
        public static SqlQuery BuilderInsertOneSqlQuery<T>(T entity, string tableName, string keyName = null, string[] ignoreFields = null, bool isIdentity = true, DatabaseType dbType = DatabaseType.MSSQLServer) where T : new()
        {
            var propertyList = PropertyUtil.GetPropertyInfos(entity, ignoreFields);
            var proNameList = propertyList.Select(m => m.Name);
            var columns = string.Join(",", proNameList);
            var values = string.Join(",", proNameList.Select(p => GetSign(dbType) + p));
            string commandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2}){3};", tableName, columns, values, isIdentity ? GetIdentityKeyScript(keyName, dbType) : string.Empty);
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.CommandText = commandText;
            sqlQuery.AddObjectParam(entity, ignoreFields: ignoreFields);
            return sqlQuery;
        }

        /// <summary>
        /// 拼接批量插入的SqlQuery
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <exception cref="ArgumentNullException">entityList</exception>
        /// <param name="entityList">实体列表，不能为Null或者为空列表</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>批量插入的SqlQuery</returns>
        public static SqlQuery BuilderInsertManySqlQuery<T>(List<T> entityList, string tableName, string keyName = null, string[] ignoreFields = null, bool isIdentity = true, DatabaseType dbType = DatabaseType.MSSQLServer) where T : new()
        {
            EnsureUtil.NotNull(entityList, "entityList");
            var entity = entityList.FirstOrDefault();
            if (entity == null)
            {
                throw new ArgumentNullException("entityList");
            }
            var propertyList = PropertyUtil.GetPropertyInfos(entity, ignoreFields);
            var proNameList = propertyList.Select(m => m.Name);
            var columns = string.Join(",", proNameList);
            StringBuilder builder = new StringBuilder();
            SqlQuery sqlQuery = new SqlQuery();
            for (int i = 0; i < entityList.Count; i++)
            {
                var values = string.Join(",", proNameList.Select(p => GetSign(dbType) + i.ToString() + p));
                builder.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2}){3};", tableName, columns, values, isIdentity ? GetIdentityKeyScript(keyName, dbType) : string.Empty);
                sqlQuery.AddObjectParam(entityList[i], i.ToString(), ignoreFields: ignoreFields);
            }
            sqlQuery.CommandText = builder.ToString();
            return sqlQuery;
        }

        /// <summary>
        /// 拼接修改的SqlQuery
        /// </summary>
        /// <param name="data">要修改的内容</param>
        /// <param name="condition">修改的条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>修改的SqlQuery</returns>
        public static SqlQuery BuilderUpdateSqlQuery(object data, object condition, string tableName, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var updatePropertyInfos = PropertyUtil.GetPropertyInfos(data, ignoreFields);
            var wherePropertyInfos = PropertyUtil.GetPropertyInfos(condition);

            var updateProperties = updatePropertyInfos.Select(p => p.Name);
            var whereProperties = wherePropertyInfos.Select(p => p.Name);

            var updateFields = string.Join(",", updateProperties.Select(p => p + " =" + GetSign(dbType) + p));
            var whereFields = string.Empty;

            if (whereProperties.Any())
            {
                whereFields = " WHERE " + string.Join(" AND ", whereProperties.Select(p => p + " =" + GetSign(dbType) + "W_" + p));
            }
            SqlQuery sqlQuery = new SqlQuery()
                                .AddObjectParam(data, ignoreFields: ignoreFields)
                                .AddObjectParam(condition, "W_")
                                .ChangeCommandText(string.Format("UPDATE {0} SET {1}{2}", tableName, updateFields, whereFields));
            return sqlQuery;
        }

        /// <summary>
        /// 拼接修改的SqlQuery
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">要修改的内容</param>
        /// <param name="condition">修改的条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>修改的SqlQuery</returns>
        public static SqlQuery BuilderUpdateSqlQuery<T>(object data, Expression<Func<T, bool>> condition, string tableName, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var updatePropertyInfos = PropertyUtil.GetPropertyInfos(data, ignoreFields);
            var updateProperties = updatePropertyInfos.Select(p => p.Name);

            var updateFields = string.Join(",", updateProperties.Select(p => p + " =" + GetSign(dbType) + p));
            var whereInfo = SqlExpressionVisitor.GetSqlWhere(condition, paramertNamePrefix: "W_", dbType: dbType);
            var whereFields = string.Empty;
            if (whereInfo.Item1.IsNotNullAndNotEmptyWhiteSpace())
            {
                whereFields = $" WHERE {whereInfo.Item1}";
            }
            SqlQuery sqlQuery = new SqlQuery()
                               .AddObjectParam(data, ignoreFields: ignoreFields)
                               .AddObjectParam(whereInfo.Item2)
                               .ChangeCommandText(string.Format("UPDATE {0} SET {1}{2}", tableName, updateFields, whereFields));
            return sqlQuery;
        }

        /// <summary>
        /// 拼接删除的SqlQuery
        /// </summary>
        /// <param name="condition">删除的条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>删除的SqlQuery</returns>
        public static SqlQuery BuilderDeleteSqlQuery(object condition, string tableName, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereFields = string.Empty;
            var whereProperties = PropertyUtil.GetPropertyInfos(condition);
            var whereFieldNames = whereProperties.Select(p => p.Name);
            if (whereFieldNames.Any())
            {
                whereFields = " WHERE " + string.Join(" AND ", whereFieldNames.Select(p => p + " = " + GetSign(dbType) + p));
            }
            var sql = string.Format("DELETE FROM {0}{1};", tableName, whereFields);
            return new SqlQuery(sql, condition);
        }

        /// <summary>
        /// 拼接删除的SqlQuery
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">条件表达式树</param>
        /// <param name="tableName">表名</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>删除的SqlQuery</returns>
        public static SqlQuery BuilderDeleteSqlQuery<T>(Expression<Func<T, bool>> condition, string tableName, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereInfo = SqlExpressionVisitor.GetSqlWhere(condition, dbType: dbType);
            var whereFields = string.Empty;
            if (whereInfo.Item1.IsNotNullAndNotEmptyWhiteSpace())
            {
                whereFields = $" WHERE {whereInfo.Item1}";
            }
            var sql = $"DELETE FROM {tableName}{whereFields};";
            return new SqlQuery(sql, whereInfo.Item2);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="topCount">查询数量</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQueryTopSqlQuery(object condition, string tableName, int topCount = 1, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            return BuilderQueryTopSqlQuery(condition, tableName, string.Empty, topCount: topCount, dbType: dbType);
        }

        /// <summary>
        /// 拼接查询前几条的SqlQuery
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="order">排序字段</param>
        /// <param name="topCount">查询数量</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQueryTopSqlQuery(object condition, string tableName, string order, int topCount = 1, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereFields = string.Empty;
            var whereProperties = PropertyUtil.GetPropertyInfos(condition);
            var whereFieldNames = whereProperties.Select(p => p.Name);
            if (whereFieldNames.Any())
            {
                whereFields = " WHERE " + string.Join(" AND ", whereFieldNames.Select(p => p + " = " + GetSign(dbType) + p));
            }
            var sql = string.Format("SELECT TOP {0} * FROM {1}{2} {3};", topCount.ToString(), tableName, whereFields, string.IsNullOrWhiteSpace(order) ? string.Empty : "ORDER BY " + order);
            return new SqlQuery(sql, condition);
        }

        /// <summary>
        /// 拼接查询前几条的SqlQuery
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="topCount">查询数量</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQueryTopSqlQuery<T>(Expression<Func<T, bool>> condition, string tableName, int topCount = 1, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            return BuilderQueryTopSqlQuery(condition, tableName, string.Empty, topCount: topCount, dbType: dbType);
        }

        /// <summary>
        /// 拼接查询前几条的SqlQuery
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="order">排序字段</param>
        /// <param name="topCount">查询数量</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQueryTopSqlQuery<T>(Expression<Func<T, bool>> condition, string tableName, string order, int topCount = 1, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereInfo = SqlExpressionVisitor.GetSqlWhere(condition, dbType: dbType);
            var whereFields = string.Empty;
            if (whereInfo.Item1.IsNotNullAndNotEmptyWhiteSpace())
            {
                whereFields = $" WHERE {whereInfo.Item1}";
            }
            var sql = string.Format("SELECT TOP {0} * FROM {1}{2} {3};", topCount.ToString(), tableName, whereFields, string.IsNullOrWhiteSpace(order) ? string.Empty : "ORDER BY " + order);
            return new SqlQuery(sql, whereInfo.Item2);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQuerySqlQuery(object condition, string tableName, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            return BuilderQuerySqlQuery(condition, tableName, string.Empty, dbType: dbType);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="order">排序字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQuerySqlQuery(object condition, string tableName, string order, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereFields = string.Empty;
            var whereProperties = PropertyUtil.GetPropertyInfos(condition);
            var whereFieldNames = whereProperties.Select(p => p.Name);
            if (whereFieldNames.Any())
            {
                whereFields = " WHERE " + string.Join(" AND ", whereFieldNames.Select(p => p + " = " + GetSign(dbType) + p));
            }
            var sql = string.Format("SELECT * FROM {0}{1} {2};", tableName, whereFields, string.IsNullOrWhiteSpace(order) ? string.Empty : "ORDER BY " + order);
            return new SqlQuery(sql, condition);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQuerySqlQuery<T>(Expression<Func<T, bool>> condition, string tableName, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            return BuilderQuerySqlQuery(condition, tableName, string.Empty, dbType: dbType);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="order">排序字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQuerySqlQuery<T>(Expression<Func<T, bool>> condition, string tableName, string order, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereInfo = SqlExpressionVisitor.GetSqlWhere(condition, dbType: dbType);
            var whereFields = string.Empty;
            if (whereInfo.Item1.IsNotNullAndNotEmptyWhiteSpace())
            {
                whereFields = $" WHERE {whereInfo.Item1}";
            }
            var sql = string.Format("SELECT * FROM {0}{1} {2};", tableName, whereFields, string.IsNullOrWhiteSpace(order) ? string.Empty : "ORDER BY " + order);
            return new SqlQuery(sql, whereInfo.Item2);
        }

        /// <summary>
        /// 拼接查询前几条的SqlQuery
        /// </summary>
        /// <typeparam name="TModel">查询字段的类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="topCount">查询数量</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQueryTopSqlQuery<TModel>(object condition, string tableName, int topCount = 1, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            return BuilderQueryTopSqlQuery<TModel>(condition, tableName, string.Empty, topCount: topCount, ignoreFields: ignoreFields, dbType: dbType);
        }

        /// <summary>
        /// 拼接查询前几条的SqlQuery
        /// </summary>
        /// <typeparam name="TModel">查询字段的类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="order">排序字段</param>
        /// <param name="topCount">查询数量</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns></returns>
        public static SqlQuery BuilderQueryTopSqlQuery<TModel>(object condition, string tableName, string order, int topCount = 1, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereFields = string.Empty;
            var selectProperties = PropertyUtil.GetTypeProperties(typeof(TModel), ignoreFields);
            var whereProperties = PropertyUtil.GetPropertyInfos(condition);
            var whereFieldNames = whereProperties.Select(p => p.Name);
            if (whereFieldNames.Any())
            {
                whereFields = " WHERE " + string.Join(" AND ", whereFieldNames.Select(p => p + " = " + GetSign(dbType) + p));
            }
            var sql = string.Format("SELECT TOP {0} {1} FROM {2}{3} {4};", topCount.ToString(), string.Join(",", selectProperties.Select(m => m.Name)), tableName, whereFields, string.IsNullOrWhiteSpace(order) ? string.Empty : "ORDER BY " + order);
            return new SqlQuery(sql, condition);
        }

        /// <summary>
        /// 拼接查询前几条的SqlQuery
        /// </summary>
        /// <typeparam name="TModel">查询字段的类型</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="topCount">查询数量</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQueryTopSqlQuery<TModel, T>(Expression<Func<T, bool>> condition, string tableName, int topCount = 1, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            return BuilderQueryTopSqlQuery<TModel,T>(condition, tableName, string.Empty, topCount: topCount, ignoreFields: ignoreFields, dbType: dbType);
        }

        /// <summary>
        /// 拼接查询前几条的SqlQuery
        /// </summary>
        /// <typeparam name="TModel">查询字段的类型</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="order">排序字段</param>
        /// <param name="topCount">查询数量</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns></returns>
        public static SqlQuery BuilderQueryTopSqlQuery<TModel, T>(Expression<Func<T, bool>> condition, string tableName, string order, int topCount = 1, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereInfo = SqlExpressionVisitor.GetSqlWhere(condition, dbType: dbType);
            var whereFields = string.Empty;
            if (whereInfo.Item1.IsNotNullAndNotEmptyWhiteSpace())
            {
                whereFields = $" WHERE {whereInfo.Item1}";
            }
            var selectProperties = PropertyUtil.GetTypeProperties(typeof(TModel), ignoreFields);
            var sql = string.Format("SELECT TOP {0} {1} FROM {2}{3} {4};", topCount.ToString(), string.Join(",", selectProperties.Select(m => m.Name)), tableName, whereFields, string.IsNullOrWhiteSpace(order) ? string.Empty : "ORDER BY " + order);
            return new SqlQuery(sql, whereInfo.Item2);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <typeparam name="TModel">查询字段的类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQuerySqlQuery<TModel>(object condition, string tableName, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            return BuilderQuerySqlQuery<TModel>(condition, tableName, string.Empty, ignoreFields: ignoreFields, dbType: dbType);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <typeparam name="TModel">查询字段的类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="order">排序字段</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQuerySqlQuery<TModel>(object condition, string tableName, string order, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereFields = string.Empty;
            var selectProperties = PropertyUtil.GetTypeProperties(typeof(TModel), ignoreFields);
            var whereProperties = PropertyUtil.GetPropertyInfos(condition);
            var whereFieldNames = whereProperties.Select(p => p.Name);
            if (whereFieldNames.Any())
            {
                whereFields = " WHERE " + string.Join(" AND ", whereFieldNames.Select(p => p + " = " + GetSign(dbType) + p));
            }
            var sql = string.Format("SELECT {0} FROM {1}{2} {3};", string.Join(",", selectProperties.Select(m => m.Name)), tableName, whereFields, string.IsNullOrWhiteSpace(order) ? string.Empty : "ORDER BY " + order);
            return new SqlQuery(sql, condition);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <typeparam name="TModel">查询字段的类型</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQuerySqlQuery<TModel, T>(Expression<Func<T, bool>> condition, string tableName, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            return BuilderQuerySqlQuery<TModel,T>(condition, tableName, string.Empty, ignoreFields: ignoreFields, dbType: dbType);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <typeparam name="TModel">查询字段的类型</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="order">排序字段</param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQuerySqlQuery<TModel, T>(Expression<Func<T, bool>> condition, string tableName, string order, string[] ignoreFields = null, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereInfo = SqlExpressionVisitor.GetSqlWhere(condition, dbType: dbType);
            var whereFields = string.Empty;
            if (whereInfo.Item1.IsNotNullAndNotEmptyWhiteSpace())
            {
                whereFields = $" WHERE {whereInfo.Item1}";
            }
            var selectProperties = PropertyUtil.GetTypeProperties(typeof(TModel), ignoreFields);
            var sql = string.Format("SELECT {0} FROM {1}{2} {3};", string.Join(",", selectProperties.Select(m => m.Name)), tableName, whereFields, string.IsNullOrWhiteSpace(order) ? string.Empty : "ORDER BY " + order);
            return new SqlQuery(sql, whereInfo.Item2);
        }

        /// <summary>
        /// 拼接查询数量的SqlQuery
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询数量的SqlQuery</returns>
        public static SqlQuery BuilderQueryCountSqlQuery(object condition, string tableName, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereFields = string.Empty;
            var whereProperties = PropertyUtil.GetPropertyInfos(condition);
            var whereFieldNames = whereProperties.Select(p => p.Name);
            if (whereFieldNames.Any())
            {
                whereFields = " WHERE " + string.Join(" AND ", whereFieldNames.Select(p => p + " = " + GetSign(dbType) + p));
            }
            var sql = string.Format("SELECT COUNT(0) FROM {0}{1};", tableName, whereFields);
            return new SqlQuery(sql, condition);
        }

        /// <summary>
        /// 拼接查询数量的SqlQuery
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <returns>查询数量的SqlQuery</returns>
        public static SqlQuery BuilderQueryCountSqlQuery<T>(Expression<Func<T, bool>> condition, string tableName, DatabaseType dbType = DatabaseType.MSSQLServer)
        {
            var whereInfo = SqlExpressionVisitor.GetSqlWhere(condition, dbType: dbType);
            var whereFields = string.Empty;
            if (whereInfo.Item1.IsNotNullAndNotEmptyWhiteSpace())
            {
                whereFields = $" WHERE {whereInfo.Item1}";
            }
            var sql = string.Format("SELECT COUNT(0) FROM {0}{1};", tableName, whereFields);
            return new SqlQuery(sql, whereInfo.Item2);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <param name="selectColumn">查询列</param>
        /// <param name="selectTable">查询表</param>
        /// <param name="where">查询条件</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQueryListSqlQuery(string selectColumn, string selectTable, string where, object cmdParms = null)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("SELECT {0} FROM {1} ", selectColumn, selectTable)
                      .AppendFormatIf(where.IsNotNullAndNotEmptyWhiteSpace(), " WHERE {0}", where)
                      ;
            return new SqlQuery(sqlBuilder.ToString(), cmdParms);
        }

        /// <summary>
        /// 拼接查询的SqlQuery
        /// </summary>
        /// <param name="selectColumn">查询列</param>
        /// <param name="selectTable">查询表</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序信息</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>查询的SqlQuery</returns>
        public static SqlQuery BuilderQueryListSqlQuery(string selectColumn, string selectTable, string where, string order, object cmdParms = null)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("SELECT {0} FROM {1} ", selectColumn, selectTable)
                      .AppendFormatIf(where.IsNotNullAndNotEmptyWhiteSpace(), " WHERE {0}", where)
                      .AppendFormatIf(order.IsNotNullAndNotEmptyWhiteSpace(), " ORDER BY {0}", order)
                      ;
            return new SqlQuery(sqlBuilder.ToString(), cmdParms);
        }

        /// <summary>
        /// 拼接分页的SQL语句
        /// </summary>
        /// <param name="selectColumn">需要查询的指定字段(多个之间用逗号隔开)</param>
        /// <param name="selectTable">需要查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">一页显示条目</param>
        /// <param name="dbType">数据库类型，默认MSSQLServer</param>
        /// <param name="cmdParms">条件值</param>
        /// <returns>分页查询结果</returns>
        public static SqlQuery BuilderQueryPageSqlQuery(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, DatabaseType dbType = DatabaseType.MSSQLServer, object cmdParms = null)
        {
            SqlQuery query = new SqlQuery();
            string sql = string.Empty;//select语句
            if (pageIndex == 1)
            {
                switch (dbType)
                {
                    case DatabaseType.PostgreSqlClient:
                        sql = string.Format(@"SELECT {0} FROM {1} {2} ORDER BY {3} limit @NUM", string.IsNullOrWhiteSpace(selectColumn) ? "*" : selectColumn, selectTable, string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format(" WHERE {0} ", where), order);
                        break;

                    default:
                        sql = string.Format(@"SELECT TOP(@NUM) {0} FROM {1} {2} ORDER BY {3}", string.IsNullOrWhiteSpace(selectColumn) ? "*" : selectColumn, selectTable, string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format(" WHERE {0} ", where), order);
                        break;
                }
                query.AddParameter("@NUM", pageSize.ToString(), DbType.Int32, 4);
            }
            else
            {
                switch (dbType)
                {
                    case DatabaseType.PostgreSqlClient:
                        sql = string.Format(@"SELECT * FROM ( SELECT {0},row_number() over(ORDER BY {3}) as [num] FROM {1} {2} ) as [tab] WHERE NUM BETWEEN @NumStart and @NumEnd", string.IsNullOrWhiteSpace(selectColumn) ? "*" : selectColumn, selectTable, string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format(" WHERE {0} ", where), order);
                        break;

                    default:
                        sql = string.Format(@"SELECT * FROM ( SELECT {0},row_number() over(ORDER BY {3}) as [num] FROM {1} {2} ) as [tab] WHERE NUM BETWEEN @NumStart and @NumEnd", string.IsNullOrWhiteSpace(selectColumn) ? "*" : selectColumn, selectTable, string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format(" WHERE {0} ", where), order);
                        break;
                }
                query.AddParameter("@NumStart", ((pageIndex - 1) * pageSize + 1), DbType.Int32, 4);
                query.AddParameter("@NumEnd", (pageIndex * pageSize).ToString(), DbType.Int32, 4);
            }
            if (cmdParms != null)
            {
                query.AddObjectParam(cmdParms);
            }
            query.CommandText = sql;
            query.CommandType = CommandType.Text;
            return query;
        }

        /// <summary>
        /// 获取参数符号
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>参数符号</returns>
        public static string GetSign(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.MSSQLServer:
                    return "@";

                case DatabaseType.MySql:
                    return "?";

                case DatabaseType.Oracle:
                    return ":";

                case DatabaseType.PostgreSqlClient:
                    return "@";

                default:
                    throw new NotSupportedException(dbType.ToString());
            }
        }

        /// <summary>
        /// 获取自增的脚本语句
        /// </summary>
        /// <param name="keyName">主键名</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>获取自增的脚本语句</returns>
        public static string GetIdentityKeyScript(string keyName, DatabaseType dbType)
        {
            string identityScript = string.Empty;
            switch (dbType)
            {
                case DatabaseType.MySql:
                case DatabaseType.MSSQLServer:
                    identityScript = " SELECT scope_identity() ";
                    break;

                case DatabaseType.PostgreSqlClient:
                    identityScript = string.Concat(" RETURNING ", keyName, " ");
                    break;
            }
            return identityScript;
        }

        /// <summary>
        /// 脏读
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static string WithNoLock(this string tableName, DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.MSSQLServer:
                    return tableName + " WITH(NOLOCK) ";

                default:
                    return tableName;
            }
        }
    }
}