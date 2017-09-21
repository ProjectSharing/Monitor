using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DoubleBufferQueue.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：双缓冲队列（需注意单例模式）
    /// 创建标识：yjq 2017/9/14 11:18:37
    /// </summary>
    public class DoubleBufferQueue<T>
    {
        /// <summary>
        /// 写队列
        /// </summary>
        private ConcurrentQueue<T> _writeQueue;

        /// <summary>
        /// 读队列
        /// </summary>
        private ConcurrentQueue<T> _readQueue;

        /// <summary>
        /// 消息处理的方法
        /// </summary>
        private Action<T> _handleMessageAction;

        /// <summary>
        /// 当卡队列处理完毕的通知
        /// </summary>
        private Action _handNoCountAction;

        /// <summary>
        /// 最大的写入数量
        /// </summary>
        private int _maxWriteCount;

        /// <summary>
        /// 是否正在处理
        /// </summary>
        private int _isInProcessMessage = 0;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="maxWriteCount">写的最大数量，超出后，改变写入速度</param>
        /// <param name="handleMessageAction">处理消息的方法</param>
        /// <param name="handNoCountAction">当卡队列处理完毕的通知</param>
        public DoubleBufferQueue(int maxWriteCount, Action<T> handleMessageAction, Action handNoCountAction)
        {
            EnsureUtil.NotNull(handleMessageAction, "消息处理方法不能为空");
            _writeQueue = new ConcurrentQueue<T>();
            _readQueue = new ConcurrentQueue<T>();
            _handleMessageAction = handleMessageAction;
            _handNoCountAction = handNoCountAction;
            _maxWriteCount = maxWriteCount;
        }

        public void EnqueueMessage(T info)
        {
            _writeQueue.Enqueue(info);
            ProcessMessage();
            if (_writeQueue.Count >= _maxWriteCount)
            {
                Thread.Sleep(1);
            }
        }

        private void ProcessMessage()
        {
            if (Interlocked.CompareExchange(ref _isInProcessMessage, 1, 0) == 0)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        if (!_writeQueue.IsEmpty)
                        {
                            SwapWriteQueue();
                            T info;
                            while (_readQueue.TryDequeue(out info))
                            {
                                try
                                {
                                    _handleMessageAction(info);
                                }
                                catch (Exception ex)
                                {
                                    LogUtil.Error(ex, memberName: "DoubleBufferQueue-ProcessMessage-handleMessageAction");
                                }
                            }
                        }
                        try
                        {
                            _handNoCountAction?.Invoke();
                        }
                        catch (Exception ex)
                        {
                            LogUtil.Error(ex, memberName: "DoubleBufferQueue-ProcessMessage-handNoCountAction");
                        }
                    }
                    finally
                    {
                        Interlocked.Exchange(ref _isInProcessMessage, 0);
                        if (_writeQueue.Count > 0)
                        {
                            ProcessMessage();
                        }
                    }
                });
            }
        }

        private void SwapWriteQueue()
        {
            var tmp = _writeQueue;
            _writeQueue = _readQueue;
            _readQueue = tmp;
        }
    }
}