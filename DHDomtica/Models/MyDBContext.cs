using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DHDomtica.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("mysql")
        {
            Database.SetInitializer<MyDBContext>(new MyDbInitializer());
        }

        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class MyDbInitializer : DropCreateDatabaseAlways<MyDBContext>
    {
        protected override void Seed(MyDBContext context)
        {
            /*context.MainCategories.Add(new MainCategory {ID = 1, Name = "Stofzuigers"});
            context.MainCategories.Add(new MainCategory {ID = 2, Name = "Lampen"});*/
            base.Seed(context);
        }
    }

}