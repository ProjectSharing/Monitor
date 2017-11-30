using System;

namespace Monitor.Domain
{
	/// <summary>
	/// 类名：SqlParameterInfo.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：SQL参数信息
	/// 创建标识：template 2017-11-29 22:17:28
	/// </summary>
	public sealed class SqlParameterInfo
	{
		/// <summary>
		/// 主键
		/// </summary>
		public long FID { get; set; }

		/// <summary>
		/// SQL记录ID
		/// </summary>
		public int FRuntimeSqlID { get; set; }

		/// <summary>
		/// 参数名
		/// </summary>
		public string FName { get; set; }

		/// <summary>
		/// 参数值
		/// </summary>
		public string FValue { get; set; }

        /// <summary>
        /// 参数长度
        /// </summary>
        public int FSize { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime FCreateTime { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public bool FIsDeleted { get; set; }

	}
}
