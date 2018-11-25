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
        [DisplayName("Huidig wachtwoord")]
        [Required(ErrorMessage = "Vul uw oude wachtwoord in")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Nieuw wachtwoord")]
        [Required(ErrorMessage = "Vul uw nieuwe wachtwoord in")]
        public string ChangePassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Verificatie nieuw wachtwoord")]
        [Required(ErrorMessage = "Bevestig uw nieuwe wachtwoord")]
        [Compare("ChangePassword")]
        public string ChangeConfirmPassword { get; set; }
    }
}