using JQCore.Configuration;
using JQCore.Extensions;
using JQCore.Mvc.ActionResult;
using JQCore.Mvc.Extensions;
using JQCore.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Monitor.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAdminApplication _adminApplication;

        public AccountController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            LogUtil.Debug(ConfigurationManage.GetValue("ConnectionStrings"));
            return View();
        }

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="model">账号信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return ResultUtil.Failed(ModelState.GetFirstErrorMsg());
            }
            if (!ValidatorCodeIsSuccess(model.ValidateCode))
            {
                return ResultUtil.Failed("验证码错误");
            }
            var operateResult = await _adminApplication.LoginAsync(model);
            if (operateResult.IsSuccess)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, operateResult.Value.ToString()));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    //IsPersistent = true
                });
                return ResultUtil.Success(null, "登录成功");
            }
            return operateResult.ToJsonResult();
        }

        /// <summary>
        /// 校验验证码是否正确
        /// </summary>
        /// <param name="code">需要校验的验证码</param>
        /// <returns>true表示正确</returns>
        private bool ValidatorCodeIsSuccess(string code)
        {
            if (code.IsNullOrEmptyWhiteSpace()) return false;
            return string.Equals(code, HttpContext.Session.GetString("ValidatorCode"));
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidatorCode()
        {
            ValidateCodeUtil validatorCode = new ValidateCodeUtil();
            var validatorInfo = validatorCode.CreateImage(4);
            HttpContext.Session.SetString("ValidatorCode", validatorInfo.Item1);
            return File(validatorInfo.Item2, @"image/png");
        }
    }
}