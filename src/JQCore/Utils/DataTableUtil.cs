using JQCore.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：DataTableUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：DataTable公共类
    /// 创建标识：yjq 2017/7/14 17:30:10
    /// </summary>
    public static class DataTableUtil
    {
        #region 将List转为DataTable

        /// <summary>
        /// 将List转为DataTable
        /// </summary>
        /// <typeparam name="T">要转换的数据类型</typeparam>
        /// <param name="list">列表信息</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isAutoIgnoreArray">是否自动忽略数组类型的属性（默认为忽略）</param>
        /// <param name="isStoreDB">是否存入数据库DateTime字段，date时间范围没事，取出展示不用设置TRUE</param>
        /// <returns>DataTable</returns>
        public static DataTable ToTable<T>(this List<T> list, string[] ignoreFields = null, bool isAutoIgnoreArray = true, bool isStoreDB = true)
        {
            var instanceType = typeof(T);
            var propertyList = PropertyUtil.GetTypeProperties(instanceType, ignoreFields);
            var fieldList = TypeUtil.GetTypeFields(instanceType);
            DataTable dt = new DataTable();
            foreach (var item in propertyList)
            {
                var propertyType = item.PropertyType;
                if ((propertyType.IsGenericType) && (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    propertyType = propertyType.GetGenericArguments()[0];
                }
                if (!(isAutoIgnoreArray && propertyType.IsArray))
                {
                    if (propertyType.IsEnum)
                    {
                        dt.Columns.Add(item.Name, typeof(int));
                    }
                    else
                        dt.Columns.Add(item.Name, propertyType); //添加列明及对应类型
                }
            }
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                foreach (var proInfo in propertyList)
                {
                    if (dt.Columns.Contains(proInfo.Name))
                    {
                        object obj = proInfo.GetValue(item);
                        if (obj == null)
                        {
                            continue;
                        }
                        if (isStoreDB && proInfo.PropertyType == typeof(DateTime) && Convert.ToDateTime(obj) < Convert.ToDateTime("1753-01-01"))
                        {
                            continue;
                        }
                        dr[proInfo.Name] = obj;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        #endregion 将List转为DataTable

        /// <summary>
        /// 判断是否包含某列名（不区分大小写）
        /// </summary>
        /// <param name="row">要判断的行</param>
        /// <param name="columnName">要判断的列名字</param>
        /// <returns>包含则返回true</returns>
        public static bool IsContainColumn(this DataRow row, string columnName)
        {
            if (row == null) return false;
            return row.Table.IsContainColumn(columnName);
        }

        /// <summary>
        /// 判断是否包含某列名（不区分大小写）
        /// </summary>
        /// <param name="table">要判断的datatable</param>
        /// <param name="columnName">要判断的列名字</param>
        /// <returns>包含则返回true</returns>
        public static bool IsContainColumn(this DataTable table, string columnName)
        {
            if (table == null) return false;
            if (string.IsNullOrWhiteSpace(columnName)) return false;
            return table.Columns.IndexOf(columnName) >= 0;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="row">要获取的DataRow</param>
        /// <param name="columnName">列名</param>
        /// <returns>值</returns>
        public static object GetValue(this DataRow row, string columnName)
        {
            if (IsContainColumn(row, columnName))
            {
                var value = row[columnName];
                if (value == DBNull.Value)
                {
                    value = null;
                }
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///将DataTable转换为标准的CSV
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns>返回标准的CSV</returns>
        public static string ToCsv(this DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else if (colum.DataType == typeof(bool))
                    {
                        if (row[colum] == null)
                        {
                            sb.Append(row[colum]);
                        }
                        else
                        {
                            if (row[colum].ToSafeBool() == true)
                            {
                                sb.Append(1);
                            }
                            else
                            {
                                sb.Append("");
                            }
                        }
                    }
                    else sb.Append(row[colum]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}