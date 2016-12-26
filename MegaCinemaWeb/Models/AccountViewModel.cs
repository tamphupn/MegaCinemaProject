using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MegaCinemaWeb.Models
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
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "Tên")]
        public string FirstName { set; get; }

        [Display(Name = "Họ")]
        public string LastName { set; get; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Giới tính")]
        public bool Sex { get; set; }

        [Display(Name = "CMND")]
        [MaxLength(13)]
        public string SSN { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại.")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Quận")]
        public string District { get; set; }

        [Display(Name = "Thành phố")]
        public string City { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bạn cần nhập email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng.")]
        public string Email { set; get; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Bạn cần nhập mật khẩu.")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        public string Password { set; get; }


    }

    public class RegisterCustomerViewModel
    {
        [Display(Name = "Tên người sử dụng")]
        [Required(ErrorMessage = "Tên người sử dụng không được để trống")]
        public string FirstName { set; get; }

        [Display(Name = "Họ người sử dụng")]
        [Required(ErrorMessage = "Họ người sử dụng không được để trống")]
        public string LastName { set; get; }

        [DisplayFormat(DataFormatString = "{0:dd/mm/yyy")]
        public DateTime Birthday { get; set; }

        public bool Sex { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập Mã CMND/passport")]
        [MaxLength(13)]
        public string SSN { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập địa chỉ")]
        public string Address { set; get; }
        public string District { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng.")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu.")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu.")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        public string ConfirmPassword { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại.")]
        public string PhoneNumber { set; get; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    [DataContract]
    public class RecapchaResult
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}