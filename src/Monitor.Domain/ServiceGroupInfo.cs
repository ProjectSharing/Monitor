using System;

namespace Monitor.Domain
{
	/// <summary>
	/// 类名：ServiceGroupInfo.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：服务器组信息
	/// 创建标识：template 2017-09-27 16:35:41
	/// </summary>
	public sealed class ServiceGroupInfo
	{
		/// <summary>
		/// 组ID
		/// </summary>
		public int FID { get; set; }

		/// <summary>
		/// 组名字
		/// </summary>
		public string FName { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public string FComment { get; set; }

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
