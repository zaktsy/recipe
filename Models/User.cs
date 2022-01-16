using System;
using System.Collections.Generic;

namespace recipe
{
    public partial class User
    {
        public User()
        {
            Fridges = new HashSet<Fridge>();
            ShoppingLists = new HashSet<ShoppingList>();
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<Fridge> Fridges { get; set; }
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
