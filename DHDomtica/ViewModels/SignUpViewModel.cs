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
    public class SignUpViewModel
    {
        public int ID { get; set; }

        public int AdminID { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Voornaam")]
        [Required(ErrorMessage = "Vul Uw voornaam in")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Achternaam")]
        [Required(ErrorMessage = "Vul Uw achternaam in")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Geslacht")]
        [Required(ErrorMessage = "Kies Uw geslacht")]
        public string Gender { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Vul Uw E-mailadres in")]
        [RegularExpression(@"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Vul een correct E-mailadres in, zoals 0956570@hr.nl")]
        public string EMail { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "Vul Uw wachtwoord in")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Conformatie Wachtwoord")]
        [Required(ErrorMessage = "Vul Uw wachtwoord in")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Land")]
        [Required(ErrorMessage = "Kies Uw land")]
        public string Country { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Provincie")]
        [Required(ErrorMessage = "Kies Uw provincie")]
        public string Province { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Stad")]
        [Required(ErrorMessage = "Vul Uw stad in")]
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        [DisplayName("Postcode")]
        [Required(ErrorMessage = "Vul Uw postcode in")]
        [RegularExpression(@"^[1-9][0-9]{3}\s*(?:[a-zA-Z]{2})?$", ErrorMessage = "Vul een correcte Nederlandse postcode in, zoals 3011 WN")]
        public string ZipCode { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Woonadres")]
        [Required(ErrorMessage = "Vul Uw woonadres in")]
        [RegularExpression(@"^([1-9][e][\s])*([a-zA-Z]+(([\.][\s])|([\s]))?)+[1-9][0-9]*(([-][1-9][0-9]*)|([\s]?[a-zA-Z]+))?$", ErrorMessage = "Vul een correct Nederlands adres in zonder komma, zoals Wijnhaven 107")]
        public string BillingAddress { get; set; }

        internal void CreateNewUser()
        {
            Password = Crypto.Hash(Password);
            User gebruiker = new User()
            {
                AdminID = 1,
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                EMail = EMail,
                Password = Password,
                Country = Country,
                Province = Province,
                City = City,
                ZipCode = ZipCode,
                BillingAddress = BillingAddress
            };

            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                DHDomoticadbModel.Users.Add(gebruiker);
                DHDomoticadbModel.SaveChanges();
            }
        }

        public bool EmailInUse()
        {
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                return (DHDomoticadbModel.Users.Any(x => x.EMail == EMail));
            }
        }
    }
}