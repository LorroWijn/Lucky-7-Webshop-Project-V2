using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DHDomtica.Models;
using DHDomtica.Supportclasses;
using System.Collections;

namespace DHDomtica.ViewModels
{
    public class AdminManageUserViewModel
    {
        public int ID { get; set; }

        public int AdminID { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Voornaam")]
        [Required(ErrorMessage = "Vul uw voornaam in")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Achternaam")]
        [Required(ErrorMessage = "Vul uw achternaam in")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Geslacht")]
        [Required(ErrorMessage = "Kies uw geslacht")]
        public string Gender { get; set; }

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

        [DataType(DataType.Text)]
        [DisplayName("Land")]
        [Required(ErrorMessage = "Kies uw land")]
        public string Country { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Provincie")]
        [Required(ErrorMessage = "Kies uw provincie")]
        public string Province { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Stad")]
        [Required(ErrorMessage = "Vul uw stad in")]
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        [DisplayName("Postcode")]
        [Required(ErrorMessage = "Vul uw postcode in")]
        [RegularExpression(@"^[1-9][0-9]{3}\s*(?:[a-zA-Z]{2})?$", ErrorMessage = "Vul een correcte Nederlandse postcode in, zoals 3011 WN")]
        public string ZipCode { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Woonadres")]
        [Required(ErrorMessage = "Vul uw woonadres in")]
        [RegularExpression(@"^([1-9][e][\s])*([a-zA-Z]+(([\.][\s])|([\s]))?)+[1-9][0-9]*(([-][1-9][0-9]*)|([\s]?[a-zA-Z]+))?$", ErrorMessage = "Vul een correct Nederlands adres in zonder komma, zoals Wijnhaven 107")]
        public string BillingAddress { get; set; }


        internal void CreateNewUser()
        {
            Password = Crypto.Hash(Password);
            User gebruiker = new User()
            {
                AdminID = AdminID,
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                EMail = EMail,
                Password = Password,
                Country = Country,
                Province = Province,
                City = City,
                ZipCode = ZipCode,
                BillingAddress = BillingAddress,
                EmailConfirmed = false
            };

            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                DHDomoticadbModel.Users.Add(gebruiker);
                DHDomoticadbModel.SaveChanges();
            }
        }

        //Aanpassen aan CRUD. Cookies eruit.
        internal void EditExistingUser(int ID)
        {
            var con = System.Web.HttpContext.Current.Request.Cookies;
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                User user = DHDomoticadbModel.Users.FirstOrDefault(x => x.ID == ID);
                {
                    user.Password = Password;
                    user.AdminID = AdminID;
                    user.FirstName = FirstName;
                    user.LastName = LastName;
                    user.Gender = Gender;
                    user.Country = Country;
                    user.Province = Province;
                    user.City = City;
                    user.ZipCode = ZipCode;
                    user.BillingAddress = BillingAddress;
                };
                {
                    DHDomoticadbModel.SaveChanges();
                }
            }
        }

        public AdminManageUserViewModel(User user)
        {
            ID = user.ID;
            AdminID = user.AdminID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Gender = user.Gender;
            EMail = user.EMail;
            Password = user.Password;
            Country = user.Country;
            Province = user.Province;
            City = user.City;
            ZipCode = user.ZipCode;
            BillingAddress = user.BillingAddress;
            // Ook hier foreign key van adminID in
        }

        public AdminManageUserViewModel()
        {

        }

        public bool EmailInUse()
        {
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                return (DHDomoticadbModel.Users.Any(x => x.EMail == EMail));
            }
        }

        public IEnumerable<AdminManageUserViewModel> VMList()
        {
            var vmUsers = new List<AdminManageUserViewModel>();
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                var users = DHDomoticadbModel.Users;
                foreach (User i in users)
                {
                    var test = new AdminManageUserViewModel(i);
                    vmUsers.Add(test);
                }
            }
            IEnumerable<AdminManageUserViewModel> enumerator = vmUsers.AsEnumerable<AdminManageUserViewModel>();
            return enumerator;
        }


    }
}