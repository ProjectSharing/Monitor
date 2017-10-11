namespace JQCore.Redis
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：RedisSaveType.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：The type of save operation to perform
    /// 创建标识：yjq 2017/7/15 14:43:18
    /// </summary>
    public enum RedisSaveType
    {
        /// <summary>
        /// Instruct Redis to start an Append Only File rewrite process. The rewrite will create a small optimized version of the current Append Only File.
        /// <see cref=" http://redis.io/commands/bgrewriteaof"/>
        /// </summary>
        BackgroundRewriteAppendOnlyFile = 0,

        /// <summary>
        /// Save the DB in background. The OK code is immediately returned. Redis forks,
        /// the parent continues to serve the clients, the child saves the DB on disk then  exits.
        ///  A client my be able to check if the operation succeeded using the LASTSAVE command.
        /// <see cref="http://redis.io/commands/bgsave"/>
        /// </summary>
        BackgroundSave = 1
    }
}