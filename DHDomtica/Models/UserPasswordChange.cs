using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DHDomtica.Models
{
    public class UserPasswordChange
    {
        [DataType(DataType.Password)]
        [DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "Vul Uw oude wachtwoord in")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "Vul Uw nieuwe wachtwoord in")]
        public string ChangePassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Bevestig wachtwoord")]
        [Required(ErrorMessage = "Bevestig Uw nieuwe wachtwoord")]
        [Compare("Password")]
        public string ChangeConfirmPassword { get; set; }
    }
}