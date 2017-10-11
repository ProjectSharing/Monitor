using System;

namespace Monitor.Domain
{
	/// <summary>
	/// 类名：SysConfigInfo.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：系统配置信息
	/// 创建标识：template 2017-10-10 13:32:46
	/// </summary>
	public sealed class SysConfigInfo
	{
		/// <summary>
		/// 主键、自增
		/// </summary>
		public int FID { get; set; }

		/// <summary>
		/// 键名(唯一不重复)
		/// </summary>
		public string FKey { get; set; }

		/// <summary>
		/// 值
		/// </summary>
		public string FValue { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public bool FIsDeleted { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime FCreateTime { get; set; }

		/// <summary>
		/// 创建人ID
		/// </summary>
		public int FCreateUserID { get; set; }

		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime? FLastModifyTime { get; set; }

		/// <summary>
		/// 最后修改人ID
		/// </summary>
		public int? FLastModifyUserID { get; set; }

	}
}
