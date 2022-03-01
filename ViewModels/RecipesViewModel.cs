using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Recipes = new ObservableCollection<Recipe>((from recipe in db.Recipes select recipe).ToList());
            name = "recipes";
        }
    }
}
