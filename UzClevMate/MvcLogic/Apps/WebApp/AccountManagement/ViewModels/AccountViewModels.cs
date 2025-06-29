using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UzClevMate.MvcLogic.Apps.WebApp.AccountManagement.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Ввведите значение")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Недопустимый адрес электронной почты.")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ввведите значение")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Недопустимый адрес электронной почты.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ввведите значение")]
        [StringLength(100, ErrorMessage = "Минимум 6 символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ввведите значение")]
        [EmailAddress(ErrorMessage = "Недопустимый адрес электронной почты.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ввведите значение")]
        [StringLength(20, ErrorMessage = "Минимум 2 символа", MinimumLength = 2)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ввведите значение")]
        [StringLength(100, ErrorMessage = "Минимум 6 символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Выберите вашу роль")]
        [Display(Name = "Роль")]
        public string UserRole { get; set; }

        public override string ToString()
        {
            return $"Email:{Email}, Password:{Password}, UserRole: {UserRole}";
        }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Ввведите значение")]
        [EmailAddress(ErrorMessage = "Недопустимый адрес электронной почты.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Минимум 6 символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Недопустимый адрес электронной почты.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
