using JQCore.Utils;
using Monitor.Constant;
using System;

namespace Monitor.Domain
{
    /// <summary>
    /// 类名：EmailSendedRecordInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：邮件发送记录
    /// 创建标识：template 2017-10-09 17:07:41
    /// </summary>
    public sealed class EmailSendedRecordInfo
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
        /// 是否删除
        /// </summary>
        public bool FIsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FCreateTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int FCreateUserID { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? FLastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        public int? FLastModifyUserID { get; set; }

        /// <summary>
        /// 不发送
        /// </summary>
        /// <param name="remark">备注</param>
        /// <param name="operaterID">操作人ID</param>
        public void NotSend(string remark, int operaterID)
        {
            ChangeSendState(SendState.NotSend, remark, operaterID);
        }

        /// <summary>
        /// 发送成功
        /// </summary>
        /// <param name="remark">备注</param>
        /// <param name="operaterID">操作人ID</param>
        public void SendSuccess(string remark, int operaterID)
        {
            ChangeSendState(SendState.Sended, remark, operaterID);
        }

        /// <summary>
        /// 发送失败
        /// </summary>
        /// <param name="remark">备注</param>
        /// <param name="operaterID">操作人ID</param>
        public void SendFailed(string remark, int operaterID)
        {
            ChangeSendState(SendState.SendFailed, remark, operaterID);
        }

        /// <summary>
        /// 更改发送状态
        /// </summary>
        /// <param name="sendState">发送状态</param>
        /// <param name="remark">备注</param>
        /// <param name="operaterID">操作人ID</param>
        private void ChangeSendState(SendState sendState, string remark, int operaterID)
        {
            FSendState = sendState;
            if (!string.IsNullOrWhiteSpace(remark) && remark.Length > 127)
            {
                remark = remark.Substring(0, 127);
            }
            FStateRemark = remark;
            SetModifyDetail(operaterID);
        }

        /// <summary>
        /// 设置修改信息
        /// </summary>
        /// <param name="operaterID">操作人ID</param>
        private void SetModifyDetail(int operaterID)
        {
            FLastModifyTime = DateTimeUtil.Now;
            FLastModifyUserID = operaterID;
        }
    }
}