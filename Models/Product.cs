using System;
using System.Collections.Generic;

namespace recipe
{
    public partial class Product
    {
        public Product()
        {
            Fridges = new HashSet<Fridge>();
            ShoppingLists = new HashSet<ShoppingList>();
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }

        public virtual ICollection<Fridge> Fridges { get; set; }
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
