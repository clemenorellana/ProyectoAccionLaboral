using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ChangePassword
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Password must be between 6-40 characters.")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }

    public class ChangeProfileTest
    {
        public bool Active { get; set; }
        public bool Busy { get; set; }
    }
}