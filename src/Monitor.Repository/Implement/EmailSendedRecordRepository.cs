using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Domain;
using Monitor.Constant;

namespace Monitor.Repository.Implement
{
	/// <summary>
	/// 类名：EmailSendedRecordRepository.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：邮件发送记录数据访问类
	/// 创建标识：template 2017-10-09 17:07:40
	/// </summary>
	public sealed class EmailSendedRecordRepository : BaseDataRepository<EmailSendedRecordInfo>, IEmailSendedRecordRepository
	{
		public EmailSendedRecordRepository(IDataAccessFactory dataAccessFactory): base(dataAccessFactory,TableConstant.TABLE_NAME_EMAILSENDEDRECORD, DbConnConstant.Conn_Name_Monitor)
		{
		}
	}
}
