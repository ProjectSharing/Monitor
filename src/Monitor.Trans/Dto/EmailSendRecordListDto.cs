using Monitor.Constant;
using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：EmailSendRecordListDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：邮件发送记录列表传输对象
    /// 创建标识：yjq 2017/11/10 17:05:26
    /// </summary>
    public class EmailSendRecordListDto
    {
        /// <summary>
        /// 主键、自增
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 接收账号
        /// </summary>
        public string FReceiveEmail { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string FTheme { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string FContent { get; set; }

        /// <summary>
        /// 发送账号
        /// </summary>
        public string FSendEmail { get; set; }

        /// <summary>
        /// 发送状态【1:待发送，2:已发送,3:发送失败,4:不发送】
        /// </summary>
        public SendState FSendState { get; set; }

        /// <summary>
        /// 发送状态备注
        /// </summary>
        public string FStateRemark { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime FLastModifyTime { get; set; }
    }
}