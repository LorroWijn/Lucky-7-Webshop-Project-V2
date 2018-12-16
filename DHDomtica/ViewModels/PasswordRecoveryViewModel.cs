using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DHDomtica.Models;
using DHDomtica.Supportclasses;

namespace DHDomtica.ViewModels
{
    public class PasswordRecoveryViewModel
    {
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Vul uw E-mailadres in")]
        [RegularExpression(@"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Vul een correct E-mailadres in, zoals 0956570@hr.nl")]
        public string EMail { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "Vul uw wachtwoord in")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirmatie Wachtwoord")]
        [Required(ErrorMessage = "Vul uw wachtwoordbevestiging in")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        internal bool EmailCheck(string InputEmail)
        {
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                User user = DHDomoticadbModel.Users.FirstOrDefault(x => x.EMail == InputEmail);
                {
                    if (user == null)
                    {
                        // Pagina herladen met errorbericht
                        return false;

                    }
                    else
                    {
                        // Verstuur email
                        return true;
                    }
                }
            }
        }

        public PasswordRecoveryViewModel()
        {

        }
    }
}