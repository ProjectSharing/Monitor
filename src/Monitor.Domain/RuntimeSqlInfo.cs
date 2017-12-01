using JQCore.Utils;
using Monitor.Constant;
using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：RuntimeSqlInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：执行SQL信息
    /// 创建标识：template 2017-12-01 13:29:26
    /// </summary>
    public sealed class RuntimeSqlInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 所属项目
        /// </summary>
        public int FProjectID { get; set; }

        /// <summary>
        /// 项目名字
        /// </summary>
        public string FProjectName { get; set; }

        /// <summary>
        /// 部署服务器ID
        /// </summary>
        public int FServicerID { get; set; }

        /// <summary>
        /// 服务器Mac地址
        /// </summary>
        public string FServicerMac { get; set; }

        /// <summary>
        /// 数据库ID
        /// </summary>
        public int FDatabeseID { get; set; }

        /// <summary>
        /// 数据库名字
        /// </summary>
        public string FDatabaseName { get; set; }

        /// <summary>
        /// SQL数据库类型
        /// </summary>
        public string FSqlDbType { get; set; }

        /// <summary>
        /// SQL文本
        /// </summary>
        public string FSqlText { get; set; }

        /// <summary>
        /// 请求标识(同一次请求中值相同)
        /// </summary>
        public string FRequestGuid { get; set; }

        /// <summary>
        /// 调用方法名字或地址
        /// </summary>
        public string FMemberName { get; set; }

        /// <summary>
        /// 日志来源【1:前端,2:网站,3:IOS,4:Android,5:API,6:管理后台,7:其它】
        /// </summary>
        public Source FSource { get; set; }

        /// <summary>
        /// 执行消耗时间
        /// </summary>
        public double FTimeElapsed { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool FIsSuccess { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime FExecutedTime { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime FCreateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool FIsDeleted { get; set; }

        public int GetSafeHashID()
        {
            if (string.IsNullOrWhiteSpace(FSqlText))
            {
                return 0;
            }
            return FSqlText.GetSafeHashCode();
        }
    }
}