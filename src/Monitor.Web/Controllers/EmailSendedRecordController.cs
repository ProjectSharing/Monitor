using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Web.Controllers
{
    public class EmailSendedRecordController : Controller
    {
        private readonly IEmailSendedRecordApplication _emailSendedRecordApplication;

        public EmailSendedRecordController(IEmailSendedRecordApplication emailSendedRecordApplication)
        {
            _emailSendedRecordApplication = emailSendedRecordApplication;
        }

        /// <summary>
        /// 获取邮件发送记录列表
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(EmailSendRecordPageQueryWhere queryWhere)
        {
            var operateResult = await _emailSendedRecordApplication.GetEmailSendRecordListAsync(queryWhere);
            ViewBag.QueryWhere = queryWhere;
            return View(operateResult.Value);
        }
    }
}