using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace recipe.ViewModels
{
    internal class RecipesViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;

        private ObservableCollection<Recipe> recipes;
        public ObservableCollection<Recipe> Recipes { get { return recipes; } set { recipes = value; OnPropertyChanged("Recipes"); } }

        private Recipe selectedRecipe;
        public Recipe SelectedRecipe { get { return selectedRecipe; } set { selectedRecipe = value; OnPropertyChanged("SelectedRecipe"); } }

        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged("Search"); SelectedRecipe = Recipes.FirstOrDefault(x => x.Name.ToLower().StartsWith(Search.ToLower())); } }

        public RecipesViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;

            Recipes = new ObservableCollection<Recipe>(db.Recipes.Include(u => u.Products).ToList());
            foreach(var recipe in Recipes)
            {
                recipe.Meal = (from meal in db.Meals where meal.Id == recipe.Mealid select meal).FirstOrDefault();
                recipe.Category = (from category in db.Categories where category.Id == recipe.Categoryid select category).FirstOrDefault();
                recipe.Kitchen = (from kitchen in db.Kitchens where kitchen.Id == recipe.Kitchenid select kitchen).FirstOrDefault();
            }
            name = "recipes";
        }

    }
}
