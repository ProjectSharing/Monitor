using System.IO;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：FileUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：文件工具类
    /// 创建标识：yjq 2017/9/14 10:33:35
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// 判断文件是否存在本地目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsExistsFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 文件是否不存在本地目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsNotExistsFile(string filePath)
        {
            return !IsExistsFile(filePath);
        }

        /// <summary>
        /// 删除指定路径的文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void DeleteFile(string filePath)
        {
            if (IsExistsFile(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}