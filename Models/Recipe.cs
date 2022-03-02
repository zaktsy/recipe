using System;
using System.Collections.Generic;

namespace recipe
{
    public partial class Recipe
    {
        public Recipe()
        {
            ProductRecipes = new HashSet<ProductRecipe>();
            RecipeSteps = new HashSet<RecipeStep>();
            Peculiarities = new HashSet<Peculiarity>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Categoryid { get; set; }
        public int? Mealid { get; set; }
        public int? Kitchenid { get; set; }
        public int? Proteins { get; set; }
        public int? Fat { get; set; }
        public int? Carbohydrates { get; set; }
        public string? Description { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Kitchen? Kitchen { get; set; }
        public virtual Meal? Meal { get; set; }
        public virtual ICollection<ProductRecipe> ProductRecipes { get; set; }
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; }

        public virtual ICollection<Peculiarity> Peculiarities { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
