using System;
using System.Collections.Generic;
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

        public EditRecipeViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;

            CurrentRecipe = ((RecipesViewModel)parent.ViewModels.Find(x => x.GetType() == typeof(RecipesViewModel))).SelectedRecipe;
        }
    }
}
