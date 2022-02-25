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
            new Item{Name="Spaghetti",Price=199,Amount=20},
            new Item{Name="Apfel",Price=35,Amount=9},
            new Item{Name="Banane",Price=65,Amount=16},
            new Item{Name="Joghurt",Price=345,Amount=8},
            new Item{Name="Gurke",Price=89,Amount=13},
            new Item{Name="Salatkopf",Price=149,Amount=10},
            new Item{Name="Milch",Price=199,Amount=18},
            new Item{Name="Käse",Price=289,Amount=7},
            new Item{Name="Linsen",Price=199,Amount=21},
            new Item{Name="Gemüsebrühe",Price=235,Amount=25},
            new Item{Name="Tafelsalz",Price=45,Amount=19},
            new Item{Name="Pfeffer",Price=149,Amount=17},
            new Item{Name="Gewürzmischung",Price=399,Amount=15},
            new Item{Name="Wasser",Price=99,Amount=35},
            new Item{Name="Apfelsaft",Price=189,Amount=23},
            new Item{Name="Bier",Price=135,Amount=27}
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

