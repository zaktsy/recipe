using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class RecipesSideViewModel: BaseViewModel
    {
        private MainViewModel parent;

        public RecipesSideViewModel(MainViewModel parent)
        {
            this.parent = parent;
            name = "recipesSide";
        }
    }
}
