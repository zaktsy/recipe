using System;
using System.Collections.Generic;

namespace recipe
{
    public partial class Measure
    {
        public Measure()
        {
            Fridges = new HashSet<Fridge>();
            ProductRecipes = new HashSet<ProductRecipe>();
            ShoppingLists = new HashSet<ShoppingList>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Fridge> Fridges { get; set; }
        public virtual ICollection<ProductRecipe> ProductRecipes { get; set; }
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
