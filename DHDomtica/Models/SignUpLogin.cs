using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

// Deze en andere code voor inloggen en sign up van Users is gebaseerd op de code van Atul Rawat bereikbaar op: https://www.c-sharpcorner.com/UploadFile/85ed7a/create-register-and-login-page-in-mvc-using-linq-to-sql/

namespace DHDomtica.Models
{
    public class Login
    {
        /*public int ID { get; set; }

        [Display(Name = "Voornaam")]
        [Required(ErrorMessage = "Een voornaam moet ingevuld worden")]
        public string FirstName { get; set; }

        [Display(Name = "Achternaam")]
        [Required(ErrorMessage = "Een achternaam moet ingevuld worden")]
        public string LastName { get; set; }

        [Display(Name = "Geslacht")]
        [Required(ErrorMessage = "Geef Uw geslacht op")]
        public string Gender { get; set; }

        //[Required]
        //[Display(Name = "E-mailadres")]
        // public string NickName { get; set; }
        // Nickname is E-mailadres

        [Required]
        [Display(Name = "E-mailadres")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Vul een correct e-mailadres in")]
        public string NickName { get; set; }

        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Een wachtwoord moet ingevuld worden")]
        public string UserPassword { set; get; }

        [Display(Name = "Bevestig Uw wachtwoord")]
        [Required(ErrorMessage = "Vul wachtwoordbevestiging in")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en de wachtwoordbevestiging komen niet overeen.")]
        [DataType(DataType.Password)]
        public string c_pwd { get; set; }

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Vul een adres in")]
        public string BillingAddress { get; set; }

        [Display(Name = "Land")]
        [Required(ErrorMessage = "Kies een land")]
        public string Country { get; set; }

        [Display(Name = "Provincie")]
        [Required(ErrorMessage = "Kies een provincie")]
        public string Province { get; set; }

        [Display(Name = "Postcode")]
        [Required(ErrorMessage = "Vul een postcode in")]
        public string ZipCode { get; set; }
        // Misschien nog aanpasse, zodat bepaalde waarden er wel of niet in kunnen.
        // Later aanpassen aan nieuwe variabelnamen van database.*/
    }

    public class SignUp
    {
        /*public void SignUpUser(Login li)
        {
            // Databasecontext moet hier
            DHDomoticaModels db = new DHDomoticamodels();
            User rgs = new User();
            rgs.ID = li.ID;
            rgs.NickName = li.NickName;
            rgs.Password = li.UserPassword;
            db.registers.InsertOnSubmit(rgs);
            db.SubmitChanges();
        }*/
    }

    public class SearchUser
    {
        /*public string searchuser (Login li)
        {
            // Databasecontext moet hier
            DHDomoticamodels db = new DHDomoticamodels();
            User rgs = new User();
            string passout = "";
            // var pass = from m in db.registers where m.emailid == li.Emailid select m.userpassword;  
            var pass = from m in db.registers where m.NickName == li.NickName select m.UserPassword;
            // Pass kan aan database liggen, maar weet niet zeker.
            foreach (string query in pass)
            {
                passout = query;
            }
            return passout;
        }*/
        
    }
}