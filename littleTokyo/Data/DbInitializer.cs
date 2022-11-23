using littleTokyo.Models;

namespace littleTokyo.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MenuContext context)
        {
            if(context.Menus.Any())
            {
                return;
            }

            var menus = new Menu[]
            {
                new Menu{itemName="Okonomiyaki",itemDesc="Japanese Pancake",itemPrice=14.99M,category="Japanese" },
                new Menu{itemName="Curry Udon",itemDesc="Thick, chewy noodles in a curry sauce",itemPrice=14.99M,category="Japanese" },
                new Menu{itemName="Shrimp Tempura",itemDesc="Fried shrimp in a tempura batter",itemPrice=5.99M,category="Japanese" },
                new Menu{itemName="Ramen",itemDesc="Noodle dish in a miso broth with meat and vegetables",itemPrice=19.99M,category="Japanese" },
                new Menu{itemName="Natto",itemDesc="Fermented soy beans served with soy sauce and rice",itemPrice=14.99M,category="Japanese" },
                new Menu{itemName="Tonkatsu",itemDesc="Fried port cutlet served with curry sauce, rice and cabbage",itemPrice=14.99M,category="Japanese" }

            };

            context.Menus.AddRange(menus);
            context.SaveChanges();
        }

    }
}
