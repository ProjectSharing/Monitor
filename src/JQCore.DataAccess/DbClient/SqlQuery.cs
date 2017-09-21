using System.Collections.Generic;
using System.Data;

namespace JQCore.DataAccess.DbClient
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SqlQuery.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/5 19:34:34
    /// </summary>
    public class SqlQuery
    {
        /// <summary>
        /// 默认超时
        /// </summary>
        protected const int COMMAND_TIMEOUT_DEFAULT = 30000;

        /// <summary>
        /// 文本
        /// </summary>
        protected string _commandText;

        /// <summary>
        /// 超时
        /// </summary>
        protected int _commandTimeout = 30000;

        /// <summary>
        /// 文本类型
        /// </summary>
        protected CommandType _commandType = CommandType.Text;

        /// <summary>
        /// 参数接口集合类型
        /// </summary>
        private IDbParameterCollection _parameters;

        /// <summary>
        /// .ctor
        /// </summary>
        public SqlQuery()
        {
        }

        /// <summary>
        ///.ctor
        /// </summary>
        /// <param name="commandText">执行文本</param>
        /// <param name="para">将对象添加到参数中（不支持数组和ParameterInfo），支持一般对象</param>
        public SqlQuery(string commandText, object para)
            : this()
        {
            _commandText = commandText;
            _parameters = new DbParameterCollection(para);
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="cmdParms">将对象添加到参数中（不支持数组和ParameterInfo），支持一般对象</param>
        public SqlQuery(string commandText, CommandType commandType = CommandType.Text, params object[] cmdParms)
        {
            _commandText = commandText;
            _commandType = commandType;
            if (cmdParms != null)
            {
                foreach (object parameter in cmdParms)
                {
                    Parameters.AddObjectParam(parameter);
                }
            }
        }

        /// <summary>
        /// 执行文本
        /// </summary>
        public virtual string CommandText
        {
            get
            {
                return _commandText;
            }
            set
            {
                _commandText = value;
            }
        }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                return this._commandTimeout;
            }
            set
            {
                if (value < 10)
                {
                    this._commandTimeout = 300;
                    return;
                }
                this._commandTimeout = value;
            }
        }

        /// <summary>
        /// 指定如何解释命令字符串
        /// </summary>
        public CommandType CommandType
        {
            get
            {
                return _commandType;
            }
            set
            {
                this._commandType = value;
            }
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        private IDbParameterCollection Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new DbParameterCollection();
                }
                return this._parameters;
            }
            set
            {
                this._parameters = value;
            }
        }

        /// <summary>
        /// 参数值（用于和数据库交互）
        /// </summary>
        public object ParaItems
        {
            get
            {
                return Parameters.ParaItems;
            }
        }

        /// <summary>
        /// 获取所有参数的名字
        /// </summary>
        public IEnumerable<string> ParameterNames
        {
            get
            {
                return Parameters.ParameterNames;
            }
        }

        /// <summary>
        /// 获取当前参数信息列表(不用于和数据库交互)
        /// </summary>
        public IEnumerable<DbParameterInfo> ParameterList
        {
            get
            {
                return Parameters.ParameterList;
            }
        }

        /// <summary>
        /// 更换查询文本
        /// </summary>
        /// <param name="commandText">查询文本</param>
        /// <returns></returns>
        public SqlQuery ChangeCommandText(string commandText)
        {
            CommandText = commandText;
            return this;
        }

        /// <summary>
        /// 更改超出时间
        /// </summary>
        /// <param name="timeOutValue">超出时间值</param>
        /// <returns></returns>
        public SqlQuery ChangeTimeOut(int timeOutValue)
        {
            CommandTimeout = timeOutValue;
            return this;
        }

        /// <summary>
        /// 更换指定如何解释命令字符串
        /// </summary>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public SqlQuery ChangeCommandType(CommandType cmdType)
        {
            CommandType = cmdType;
            return this;
        }

        /// <summary>
        /// 将对象添加到参数中（不支持数组和ParameterInfo），支持一般对象
        /// </summary>
        /// <param name="param">对象值</param>
        /// <param name="prefix">参数名字前缀</param>
        /// <param name="ignoreFields">忽略的字段</param>
        public SqlQuery AddObjectParam(object param, string prefix = null, string[] ignoreFields = null)
        {
            Parameters.AddObjectParam(param, prefix: prefix, ignoreFields: ignoreFields);
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        public SqlQuery AddParameter(string parameterName, object value)
        {
            Parameters.AddParameter(parameterName, value);
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        public SqlQuery AddParameter(string parameterName, object value, DbType? dbType)
        {
            Parameters.AddParameter(parameterName, value, dbType);
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="size">长度</param>
        public SqlQuery AddParameter(string parameterName, object value, DbType? dbType, int? size)
        {
            Parameters.AddParameter(parameterName, value, dbType, size);
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="size">长度</param>
        /// <param name="direction">Dataset参数类型</param>
        /// <param name="scale">参数值的精度</param>
        public SqlQuery AddParameter(string parameterName, object value, DbType? dbType, int? size, ParameterDirection? direction, byte? scale = null)
        {
            Parameters.AddParameter(parameterName, value, dbType, size, direction, scale: scale);
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="info">参数信息</param>
        public SqlQuery AddParameter(DbParameterInfo info)
        {
            Parameters.AddParameter(info);
            return this;
        }

        /// <summary>
        /// 添加参数列表
        /// </summary>
        /// <param name="infoList">参数信息</param>
        public SqlQuery AddParameter(List<DbParameterInfo> infoList)
        {
            Parameters.AddParameter(infoList);
            return this;
        }

        /// <summary>
        /// 根据名字获取值
        /// </summary>
        /// <typeparam name="T">值得类型</typeparam>
        /// <param name="name">参数名字</param>
        /// <returns></returns>
        public T GetParamValue<T>(string name)
        {
            return Parameters.GetValue<T>(name);
        }
    }
}