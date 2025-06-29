using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.Emails.EmailSending.ViewModels;
using UzClevMate.BL.UzClevMateUsers._Common.Models;
using UzClevMate.BL.UzClevMateUsers.Students.Managers;
using UzClevMate.BL.UzClevMateUsers.Students.Models;
using UzClevMate.BL.UzClevMateUsers.Teachers.Managers;
using UzClevMate.BL.UzClevMateUsers.Teachers.Models;
using UzClevMate.MvcLogic._Common.Controllers;
using UzClevMate.MvcLogic._Common.Extensions;
using UzClevMate.MvcLogic.Apps.WebApp.AccountManagement.ViewModels;

namespace UzClevMate.Controllers
{
    [Authorize]
    public class AccountController : _BaseController
    {
        #region register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Register.cshtml");
        }

        private bool VerifyCaptcha(string response)
        {
            using (var client = new WebClient())
            {
                var secret = "6LctRHQqAAAAAP0DsoYG6K48_tLiyNAvTw8FgVF9";
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={response}";

                var result = client.DownloadString(url);

                dynamic jsonData = JsonConvert.DeserializeObject(result);
                return jsonData.success == true && jsonData.score > 0.5;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            var recaptchaResponse = Request.Form["recaptchaResponse"];

            if (!recaptchaResponse.HasValue())
            {
                ModelState.AddModelError("", "Ошибка регистрации. Возможно, вы бот. Если нет, повторите попытку");
                return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Register.cshtml", model);
            }

            var isValidCaptcha = VerifyCaptcha(recaptchaResponse);

            if (!isValidCaptcha)
            {
                ModelState.AddModelError("", "Ошибка регистрации. Возможно, вы бот. Если нет, повторите попытку");
                return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Register.cshtml", model);
            }

            if (string.IsNullOrEmpty(model.UserRole))
            {
                ModelState.AddModelError("", "Укажите вашу роль: учитель или ученик");
                return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Register.cshtml", model);
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    var isTeacher = model.UserRole == _Definitions.TeacherRole;
                    var userName = model.Name.LimitStringLength().Trim();
                    var culture = this.GetCookieValue(CookieDefinitions.CultureCookieName) ?? _Definitions.DefaultCulture;
                    if (isTeacher)
                    {
                        AddRole(user.Id, _Definitions.TeacherRole);

                        var teacher = new Teacher()
                        {
                            Name = userName,
                            Email = user.Email,
                            UserId = user.Id,
                            Culture = culture
                        };

                        TeacherEditManager.CreateTeacher(teacher);
                        SendWelcomeEmailToTeacher(user.Id, userName);
                        this.SetCookieValue(CookieDefinitions.RoleCockieName, _Definitions.TeacherRole);
                    }
                    else
                    {
                        var student = new Student()
                        {
                            Name = userName,
                            Email = user.Email,
                            UserId = user.Id,
                            Culture = culture
                        };

                        StudentEditManager.CreateStudent(student);
                        SendWelcomeEmailToStudent(user.Id, userName);
                        this.SetCookieValue(CookieDefinitions.RoleCockieName, _Definitions.StudentRole);
                    }

                    SignInManager.SignIn(user, isPersistent: true, rememberBrowser: true);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Ошибка входа. Пользователь с таким email уже существует.");
            }

            return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Register.cshtml", model);
        }

        private void SendWelcomeEmailToStudent(string userId, string userName)
        {
            if (Debugger.IsAttached)
            {
                return;
            }

            var emailModel = new CommonEmailModel()
            {
                Name = userName
            };

            //var telegramBot = System.Configuration.ConfigurationManager.AppSettings["StudentBotName"];
            //var link = $"https://t.me/{telegramBot}?start={userId}";
            emailModel.EmailParagraphs = new List<string>()
            {
                $"Добро пожаловать на ClevMate! 🤝",

                //$"Ты на нашей платформе, а значит, готовишься к экзамену по математике. И у тебя точно всё получится! ❤️",
                //$"На ClevMate можно заниматься с учителем или самостоятельно через нашего <a href=\"{link}\">умного чат-бота ClevBot в Telegram</a>.",
                //
                //"<b>Чем тебе поможет ClevBot</b>",
                //"📚 Выдаст короткие 5-минутные задания для тренировки",
                //"💡 Покажем подсказки и объяснения к решениям",
                //"💪 Будет помогать с мотивацией — даст волшебный пендель!",
                //"🏆 Похвалит и наградит за выполненную работу",
                //
                //"Заниматься с учителем — абсолютно бесплатно, а <b>месяц использования ClevBot стоит меньше, чем один урок с репетитором</b>. Это удобный и доступный способ подготовиться к экзаменам самостоятельно.",
                //$"Попробуй бесплатно наш умный чат-бот! Вот ссылка: <a href=\"{link}\">ClevBot в Telegram</a>.",
                //"И главное: не бойся экзамена — с ClevMate ты подготовишься легко и уверенно. Удачи! 🙌"
            };

            var body = this.RenderPartialViewToString("~/BL/Emails/EmailSending/Views/DefaultEmail.cshtml", emailModel);
            UserManager.SendEmailAsync(userId, "Добро пожаловать на ClevMate", body);
        }

        private void SendWelcomeEmailToTeacher(string userId, string userName)
        {
            if (Debugger.IsAttached)
            {
                return;
            }

            var emailModel = new CommonEmailModel()
            {
                Name = userName
            };

            emailModel.EmailParagraphs = new List<string>()
            {
                $"<b>🎉 Добро пожаловать на платформу ClevMate в статусе учителя!</b>.",
                //"Теперь у вас есть доступ к множеству полезных инструментов для успешной работы с учениками:",
                //"📚 используйте <b>150 000+ заданий</b> в каталоге для любого уровня подготовки",
                //"📝 Создавайте <b>контрольные и проверочные работы</b> — онлайн или в формате PDF.",
                //"📈 Разрабатывайте <b>индивидуальные программы подготовки</b> с учётом целей и задач ваших учеников.",
                //"📒 Формируйте <b>персональные рабочие тетради</b>.",
                //"👩‍🏫 Добавляйте учеников, распределяйте их по классам и назначайте <b>персонализированные программы подготовки</b>.",
                //"✍️ Вносите свои <b>авторские задачи</b> и прикрепляйте дополнительные материалы к темам.",
                //"📊 Отслеживайте <b>прогресс класса</b> или каждого ученика индивидуально и используйте мотивационные системы.",
                //
                //"",
                //"💡 <b>Наша миссия</b> — облегчить вашу работу, автоматизировать подготовку к экзаменам, чтобы вы тратили меньше времени на рутинные задачи. Платформа интуитивно понятна, но если возникнут вопросы — мы всегда рядом и готовы помочь!"
            };

            var body = this.RenderPartialViewToString("~/BL/Emails/EmailSending/Views/DefaultEmail.cshtml", emailModel);
            UserManager.SendEmailAsync(userId, "Добро пожаловать на ClevMate", body);
        }

        private void AddRole(string userId, string role)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            if (!roleManager.RoleExists(role))
            {
                roleManager.Create(new IdentityRole(role));
            }

            UserManager.AddToRole(userId, role);
        }

        #endregion

        #region login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Login.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Login.cshtml", model);
            }

            var userToCheckPass = UserManager.FindByEmailAsync(model.Email).Result;

            if (Debugger.IsAttached)
            {
                if (userToCheckPass != null)
                {
                    await SignInManager.SignInAsync(userToCheckPass, true, true);
                    return RedirectToLocal(returnUrl);
                }
            }

            if (userToCheckPass == null)
            {
                ModelState.AddModelError("", "Ошибка входа. Пользователя с таким email не существует. Пожалуйста, создайте аккаунт");
                return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Login.cshtml", model);
            }

            var isValidPassword = UserManager.CheckPasswordAsync(userToCheckPass, model.Password);
            if (!isValidPassword.Result)
            {
                ModelState.AddModelError("", "Ошибка входа. Введен неверный пароль или пользователя с таким email не существует");
                return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Login.cshtml", model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Ошибка входа");
                    return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/Login.cshtml", model);
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            if (User.IsInRole(_Definitions.TeacherRole))
            {
                this.SetCookieValue(CookieDefinitions.RoleCockieName, _Definitions.TeacherRole);
            }
            else
            {
                this.SetCookieValue(CookieDefinitions.RoleCockieName, _Definitions.StudentRole);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region forgot password

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/ForgotPassword.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null /*|| !(await UserManager.IsEmailConfirmedAsync(user.Id))*/)
                {
                    return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/ForgotPasswordConfirmation.cshtml");
                }

                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Восстановление пароля", "Не помнишь свой пароль к ClevMate? Не беда, кликай по <a href=\"" + callbackUrl + "\">ссылке</a> и следуй инструкциям.");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/ForgotPassword.cshtml", model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/ForgotPasswordConfirmation.cshtml");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null
                ? View("~/_Common/BaseMvcLogic/Views/Error.cshtml")
                : View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/ResetPassword.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/ResetPassword.cshtml");
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View("~/MvcLogic/Apps/WebApp/AccountManagement/Views/ResetPasswordConfirmation.cshtml");
        }

        #endregion

        #region external login

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #endregion

        #region managers

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

        #region helpers

        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (returnUrl != null && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}