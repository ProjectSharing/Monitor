using Monitor.Repository;
using Monitor.DomainService;

namespace Monitor.Application.Implement
{
	/// <summary>
	/// 类名：EmailSendedRecordApplication.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：邮件发送记录业务逻辑
	/// 创建标识：template 2017-10-09 17:07:41
	/// </summary>
	public sealed class EmailSendedRecordApplication : IEmailSendedRecordApplication
	{
		private readonly IEmailSendedRecordRepository _emailSendedRecordRepository;
		private readonly IEmailSendedRecordDomainService _emailSendedRecordDomainService;

		public EmailSendedRecordApplication(IEmailSendedRecordRepository emailSendedRecordRepository, IEmailSendedRecordDomainService emailSendedRecordDomainService)
		{
			_emailSendedRecordRepository= emailSendedRecordRepository;
			_emailSendedRecordDomainService =emailSendedRecordDomainService; 
		}
	}
}
