using JQCore.Configuration;
using JQCore.Extensions;
using JQCore.Security;
using JQCore.Utils;
using JQCore.Web;

namespace Monitor.Web.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：PublicUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：公共帮助类
    /// 创建标识：yjq 2017/9/25 9:45:43
    /// </summary>
    public sealed class PublicUtil
    {
        #region CurrentUserID

        /// <summary>
        /// CurrentUserID
        /// </summary>
        private static readonly string _CurrentUserID_Key = "CurrentUser";

        /// <summary>
        /// UserCookieSign
        /// </summary>
        private static readonly string _CurrentUserCookieSign_Key = "CurrentUserSign";

        /// <summary>
        /// 获取用的签名的密钥
        /// </summary>
        private static string GetCurrentUserSignKey
        {
            get
            {
                string sign = ConfigurationManage.GetValue("CurrentUserSign");
                return sign.IsNotNullAndNotEmptyWhiteSpace() ? sign : "77582589";
            }
        }

        /// <summary>
        /// 获取用户cookie签名的密钥
        /// </summary>
        private static string GetCurrentUserCookieSignKey
        {
            get
            {
                string sign = ConfigurationManage.GetValue("CurrentUserCookieSig");
                return sign.IsNotNullAndNotEmptyWhiteSpace() ? sign : "77582589";
            }
        }

        /// <summary>
        /// 设置当前用户ID
        /// </summary>
        /// <param name="userID"></param>
        public static void SetCurrentUserID(int userID)
        {
            if (userID <= 0) return;
            ExceptionUtil.LogException(() =>
            {
                var encodeString = DESProviderUtil.Encode(userID.ToString(), GetCurrentUserSignKey);
                CookieUtil.SetCookie(_CurrentUserID_Key, encodeString);
                string cookieSign = (encodeString + GetCurrentUserCookieSignKey).ToMd5();
                CookieUtil.SetCookie(_CurrentUserCookieSign_Key, cookieSign);
            }, memberName: "PublicUtil-SetCurrentUserID");
        }

        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentUserID()
        {
            return ExceptionUtil.LogException(() =>
            {
                string sign = CookieUtil.GetCookie(_CurrentUserCookieSign_Key);
                string userStr = CookieUtil.GetCookie(_CurrentUserID_Key);
                if (sign.IsNullOrEmptyWhiteSpace() || userStr.IsNullOrEmptyWhiteSpace())
                {
                    return -99;
                }
                var decodeString = DESProviderUtil.Decode(userStr, GetCurrentUserSignKey);
                string checkSign = (userStr + GetCurrentUserCookieSignKey).ToMd5();
                if (sign.Equals(checkSign))
                {
                    return decodeString.ToSafeInt32(-99);
                }
                return -99;
            }, memberName: "PublicUtil-GetCurrentUserID");
        }

        #endregion CurrentUserID
    }
}