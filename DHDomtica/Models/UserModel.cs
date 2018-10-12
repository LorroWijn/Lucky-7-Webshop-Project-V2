using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHDomtica.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public int Gender { get; set; }

        public string BillingAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        //public List<int> WishList { get; set; }
    }
}