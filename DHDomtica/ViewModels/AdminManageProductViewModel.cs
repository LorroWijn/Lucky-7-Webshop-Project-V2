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
    public class AdminManageProductViewModel
    {
        public int ID { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Productnaam")]
        [Required(ErrorMessage = "Vul een productnaam in")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Productbeschrijving")]
        [Required(ErrorMessage = "Vul een productbeschrijving in in")]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Productprijs")]
        [Required(ErrorMessage = "Vul een productprijs in")]
        [RegularExpression(@"^(\d+(?:[\.\,]\d{1,2})?)$", ErrorMessage = "Vul een correcte prijs in met punt en 2 cijfers achter de punt, zoals 123.45")]
        public double Price { get; set; }

        [DataType(DataType.ImageUrl)]
        [DisplayName("Productafbeelding")]
        [Required(ErrorMessage = "Vul een URL voor het plaatje van Uw product in")]
        [RegularExpression(@"^https?:\/\/(?:[a-z\-]+\.)+[a-z]{2,6}(?:\/[^\/#?]+)+\.(?:jpe?g|gif|png)$", ErrorMessage = "Vul een correcte URL van de afbeelding in, zoals 'https://www.hashop.nl/Files/5/18000/18005/ProductPhotos/360x200/222127103.png'")]
        public string Image { get; set; }

        [DisplayName("Productcategorie")]
        [Required(ErrorMessage = "Vul een categorienummer van uw product in")]
        public int MainCategoryID { get; set; }

        [DisplayName("Productcategorie")]
        public virtual MainCategory MainCategory { get; set; }

        public AdminManageProductViewModel()
        {

        }

        internal void CreateNewProduct()
        {
            Product product = new Product()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Image = Image,
                MainCategoryID = MainCategoryID
                // Weet niet of bovenste foreign key goed is
            };

            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                DHDomoticadbModel.Products.Add(product);
                DHDomoticadbModel.SaveChanges();
            }
        }

        internal void ChangeExistingProduct(int ID)
        {
            // Iets met int ID = 0 toevoegen, zodat controller weet welk product hij moet pakken.
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                Product product = DHDomoticadbModel.Products.FirstOrDefault(x => x.ID == ID);
                {
                    product.Name = Name;
                    product.Description = Description;
                    product.Price = Price;
                    product.Image = Image;
                    product.MainCategoryID = MainCategoryID;
                    // Ik weet niet of de foreign key hiervan goed is
                };
                {
                    DHDomoticadbModel.SaveChanges();
                }
            }
        }

        public AdminManageProductViewModel(Product product)
        {
            Name = product.Name;
            ID = product.ID;
            Description = product.Description;
            Price = product.Price;
            Image = product.Image;
            MainCategoryID = product.MainCategoryID;
        }

        public IEnumerable<AdminManageProductViewModel> VMList()
        {
            var vmProduct = new List<AdminManageProductViewModel>();
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                var product = DHDomoticadbModel.Products;
                foreach (Product i in product)
                {
                    var test = new AdminManageProductViewModel(i);
                    vmProduct.Add(test);
                }
            }
            IEnumerable<AdminManageProductViewModel> enumerator = vmProduct.AsEnumerable<AdminManageProductViewModel>();
            return enumerator;
        }
    }
}