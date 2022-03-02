using Microsoft.EntityFrameworkCore;
using recipe.Infrastructure;
using recipe.Infrastructure.dialogs.DialogService;
using recipe.Infrastructure.dialogs.DialogYesNo;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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

            Recipes = new ObservableCollection<Recipe>(db.Recipes.ToList());
            foreach(var recipe in Recipes)
            {
                recipe.Meal = (from meal in db.Meals where meal.Id == recipe.Mealid select meal).FirstOrDefault();
                recipe.Category = (from category in db.Categories where category.Id == recipe.Categoryid select category).FirstOrDefault();
                recipe.Kitchen = (from kitchen in db.Kitchens where kitchen.Id == recipe.Kitchenid select kitchen).FirstOrDefault();
                recipe.ProductRecipes = (from step in db.ProductRecipes where step.Recipeid == recipe.Id select step).ToList();
            }
            
            name = "recipes";
        }



        #region commands
        private LambdaCommand delRecipeCommand;
        public LambdaCommand DelRecipeCommand
        {
            get
            {
                return delRecipeCommand ??
                    (delRecipeCommand = new LambdaCommand(obj =>
                    {
                        DialogViewModelBase vm = new DialogYesNoViewModel("Удалить рецепт?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var id = SelectedRecipe.Id;
                            Recipe recipe = (from c in db.Recipes
                                               where c.Id == id
                                               select c).FirstOrDefault();
                            db.Recipes.Remove(recipe);
                            db.SaveChanges();
                            Recipes.Remove(SelectedRecipe);
                            SelectedRecipe = null;
                        }
                    },
                    (obj) => SelectedRecipe != null));
            }
        }

        private LambdaCommand editRecipeCommand;
        public LambdaCommand EditRecipeCommand
        {
            get
            {
                return editRecipeCommand ??
                    (editRecipeCommand = new LambdaCommand(obj =>
                    {

                        parent.ChangeViewModel.Execute("editRecipe");

                    },
                    (obj) => SelectedRecipe != null));
            }
        }

        //private LambdaCommand newProductCommand;
        //public LambdaCommand NewProductCommand
        //{
        //    get
        //    {
        //        return newProductCommand ??
        //            (newProductCommand = new LambdaCommand(obj =>
        //            {
        //                EditProductDialogViewModel vm = new EditProductDialogViewModel("тест", "", ""/*SelectedProduct.Photo*/);
        //                DialogResult result = DialogService.OpenDialog(vm, obj as Window);
        //                if (result == DialogResult.Yes)
        //                {
        //                    var name = vm.Name;
        //                    var desc = vm.Description;
        //                    Product product = new Product();
        //                    product.Name = name;
        //                    product.Description = desc;
        //                    db.Products.Add(product);
        //                    db.SaveChanges();
        //                    Products.Add(product);
        //                }

        //            }));
        //    }
        //}
        #endregion
    }
}
