//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DHDomtica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Wishlists = new HashSet<Wishlist>();
        }
    
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
        public float Price { get; set; }

        [DataType(DataType.ImageUrl)]
        [DisplayName("Productafbeelding")]
        [Required(ErrorMessage = "Vul een URL voor het plaatje van Uw product in")]
        [RegularExpression(@"^https?:\/\/(?:[a-z\-]+\.)+[a-z]{2,6}(?:\/[^\/#?]+)+\.(?:jpe?g|gif|png)$", ErrorMessage = "Vul een correcte URL van de afbeelding in, zoals 'https://www.hashop.nl/Files/5/18000/18005/ProductPhotos/360x200/222127103.png'")]
        public string Image { get; set; }

        [DisplayName("Productcategorie")]
        [Required(ErrorMessage = "Vul een categorienummer van Uw product in")]
        public int MainCategoryID { get; set; }

        [DisplayName("Productcategorie")]
        public virtual MainCategory MainCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
