namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：WarningLogDealModel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：处理警告日志Model
    /// 创建标识：yjq 2017/11/9 15:01:41
    /// </summary>
    public class WarningLogDealModel: BaseOperateModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 处理建议
        /// </summary>
        public string FOperateAdvice { get; set; }

        /// <summary>
        /// 处理方案
        /// </summary>
        public string FTreatmentScheme { get; set; }
    }
}