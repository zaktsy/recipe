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
            name = "recipes";
            var Cat = (from cat in db.Categories where cat.Id == Recipes[0].Categoryid select cat).FirstOrDefault();
            if (Cat != null) { Category = Cat.Name;}
        }

        private string category;
        public string Category { get { return category; } set { category = value; OnPropertyChanged("Category"); } }
    }
}
