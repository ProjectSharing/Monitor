using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BufferQueue.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：单队列（需注意单例模式）
    /// 创建标识：yjq 2017/9/14 11:17:50
    /// </summary>
    public class BufferQueue<TMessage>
    {
        private ConcurrentQueue<TMessage> _messageQueue;//缓存队列
        private Action<TMessage> _handleMessage;//处理方法
        private Action _handNoCount;//当卡队列处理完毕的通知
        private int _isInProcessMessage = 0;//是否正在处理
        private int _maxWriteCount = 10000;//允许写的最大数量

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxWriteCount">允许写的最大数量</param>
        /// <param name="handleMessage">处理的方法</param>
        /// <param name="handNoCount">当前队列没有没有消息的处理方法，在单次处理完之后调用</param>
        public BufferQueue(int maxWriteCount, Action<TMessage> handleMessage, Action handNoCount = null)
        {
            EnsureUtil.NotNull(handleMessage, "处理方法不能为空");
            _maxWriteCount = maxWriteCount;
            _messageQueue = new ConcurrentQueue<TMessage>();
            _handleMessage = handleMessage;
            _handNoCount = handNoCount;
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="message"></param>
        public void EnqueueMessage(TMessage message)
        {
            _messageQueue.Enqueue(message);
            ProcessMessage();
            if (_messageQueue.Count >= _maxWriteCount)
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
                        TMessage message;
                        while (_messageQueue.TryDequeue(out message))
                        {
                            try
                            {
                                _handleMessage(message);
                            }
                            catch (Exception ex)
                            {
                                LogUtil.Error(ex, memberName: "BufferQueue-ProcessMessage");
                            }
                        }
                        try
                        {
                            _handNoCount?.Invoke();
                        }
                        catch (Exception ex)
                        {
                            LogUtil.Error(ex, memberName: "BufferQueue-ProcessMessage-handNoCount");
                        }
                    }
                    finally
                    {
                        Interlocked.Exchange(ref _isInProcessMessage, 0);
                        if (_messageQueue.Count > 0)
                        {
                            ProcessMessage();
                        }
                    }
                });
            }
        }
    }
}