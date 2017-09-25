namespace Monitor.Cache
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IAdminCache.cs
    /// 接口属性：公共
    /// 类功能描述：IAdminCache接口
    /// 创建标识：yjq 2017/9/25 10:28:50
    /// </summary>
    public interface IAdminCache
    {
        /// <summary>
        /// 获取尝试登录次数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>尝试登录次数</returns>
        int GetTryLoginCount(string userName);

        /// <summary>
        /// 清除用户尝试登录的次数
        /// </summary>
        /// <param name="userName">用户名</param>
        void ClearLoginCount(string userName);
    }
}