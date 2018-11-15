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
        [Required(ErrorMessage = "Vul Uw wachtwoord in")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Bevestig wachtwoord")]
        [Required(ErrorMessage = "Bevestig Uw wachtwoord")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}