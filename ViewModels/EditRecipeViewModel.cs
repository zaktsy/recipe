using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class EditRecipeViewModel : BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;

        private Recipe currentRecipe;
        public Recipe CurrentRecipe { get { return currentRecipe; } set { currentRecipe = value;OnPropertyChanged("CurrentRecipe"); } }

        private ObservableCollection<RecipeStep> recipesSteps;
        public ObservableCollection<RecipeStep> RecipesSteps { get { return recipesSteps; } set { recipesSteps = value; OnPropertyChanged("RecipesSteps"); } }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products { get { return products; } set { products = value; OnPropertyChanged(nameof(Products)); } }

        public EditRecipeViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;

            CurrentRecipe = ((RecipesViewModel)parent.ViewModels.Find(x => x.GetType() == typeof(RecipesViewModel))).SelectedRecipe;

            RecipesSteps = new ObservableCollection<RecipeStep>((from steps in db.RecipeSteps where steps.Recipeid == CurrentRecipe.Id select steps));

            Products = new ObservableCollection<Product>(CurrentRecipe.Products);
        }
    }
}
