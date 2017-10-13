namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BaseOperateModel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/12 15:34:53
    /// </summary>
    public class BaseOperateModel
    {
        private int? _operaterUserID;

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int? OperateUserID
        {
            get
            {
                return _operaterUserID ?? -99;
            }
            set
            {
                _operaterUserID = value;
            }
        }
    }
}