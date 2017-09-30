namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IgnoreConstant.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/29 17:42:30
    /// </summary>
    public class IgnoreConstant
    {
        /// <summary>
        /// FID
        /// </summary>
        public static readonly string[] FID = new string[] { "FID" };

        /// <summary>
        /// FID和创建信息和是否删除
        /// </summary>
        public static readonly string[] IDAndCreate = new string[] { "FID", "FCreateTime", "FCreateUserID", "FIsDeleted" };
    }
}