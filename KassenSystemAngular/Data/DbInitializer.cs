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

            var items  = new ItemModel[]
            {
            new ItemModel{Name="Spaghetti",Price=199,Amount=20},
            new ItemModel{Name="Apfel",Price=35,Amount=9},
            new ItemModel{Name="Banane",Price=65,Amount=16},
            new ItemModel{Name="Joghurt",Price=345,Amount=8},
            new ItemModel{Name="Gurke",Price=89,Amount=13},
            new ItemModel{Name="Salatkopf",Price=149,Amount=10},
            new ItemModel{Name="Milch",Price=199,Amount=18},
            new ItemModel{Name="Käse",Price=289,Amount=7},
            new ItemModel{Name="Linsen",Price=199,Amount=21},
            new ItemModel{Name="Gemüsebrühe",Price=235,Amount=25},
            new ItemModel{Name="Tafelsalz",Price=45,Amount=19},
            new ItemModel{Name="Pfeffer",Price=149,Amount=17},
            new ItemModel{Name="Gewürzmischung",Price=399,Amount=15},
            new ItemModel{Name="Wasser",Price=99,Amount=35},
            new ItemModel{Name="Apfelsaft",Price=189,Amount=23},
            new ItemModel{Name="Bier",Price=135,Amount=27}
            };
            foreach (ItemModel s in items)
            {
                context.ItemModels.Add(s);
            }
            context.SaveChanges();
            

           

            var pay = new PaymentModel[]
            {
            new PaymentModel{PayWay=PayWay.Cash,PayTime=DateTime.Now} };
            foreach (PaymentModel s in pay)
            {
                context.PaymentModels.Add(s);
            }
            context.SaveChanges();




        }

        
    }
}

