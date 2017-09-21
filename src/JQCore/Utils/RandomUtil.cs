using System;
using System.Text;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RandomUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：随机数工具类
    /// 创建标识：yjq 2017/9/5 11:14:53
    /// </summary>
    public static class RandomUtil
    {
        private static readonly Random _Random = new Random();

        /// <summary>
        /// 获取指定长度的随机数字
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns>指定长度的随机数字</returns>
        public static string GetRandomNums(int length)
        {
            int[] ints = new int[length];
            for (int i = 0; i < length; i++)
            {
                ints[i] = _Random.Next(0, 9);
            }
            return string.Join("", ints);
        }

        /// <summary>
        /// 获取数字和文字的随机组合
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>数字和文字的随机组合</returns>
        public static string GetRandomNumsAndLetters(int length)
        {
            const string allChar = "0,1,2,3,4,5,6,7,8,9," +
                "A,B,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,T,U,V,W,X,Y,Z," +
                "a,b,c,d,e,f,g,h,k,m,n,p,q,r,s,t,u,v,w,x,y,z";
            string[] allChars = allChar.Split(',');
            StringBuilder result = new StringBuilder();
            while (result.Length < length)
            {
                int index = _Random.Next(allChars.Length);
                string c = allChars[index];
                result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取指定长度汉字
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>汉字组合</returns>
        public static string GetRandomHanzis(int length)
        {
            //汉字编码的组成元素，十六进制数
            string[] baseStrs = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f".Split(',');
            Encoding encoding = Encoding.GetEncoding("GB2312");
            string result = null;

            //每循环一次产生一个含两个元素的十六进制字节数组，并放入bytes数组中
            //汉字由四个区位码组成，1、2位作为字节数组的第一个元素，3、4位作为第二个元素
            for (int i = 0; i < length; i++)
            {
                Random rnd = _Random;
                int index1 = rnd.Next(11, 14);
                string str1 = baseStrs[index1];

                int index2 = index1 == 13 ? rnd.Next(0, 7) : rnd.Next(0, 16);
                string str2 = baseStrs[index2];

                int index3 = rnd.Next(10, 16);
                string str3 = baseStrs[index3];

                int index4 = index3 == 10 ? rnd.Next(1, 16) : (index3 == 15 ? rnd.Next(0, 15) : rnd.Next(0, 16));
                string str4 = baseStrs[index4];

                //定义两个字节变量存储产生的随机汉字区位码
                byte b1 = Convert.ToByte(str1 + str2, 16);
                byte b2 = Convert.ToByte(str3 + str4, 16);
                byte[] bs = { b1, b2 };

                result += encoding.GetString(bs);
            }
            return result;
        }
    }
}