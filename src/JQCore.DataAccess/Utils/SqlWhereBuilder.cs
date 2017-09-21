using JQCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JQCore.DataAccess.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SqlWhereBuilder.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SQL拼接条件
    /// 创建标识：yjq 2017/9/5 20:10:42
    /// </summary>
    public class SqlWhereBuilder
    {
        private DatabaseType _dbType;
        private List<WhereInfo> _whereList;

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbType"></param>
        public SqlWhereBuilder(DatabaseType dbType)
        {
            _dbType = dbType;
            _whereList = new List<WhereInfo>();
        }

        /// <summary>
        ///
        /// </summary>
        public SqlWhereBuilder() : this(DatabaseType.MSSQLServer)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="where">条件</param>
        public SqlWhereBuilder(string where) : this(DatabaseType.MSSQLServer)
        {
            Append(where);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="dbType">数据库类型</param>
        public SqlWhereBuilder(string where, DatabaseType dbType) : this(dbType)
        {
            Append(where);
        }

        /// <summary>
        /// 更改数据库类型
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public SqlWhereBuilder ChangeDbType(DatabaseType dbType)
        {
            _dbType = dbType;
            return this;
        }

        /// <summary>
        /// 拼接where条件
        /// </summary>
        /// <param name="where">要拼接的内容</param>
        /// <returns></returns>
        public SqlWhereBuilder Append(string where)
        {
            AddWhereInfoIf(where.IsNotNullAndNotEmptyWhiteSpace(), where, isFormat: false);
            return this;
        }

        /// <summary>
        /// 拼接where条件
        /// </summary>
        /// <param name="format">符合格式字符串</param>
        /// <param name="args">一个对象数组</param>
        /// <returns></returns>
        public SqlWhereBuilder AppendFormat(string format, params object[] args)
        {
            AddWhereInfoIf(format.IsNotNullAndNotEmptyWhiteSpace(), string.Format(format, args), isFormat: false);
            return this;
        }

        /// <summary>
        /// 拼接相等条件语句
        /// </summary>
        /// <param name="key">要拼接的查询字段</param>
        /// <param name="obj">值</param>
        /// <param name="paramKey">参数名字(不传则默认拼接字段名)</param>
        /// <returns></returns>
        public SqlWhereBuilder AppendEqual(string key, object obj, string paramKey = null)
        {
            if (key.IsNullOrEmptyWhiteSpace())
            {
                return this;
            }
            if (obj.IsNotNullAndNotEmptyWhiteSpace())
            {
                AddWhereInfo(string.Format(" {0}={1}{2} ", key, "{0}", Clean(paramKey ?? key)), isFormat: true);
            }
            return this;
        }

        /// <summary>
        /// 拼接包含条件语句
        /// </summary>
        /// <param name="key">要拼接的查询字段</param>
        /// <param name="obj">值</param>
        /// <param name="paramKey">参数名字(不传则默认拼接字段名)</param>
        /// <returns></returns>
        public SqlWhereBuilder AppendLike(string key, object obj, string paramKey = null)
        {
            if (key.IsNullOrEmptyWhiteSpace())
            {
                return this;
            }
            if (obj.IsNotNullAndNotEmptyWhiteSpace())
            {
                AddWhereInfo(string.Format(" {0} LIKE '%'+{1}{2}+'%' ", key, "{0}", Clean(paramKey ?? key)), isFormat: true);
            }
            return this;
        }

        /// <summary>
        /// 拼接以什么开始条件语句
        /// </summary>
        /// <param name="key">要拼接的查询字段</param>
        /// <param name="obj">值</param>
        /// <param name="paramKey">参数名字(不传则默认拼接字段名)</param>
        /// <returns></returns>
        public SqlWhereBuilder AppendStartWith(string key, object obj, string paramKey = null)
        {
            if (key.IsNullOrEmptyWhiteSpace())
            {
                return this;
            }
            if (obj.IsNotNullAndNotEmptyWhiteSpace())
            {
                AddWhereInfo(string.Format(" {0} LIKE {1}{2}+'%' ", key, "{0}", Clean(paramKey ?? key)));
            }
            return this;
        }

        /// <summary>
        /// 拼接以什么结束条件语句
        /// </summary>
        /// <param name="key">要拼接的查询字段</param>
        /// <param name="obj">值</param>
        /// <param name="paramKey">参数名字(不传则默认拼接字段名)</param>
        /// <returns></returns>
        public SqlWhereBuilder AppenEndWith(string key, object obj, string paramKey = null)
        {
            if (key.IsNullOrEmptyWhiteSpace())
            {
                return this;
            }
            if (obj.IsNotNullAndNotEmptyWhiteSpace())
            {
                AddWhereInfo(string.Format(" {0} LIKE '%'+{1}{2} ", key, "{0}", Clean(paramKey ?? key)));
            }
            return this;
        }

        /// <summary>
        /// 拼接位于两者之间的条件 只传递最大值时 按小于等于计算，只传递最小值时按大于等于计算
        /// </summary>
        /// <param name="key">要拼接的查询字段</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="startParamKey">起始值参数</param>
        /// <param name="endParamKey">最大值参数</param>
        /// <returns></returns>
        public SqlWhereBuilder AppendBetween(string key, object minValue, object maxValue, string startParamKey, string endParamKey)
        {
            if (key.IsNullOrEmptyWhiteSpace())
            {
                return this;
            }
            if (minValue.IsNotNullAndNotEmptyWhiteSpace() && maxValue.IsNotNullAndNotEmptyWhiteSpace())
            {
                AddWhereInfo(string.Format(" {0} BETWEEN {1}{2} AND {1}{3} ", key, "{0}", Clean(startParamKey), Clean(endParamKey)));
            }
            else if (minValue.IsNotNullAndNotEmptyWhiteSpace())
            {
                AppendMoreThanOrEqual(key, minValue, paramKey: startParamKey);
            }
            else if (maxValue.IsNotNullAndNotEmptyWhiteSpace())
            {
                AppendLessThanOrEqual(key, maxValue, paramKey: endParamKey);
            }
            return this;
        }

        /// <summary>
        /// 拼接小于的条件
        /// </summary>
        /// <param name="key">要拼接的查询字段</param>
        /// <param name="obj">值</param>
        /// <param name="paramKey">参数名字(不传则默认拼接字段名)</param>
        /// <returns></returns>
        public SqlWhereBuilder AppendLessThan(string key, object obj, string paramKey = null)
        {
            if (key.IsNullOrEmptyWhiteSpace())
            {
                return this;
            }
            if (obj.IsNotNullAndNotEmptyWhiteSpace())
            {
                AddWhereInfo(string.Format(" {0}<{1}{2} ", key, "{0}", Clean(paramKey ?? key)));
            }
            return this;
        }

        /// <summary>
        /// 拼接大于的条件
        /// </summary>
        /// <param name="key">要拼接的查询字段</param>
        /// <param name="obj">值</param>
        /// <param name="paramKey">参数名字(不传则默认拼接字段名)</param>
        /// <returns></returns>
        public SqlWhereBuilder AppendMoreThan(string key, object obj, string paramKey = null)
        {
            if (key.IsNullOrEmptyWhiteSpace())
            {
                return this;
            }
            if (obj.IsNotNullAndNotEmptyWhiteSpace())
            {
                AddWhereInfo(string.Format(" {0}>{1}{2} ", key, "{0}", Clean(paramKey ?? key)));
            }
            return this;
        }

        /// <summary>
        /// 拼接小于等于的条件
        /// </summary>
        /// <param name="key">要拼接的查询字段</param>
        /// <param name="obj">值</param>
        /// <param name="paramKey">参数名字(不传则默认拼接字段名)</param>
        /// <returns></returns>
        public SqlWhereBuilder AppendLessThanOrEqual(string key, object obj, string paramKey = null)
        {
            if (key.IsNullOrEmptyWhiteSpace())
            {
                return this;
            }
            if (obj.IsNotNullAndNotEmptyWhiteSpace())
            {
                AddWhereInfo(string.Format(" {0}<={1}{2} ", key, "{0}", Clean(paramKey ?? key)));
            }
            return this;
        }

        /// <summary>
        /// 拼接大于等于的条件
        /// </summary>
        /// <param name="key">要拼接的查询字段</param>
        /// <param name="obj">值</param>
        /// <param name="paramKey">参数名字(不传则默认拼接字段名)</param>
        /// <returns></returns>
        public SqlWhereBuilder AppendMoreThanOrEqual(string key, object obj, string paramKey = null)
        {
            if (key.IsNullOrEmptyWhiteSpace())
            {
                return this;
            }
            if (obj.IsNotNullAndNotEmptyWhiteSpace())
            {
                AddWhereInfo(string.Format(" {0}>={1}{2} ", key, "{0}", Clean(paramKey ?? key)));
            }
            return this;
        }

        /// <summary>
        /// 清楚原来的条件
        /// </summary>
        public void Clear()
        {
            _whereList.Clear();
        }

        /// <summary>
        /// 将之前所有的条件按照and方式拼接
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_whereList.Count > 2)
            {
                StringBuilder stringBuilder = new StringBuilder();
                var index = 0;
                foreach (var item in _whereList)
                {
                    if (index == 0)
                    {
                        stringBuilder.Append(item.ToString(GetParamSign()));
                    }
                    else
                    {
                        stringBuilder.AppendFormat(" AND {0}", item.ToString(GetParamSign()));
                    }

                    index++;
                }
                return stringBuilder.ToString();
            }
            return string.Join(" AND ", _whereList.Select(m => m.ToString(GetParamSign())));
        }

        /// <summary>
        /// 获取参数符号
        /// </summary>
        /// <returns></returns>
        private string GetParamSign()
        {
            return SqlQueryUtil.GetSign(_dbType);
        }

        /// <summary>
        /// 清除符号
        /// </summary>
        /// <param name="name">要清除的字符</param>
        /// <returns>清除后的字符</returns>
        private static string Clean(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                switch (name[0])
                {
                    case '@':
                    case ':':
                    case '?':
                        return name.Substring(1);
                }
            }
            return name;
        }

        #region 添加条件信息

        /// <summary>
        /// 添加条件信息
        /// </summary>
        /// <param name="whereContent">条件内容</param>
        /// <param name="isFormat">是否为复合格式字符串</param>
        /// <returns></returns>
        private SqlWhereBuilder AddWhereInfo(string whereContent, bool isFormat = true)
        {
            _whereList.Add(new WhereInfo(whereContent, isFormat));
            return this;
        }

        /// <summary>
        /// 添加条件信息
        /// </summary>
        /// <param name="condition">判断条件</param>
        /// <param name="whereContent">条件内容</param>
        /// <param name="isFormat">是否为复合格式字符串</param>
        /// <returns></returns>
        private SqlWhereBuilder AddWhereInfoIf(bool condition, string whereContent, bool isFormat = true)
        {
            if (condition)
                _whereList.Add(new WhereInfo(whereContent, isFormat));
            return this;
        }

        /// <summary>
        /// 添加条件信息
        /// </summary>
        /// <param name="condition">判断条件</param>
        /// <param name="whereContent">条件内容</param>
        /// <param name="isFormat">是否为复合格式字符串</param>
        /// <returns></returns>
        private SqlWhereBuilder AddWhereInfoIf(Func<bool> condition, string whereContent, bool isFormat = true)
        {
            if (condition != null && condition())
                _whereList.Add(new WhereInfo(whereContent, isFormat));
            return this;
        }

        #endregion 添加条件信息
    }

    /// <summary>
    /// 条件信息
    /// </summary>
    public struct WhereInfo
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="whereContent">内容</param>
        /// <param name="isFormat"></param>
        public WhereInfo(string whereContent, bool isFormat = true)
        {
            WhereContent = whereContent;
            IsFormat = isFormat;
        }

        /// <summary>
        /// 条件内容
        /// </summary>
        public string WhereContent { get; set; }

        /// <summary>
        /// 是否为字符串文本
        /// </summary>
        public bool IsFormat { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public string ToString(string sign)
        {
            if (IsFormat)
            {
                return string.Format(WhereContent, sign);
            }
            return WhereContent;
        }
    }
}