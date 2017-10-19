namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BasePageQueryWhere.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：基础分页查询条件
    /// 创建标识：yjq 2017/9/27 17:11:57
    /// </summary>
    public class BasePageQueryWhere
    {
        protected int? _pageIndex;
        protected int? _pageSize;

        /// <summary>
        /// 当前页面Id
        /// </summary>
        public virtual int PageIndex
        {
            get
            {
                if (_pageIndex == null || _pageIndex <= 0)
                {
                    return 1;
                }
                return _pageIndex ?? 1;
            }
            set
            {
                _pageIndex = value;
            }
        }

        /// <summary>
        /// 页长
        /// </summary>
        public virtual int PageSize
        {
            get
            {
                if (_pageSize == null || _pageSize <= 0)
                {
                    return 30;
                }
                if (_pageSize > 500)
                {
                    return 500;
                }
                return _pageSize ?? 30;
            }
            set
            {
                _pageSize = value;
            }
        }

        /// <summary>
        /// 排列字段
        /// </summary>
        public string OrderColumn { get; set; }

        /// <summary>
        /// 获取排序信息
        /// </summary>
        /// <returns></returns>
        public virtual string GetOrderColumn()
        {
            if (!string.IsNullOrWhiteSpace(OrderColumn))
            {
                return string.Join(" ", OrderColumn.Split('|'));
            }
            return null;
        }
    }
}