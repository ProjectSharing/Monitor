using JQCore.Extensions;
using JQCore.Redis.Serialization;
using JQCore.Utils;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JQCore.Redis.StackExchangeRedis
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：StackExchangeRedisClient.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/7/15 15:46:38
    /// </summary>
    public sealed class StackExchangeRedisClient : IRedisClient
    {
        private IDatabase _database;

        private readonly IRedisBinarySerializer _serializer;

        private readonly ConnectionMultiplexer _connectionMultiplexer;

        private readonly RedisCacheOption _redisCacheOption;

        public StackExchangeRedisClient(RedisCacheOption redisCacheOption, IRedisBinarySerializer serializer)
        {
            EnsureUtil.NotNull(redisCacheOption, "RedisCacheOptions");
            EnsureUtil.NotNull(serializer, "IBinarySerializer");
            _redisCacheOption = redisCacheOption;
            _serializer = serializer;
            _connectionMultiplexer = ConnectionMultiplexerFactory.GetConnection(_redisCacheOption.ConnectionString);
        }

        private ConnectionMultiplexer Connection { get { return _connectionMultiplexer; } }

        private IDatabase Database
        {
            get { return _database ?? (_database = Connection.GetDatabase(_redisCacheOption.DatabaseId)); }
        }

        public IRedisBinarySerializer Serializer
        {
            get
            {
                return _serializer;
            }
        }

        private string SetPrefix(string key)
        {
            return _redisCacheOption.Prefix.IsNullOrEmptyWhiteSpace() ? key : $"{_redisCacheOption.Prefix}{_redisCacheOption.NamespaceSplitSymbol}{key}";
        }

        #region Keys

        /// <summary>
        /// 查找当前命名前缀下共有多少个Key
        /// </summary>
        /// <returns></returns>
        public int KeyCount()
        {
            return CalcuteKeyCount("*");
        }

        /// <summary>
        /// 查找键名
        /// </summary>
        /// <param name="pattern">匹配项</param>
        /// <returns>匹配上的所有键名</returns>
        public IEnumerable<string> SearchKeys(string pattern)
        {
            var endpoints = Connection?.GetEndPoints();

            if (endpoints == null || !endpoints.Any() || Connection == null) return null;

            return Connection.GetServer(endpoints.First())
                .Keys(_redisCacheOption.DatabaseId, pattern)
                .Select(r => (string)r);
        }

        /// <summary>
        /// 判断是否存在当前的Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return Database.KeyExists(SetPrefix(key));
        }

        /// <summary>
        /// 判断是否存在当前的Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(string key)
        {
            return Database.KeyExistsAsync(SetPrefix(key));
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Expire(string key, TimeSpan expiry)
        {
            return Database.KeyExpire(SetPrefix(key), expiry);
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> ExpireAsync(string key, TimeSpan expiry)
        {
            return Database.KeyExpireAsync(SetPrefix(key), expiry);
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Expire(string key, DateTime expiry)
        {
            return Database.KeyExpire(SetPrefix(key), expiry);
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> ExpireAsync(string key, DateTime expiry)
        {
            return Database.KeyExpireAsync(SetPrefix(key), expiry);
        }

        /// <summary>
        /// 移除当前key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Database.KeyDelete(SetPrefix(key));
        }

        /// <summary>
        /// 移除当前key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> RemoveAsync(string key)
        {
            return Database.KeyDeleteAsync(SetPrefix(key));
        }

        /// <summary>
        /// 移除全部key
        /// </summary>
        /// <param name="keys"></param>
        public void RemoveAll(IEnumerable<string> keys)
        {
            keys.ForEach(key =>
            {
                Remove(key);
            });
        }

        /// <summary>
        /// 移除全部key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Task RemoveAllAsync(IEnumerable<string> keys)
        {
            return keys.ForEachAsync(RemoveAsync);
        }

        #endregion Keys

        #region Public

        /// <summary>
        /// 清除key
        /// </summary>
        public void FlushDb()
        {
            var endPoints = Database.Multiplexer.GetEndPoints();
            endPoints.ForEach(endPoint =>
            {
                Database.Multiplexer.GetServer(endPoint).FlushDatabase(Database.Database);
            });
        }

        /// <summary>
        /// 清除key
        /// </summary>
        public async Task FlushDbAsync()
        {
            var endPoints = Database.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                await Database.Multiplexer.GetServer(endpoint).FlushDatabaseAsync(Database.Database);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveType"></param>
        public void Save(RedisSaveType saveType)
        {
            var endPoints = Database.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                Database.Multiplexer.GetServer(endpoint).Save(saveType.ToSveType());
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveType"></param>
        /// <returns></returns>
        public async Task SaveAsync(RedisSaveType saveType)
        {
            var endPoints = Database.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                await Database.Multiplexer.GetServer(endpoint).SaveAsync(saveType.ToSveType());
            }
        }

        /// <summary>
        /// 清除当前db的所有数据
        /// </summary>
        public void Clear()
        {
            DeleteKeyWithKeyPrefix("*");
        }

        /// <summary>
        /// 计算当前prefix开头的key总数
        /// </summary>
        /// <param name="prefix">key前缀</param>
        /// <returns></returns>
        private int CalcuteKeyCount(string prefix)
        {
            if (Database.IsNull())
            {
                return 0;
            }
            var retVal = Database.ScriptEvaluate("return table.getn(redis.call('keys', ARGV[1]))", values: new RedisValue[] { SetPrefix(prefix) });
            if (retVal.IsNull)
            {
                return 0;
            }
            return (int)retVal;
        }

        /// <summary>
        /// 删除以当前prefix开头的所有key缓存
        /// </summary>
        /// <param name="prefix">key前缀</param>
        private void DeleteKeyWithKeyPrefix(string prefix)
        {
            if (Database.IsNotNull())
            {
                Database.ScriptEvaluate(@"
                local keys = redis.call('keys', ARGV[1])
                for i=1,#keys,5000 do
                redis.call('del', unpack(keys, i, math.min(i+4999, #keys)))
                end", values: new RedisValue[] { SetPrefix(prefix) });
            }
        }

        #endregion Public

        #region StringSet

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public bool StringSet<T>(string key, T value)
        {
            var objBytes = Serializer.Serialize(value);
            return Database.StringSet(SetPrefix(key), objBytes);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAsync<T>(string key, T value)
        {
            var objBytes = await Serializer.SerializeAsync(value);
            return await Database.StringSetAsync(SetPrefix(key), objBytes);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期间隔</param>
        /// <returns>成功返回true</returns>
        public bool StringSet<T>(string key, T value, TimeSpan expiresIn)
        {
            var objBytes = Serializer.Serialize(value);
            return Database.StringSet(SetPrefix(key), objBytes, expiresIn);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期间隔</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAsync<T>(string key, T value, TimeSpan expiresIn)
        {
            var objBytes = await Serializer.SerializeAsync(value);
            return await Database.StringSetAsync(SetPrefix(key), objBytes, expiresIn);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns>成功返回true</returns>
        public bool StringSet<T>(string key, T value, DateTimeOffset expiresAt)
        {
            var objBytes = Serializer.Serialize(value);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);
            return Database.StringSet(SetPrefix(key), objBytes, expiration);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAsync<T>(string key, T value, DateTimeOffset expiresAt)
        {
            var objBytes = await Serializer.SerializeAsync(value);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);
            return await Database.StringSetAsync(SetPrefix(key), objBytes, expiration);
        }

        /// <summary>
        /// 批量设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="items">键值列表</param>
        /// <returns>成功返回true</returns>
        public bool StringSetAll<T>(IList<Tuple<string, T>> items)
        {
            var values = items.Select(m => new KeyValuePair<RedisKey, RedisValue>(SetPrefix(m.Item1), Serializer.Serialize(m.Item2))).ToArray();
            return Database.StringSet(values);
        }

        /// <summary>
        /// 批量设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="items">键值列表</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAllAsync<T>(IList<Tuple<string, T>> items)
        {
            var values = items.Select(m => new KeyValuePair<RedisKey, RedisValue>(SetPrefix(m.Item1), Serializer.Serialize(m.Item2))).ToArray();
            return await Database.StringSetAsync(values);
        }

        /// <summary>
        /// string获取值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            var valuesBytes = Database.StringGet(SetPrefix(key));
            if (!valuesBytes.HasValue)
            {
                return default(T);
            }
            return Serializer.Deserialize<T>(valuesBytes);
        }

        /// <summary>
        /// string获取值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        public async Task<T> StringGetAsync<T>(string key)
        {
            var valuesBytes = await Database.StringGetAsync(SetPrefix(key));
            if (!valuesBytes.HasValue)
            {
                return default(T);
            }
            return await Serializer.DeserializeAsync<T>(valuesBytes);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public long StringIncrement(string key, long value = 1)
        {
            return Database.StringIncrement(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public Task<long> StringIncrementAsync(string key, long value = 1)
        {
            return Database.StringIncrementAsync(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public double StringIncrementDouble(string key, double value)
        {
            return Database.StringIncrement(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public Task<double> StringIncrementDoubleAsync(string key, double value)
        {
            return Database.StringIncrementAsync(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public long StringDecrement(string key, long value = 1)
        {
            return Database.StringDecrement(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public Task<long> StringDecrementAsync(string key, long value = 1)
        {
            return Database.StringDecrementAsync(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public double StringDecrementDouble(string key, double value)
        {
            return Database.StringDecrement(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public Task<double> StringDecrementDoubleAsync(string key, double value)
        {
            return Database.StringDecrementAsync(SetPrefix(key), value);
        }

        #endregion StringSet

        #region hash

        /// <summary>
        /// 获取所有的Hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public IEnumerable<string> HashKeys(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            return Database.HashKeys(SetPrefix(key), commandFlags).Select(x => x.ToString());
        }

        /// <summary>
        /// 获取hash键的个数
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public long HashLength(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            return Database.HashLength(SetPrefix(key), commandFlags);
        }

        /// <summary>
        /// 设置一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash的键值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string hashField, T value)
        {
            return Database.HashSet(SetPrefix(key), hashField, Serializer.Serialize(value));
        }

        /// <summary>
        /// 设置一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash的键值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string key, string hashField, T value)
        {
            var objBytes = await Serializer.SerializeAsync(value);
            return await Database.HashSetAsync(SetPrefix(key), hashField, objBytes);
        }

        /// <summary>
        /// 批量设置hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="values">键值对</param>
        public void HashSet<T>(string key, Dictionary<string, T> values)
        {
            var entries = values.Select(kv => new HashEntry(kv.Key, Serializer.Serialize(kv.Value)));
            Database.HashSet(SetPrefix(key), entries.ToArray());
        }

        /// <summary>
        /// 批量设置hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="values">键值对</param>
        public Task HashSetAsync<T>(string key, Dictionary<string, T> values)
        {
            var entries = values.Select(kv => new HashEntry(kv.Key, Serializer.Serialize(kv.Value)));
            return Database.HashSetAsync(SetPrefix(key), entries.ToArray());
        }

        /// <summary>
        /// 获取一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public T HashGet<T>(string key, string hashField)
        {
            var redisValue = Database.HashGet(SetPrefix(key), hashField);
            return redisValue.HasValue ? Serializer.Deserialize<T>(redisValue) : default(T);
        }

        /// <summary>
        /// 获取一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string hashField)
        {
            var redisValue = await Database.HashGetAsync(SetPrefix(key), hashField);

            return redisValue.HasValue ? await Serializer.DeserializeAsync<T>(redisValue) : default(T);
        }

        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键组合</param>
        /// <returns></returns>
        public Dictionary<string, T> HashGet<T>(string key, IEnumerable<string> hashFields)
        {
            var result = new Dictionary<string, T>();
            foreach (var hashField in hashFields)
            {
                var value = HashGet<T>(key, hashField);
                result.Add(key, value);
            }
            return result;
        }

        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键组合</param>
        /// <returns></returns>
        public async Task<Dictionary<string, T>> HashGetAsync<T>(string key, IEnumerable<string> hashFields)
        {
            var result = new Dictionary<string, T>();
            foreach (var hashField in hashFields)
            {
                var value = await HashGetAsync<T>(key, hashField);
                result.Add(key, value);
            }
            return result;
        }

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public Dictionary<string, T> HashGetAll<T>(string key)
        {
            return (Database
                        .HashGetAll(SetPrefix(key)))
                        .ToDictionary(
                            x => x.Name.ToString(),
                            x => Serializer.Deserialize<T>(x.Value),
                            StringComparer.Ordinal);
        }

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<Dictionary<string, T>> HashGetAllAsync<T>(string key)
        {
            return (await Database
                        .HashGetAllAsync(SetPrefix(key)))
                        .ToDictionary(
                            x => x.Name.ToString(),
                            x => Serializer.Deserialize<T>(x.Value),
                            StringComparer.Ordinal);
        }

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public IEnumerable<T> HashValues<T>(string key)
        {
            return Database.HashValues(SetPrefix(key)).Select(m => Serializer.Deserialize<T>(m));
        }

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> HashValuesAsync<T>(string key)
        {
            return (await Database.HashValuesAsync(SetPrefix(key))).Select(m => Serializer.Deserialize<T>(m));
        }

        /// <summary>
        /// 判断是否存在hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public bool HashExists(string key, string hashField)
        {
            return Database.HashExists(SetPrefix(key), hashField);
        }

        /// <summary>
        /// 判断是否存在hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public Task<bool> HashExistsAsync(string key, string hashField)
        {
            return Database.HashExistsAsync(SetPrefix(key), hashField);
        }

        /// <summary>
        /// 删除一个hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public bool HashDelete(string key, string hashField)
        {
            return Database.HashDelete(SetPrefix(key), hashField);
        }

        /// <summary>
        /// 删除一个hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public Task<bool> HashDeleteAsync(string key, string hashField)
        {
            return Database.HashDeleteAsync(SetPrefix(key), hashField);
        }

        /// <summary>
        /// 删除hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键集合</param>
        /// <returns></returns>
        public long HashDelete(string key, IEnumerable<string> hashFields)
        {
            return Database.HashDelete(SetPrefix(key), hashFields.Select(x => (RedisValue)x).ToArray());
        }

        /// <summary>
        /// 删除hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键集合</param>
        /// <returns></returns>
        public Task<long> HashDeleteAsync(string key, IEnumerable<string> hashFields)
        {
            return Database.HashDeleteAsync(SetPrefix(key), hashFields.Select(x => (RedisValue)x).ToArray());
        }

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        public long HashIncrement(string key, string hashField, long value = 1)
        {
            return Database.HashIncrement(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        public Task<long> HashIncrementAsync(string key, string hashField, long value = 1)
        {
            return Database.HashIncrementAsync(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        public long HashDecrement(string key, string hashField, long value = 1)
        {
            return Database.HashDecrement(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        public Task<long> HashDecrementAsync(string key, string hashField, long value = 1)
        {
            return Database.HashDecrementAsync(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        public double HashIncrementDouble(string key, string hashField, double value)
        {
            return Database.HashIncrement(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        public Task<double> HashIncrementDoubleAsync(string key, string hashField, double value)
        {
            return Database.HashIncrementAsync(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        public double HashDecrementDouble(string key, string hashField, double value)
        {
            return Database.HashDecrement(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        public Task<double> HashDecrementDoubleAsync(string key, string hashField, double value)
        {
            return Database.HashDecrementAsync(SetPrefix(key), hashField, value);
        }

        #endregion hash

        #region lock

        /// <summary>
        /// 获取一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>成功返回true</returns>
        public bool LockTake<T>(string key, T value, TimeSpan expiry)
        {
            var objBytes = Serializer.Serialize(value);
            return Database.LockTake(SetPrefix(key), objBytes, expiry);
        }

        /// <summary>
        /// 异步获取一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> LockTakeAsync<T>(string key, T value, TimeSpan expiry)
        {
            var objBytes = await Serializer.SerializeAsync(value);
            return await Database.LockTakeAsync(SetPrefix(key), objBytes, expiry);
        }

        /// <summary>
        /// 释放一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public bool LockRelease<T>(string key, T value)
        {
            var objBytes = Serializer.Serialize(value);
            return Database.LockRelease(SetPrefix(key), objBytes);
        }

        /// <summary>
        /// 异步释放一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> LockReleaseAsync<T>(string key, T value)
        {
            var objBytes = await Serializer.SerializeAsync(value);
            return await Database.LockReleaseAsync(SetPrefix(key), objBytes);
        }

        #endregion lock
    }
}