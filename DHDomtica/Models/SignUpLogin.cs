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
        public int ID { get; set; }

        [Display(Name = "Voornaam")]
        [Required(ErrorMessage = "Een voornaam moet ingevuld worden")]
        public string UserFirstName { get; set; }

        [Display(Name = "Achternaam")]
        [Required(ErrorMessage = "Een achternaam moet ingevuld worden")]
        public string UserLastName { get; set; }

        [Display(Name = "Geslacht")]
        [Required(ErrorMessage = "Geef Uw geslacht op")]
        public string UserGender { get; set; }

        //[Required]
        //[Display(Name = "E-mailadres")]
        // public string NickName { get; set; }
       
        // NickName = e-mailadres in onze database
        [Required]
        [Display(Name = "E-mailadres")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Vul een correct e-mailadres in")]
        public string UserNickName { get; set; }

        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Een wachtwoord moet ingevuld worden")]
        public string UserPassword { set; get; }

        [Display(Name = "Bevestig Uw wachtwoord")]
        [Required(ErrorMessage = "Vul wachtwoordbevestiging in")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en de wachtwoordbevestiging komen niet overeen.")]
        [DataType(DataType.Password)]
        public string UserPasswordCheck { get; set; }

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Vul een adres in")]
        public string UserBillingAddress { get; set; }

        [Display(Name = "Land")]
        [Required(ErrorMessage = "Kies een land")]
        public string UserCountry { get; set; }

        [Display(Name = "Provincie")]
        [Required(ErrorMessage = "Kies een provincie")]
        public string UserProvince { get; set; }

        [Display(Name = "Postcode")]
        [Required(ErrorMessage = "Vul een postcode in")]
        public string UserZipCode { get; set; }
        // Misschien nog aanpassen, zodat bepaalde waarden er wel of niet in kunnen.
        // Later aanpassen aan nieuwe variabelenamen van database.
    }

    public class SignUp
    {
        public void SignUpUser(Login li)
        {
            // Databasecontext staat in dbml file bij properties
            // Deze waarden moeten nog worden aangepast aan de nieuwe databasewaarden
            DHDomoticaDataContext db = new DHDomoticaDataContext();
            User rgs = new User();
            rgs.ID = li.ID;
            rgs.FirstName = li.UserFirstName;
            rgs.LastName = li.UserLastName;
            rgs.Gender = li.UserGender;
            rgs.NickName = li.UserNickName;
            rgs.Password = li.UserPassword;
            rgs.BillingAddress = li.UserBillingAddress;
            rgs.Land = li.UserCountry;
            // City moet nog provincie worden in database
            rgs.City = li.UserProvince;
            rgs.ZipCode = li.UserZipCode;

            db.Users.InsertOnSubmit(rgs);
            db.SubmitChanges();
        }
    }

    public class SearchUser
    {
        public string searchuser (Login li)
        {
            // Databasecontext moet hier
            DHDomoticaDataContext db = new DHDomoticaDataContext();
            User rgs = new User();
            string passout = "";
            // var pass = from m in db.registers where m.emailid == li.Emailid select m.userpassword;  
            var pass = from m in db.Users where m.NickName == li.UserNickName select m.Password;
            foreach (string query in pass)
            {
                passout = query;
            }
            return passout;
        }
        
    }
}