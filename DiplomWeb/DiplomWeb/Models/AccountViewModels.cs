﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomWeb.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
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
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомнить браузер?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        public RegisterViewModel(){
            this.CountHolidayDays = 24;
            }

        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Уведомления по email")]
        public bool EmailNotifications { get; set; }

        [Display(Name = "Уведомления по SMS")]
        public bool SMSNotifications { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public  DateTime DateBirth { get; set; }
       
        [Display(Name = "Фото")]
        public string ImageAvatar { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефонный номмер")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Количество дней отпуска")]
        public int CountHolidayDays { get; set; }

    }

    public class EditUsetViewModel
    {
      
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Уведомления по email")]
        public bool EmailNotifications { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Возраст")]
        public DateTime DateBirth { get; set; }

        [Display(Name = "Фото")]
        public string ImageAvatar { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }


        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефонный номмер")]
        public string Number { get; set; }
    }



    public class ChangedViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Уведомления по email")]
        public bool EmailNotifications { get; set; }

        [Display(Name = "Уведомления по SMS")]
        public bool SMSNotifications { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateBirth { get; set; }

        [Display(Name = "Фото")]
        public string ImageAvatar { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефонный номмер")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }


        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }


        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Display(Name = "Количество дней отпуска")]
        public int CountHolidayDays { get; set; }


        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

      

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}
