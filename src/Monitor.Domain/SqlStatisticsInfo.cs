using System;

namespace Monitor.Domain
{
	/// <summary>
	/// 类名：SqlStatisticsInfo.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：SQL统计
	/// 创建标识：template 2017-12-02 13:19:32
	/// </summary>
	public sealed class SqlStatisticsInfo
	{
		/// <summary>
		/// 记录ID
		/// </summary>
		public string FID { get; set; }

		/// <summary>
		/// 维度类型(1:项目,2:数据库)
		/// </summary>
		public int FDimensionType { get; set; }

		/// <summary>
		/// 统计维度ID
		/// </summary>
		public int FDimensionID { get; set; }

		/// <summary>
		/// 统计维度名字
		/// </summary>
		public string FDimensionName { get; set; }

		/// <summary>
		/// 统计值
		/// </summary>
		public decimal FValue { get; set; }

		/// <summary>
		/// 统计值类型(1:最大值,2:平均值,3:最小值,4:失败次数,5:执行次数)
		/// </summary>
		public int FValueType { get; set; }

		/// <summary>
		/// 统计时间
		/// </summary>
		public DateTime FStatisticsTime { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public bool FIsDeleted { get; set; }

	}
}
