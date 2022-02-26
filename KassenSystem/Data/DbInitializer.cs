using System;
using System.Linq;
using KassenSystem.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KassenSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ItemContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.ItemModels.Any())
            {
                return;   // DB has been seeded
            }

            var items  = new Item[]
            {
            
            new Item{Name="Bier",Price=1.35M}
            };
            foreach (Item s in items)
            {
                context.ItemModels.Add(s);
            }
            context.SaveChanges();

           

            var pay = new Sale[]
            {
            new Sale{PayWay=PayWay.Cash,PayTime=DateTime.Now} };
            foreach (Sale s in pay)
            {
                context.SaleModels0.Add(s);
            }
            context.SaveChanges();




        }

        
    }
}

