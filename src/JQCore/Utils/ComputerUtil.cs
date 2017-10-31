using JQCore.Extensions;
using System.Linq;
using System.Net.NetworkInformation;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ComputerUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/30 21:18:49
    /// </summary>
    public class ComputerUtil
    {
        private static string _MacAddressStr = string.Empty;

        /// <summary>
        /// Mac地址
        /// </summary>
        /// <returns>Mac地址</returns>
        public static string MacAddress
        {
            get
            {
                if (_MacAddressStr.IsNullOrEmptyWhiteSpace())
                {
                    var networks = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (var network in networks)
                    {
                        if (network.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                        {
                            var physicalAddress = network.GetPhysicalAddress();
                            _MacAddressStr = string.Join(":", physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
                            break;
                        }
                    }
                }
                return _MacAddressStr;
            }
        }
    }
}