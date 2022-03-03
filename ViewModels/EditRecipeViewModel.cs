using recipe.Infrastructure;
using recipe.Infrastructure.dialogs.DialogService;
using recipe.Infrastructure.dialogs.DialogYesNo;
using recipe.Infrastructure.dialogs.RecipeStepEditDialog;
using recipe.Infrastructure.dialogs.СhoiceDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private RecipeStep selectedStep;
        public RecipeStep SelectedStep { get { return selectedStep; } set { selectedStep = value;OnPropertyChanged("SelectedStep"); } }

        private ObservableCollection<ProductRecipe> products;
        public ObservableCollection<ProductRecipe> Products { get { return products; } set { products = value; OnPropertyChanged(nameof(Products)); } }

        private ProductRecipe selectedProduct;
        public ProductRecipe SelectedProduct { get { return selectedProduct; } set { selectedProduct = value; OnPropertyChanged("SelectedProduct"); } }

        public EditRecipeViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;

            CurrentRecipe = ((RecipesViewModel)parent.ViewModels.Find(x => x.GetType() == typeof(RecipesViewModel))).SelectedRecipe;

            RecipesSteps = new ObservableCollection<RecipeStep>((from steps in db.RecipeSteps where steps.Recipeid == CurrentRecipe.Id select steps));

            Products = new ObservableCollection<ProductRecipe>((from steps in db.ProductRecipes where steps.Recipeid == CurrentRecipe.Id select steps));

            foreach(var prod in Products)
            {
                prod.Product = (from pr in db.Products where pr.Id == prod.Productid select pr).FirstOrDefault();
                prod.Mesure = (from mr in db.Measures where mr.Id == prod.Mesureid select mr).FirstOrDefault();
            }      
        }

        #region commands
        private LambdaCommand delProdCommand;
        public LambdaCommand DelProdCommand
        {
            get
            {
                return delProdCommand ??
                    (delProdCommand = new LambdaCommand(obj =>
                    {
                        DialogViewModelBase vm = new DialogYesNoViewModel("Удалить продукт из рецепта?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            db.ProductRecipes.Remove(SelectedProduct);
                            Products.Remove(SelectedProduct);
                            db.SaveChanges();
                        }
                    },
                    (obj) => SelectedProduct != null));
            }
        }

        private LambdaCommand goBackCommand;
        public LambdaCommand GoBackCommand
        {
            get
            {
                return goBackCommand ??
                    (goBackCommand = new LambdaCommand(obj =>
                    {
                        parent.ChangeViewModel.Execute("recipes");
                    }));
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
                        RecipeStepEditDialogViewModel vm = new RecipeStepEditDialogViewModel("test", SelectedStep.Stepnumber.ToString(), SelectedStep.Description, RecipesSteps.Count);
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            foreach (var stepp in RecipesSteps)
                            {
                                db.RecipeSteps.Remove(stepp);
                            }
                            db.SaveChanges();
                            int newStepNumber = Convert.ToInt32(vm.Name);
                            int currentStepNumber = SelectedStep.Stepnumber;
                            var step = (from r in db.RecipeSteps where r.Recipeid == CurrentRecipe.Id && r.Stepnumber == currentStepNumber select r).FirstOrDefault();
                            if (newStepNumber > currentStepNumber)
                            {
                                for (int i = currentStepNumber; i < newStepNumber; i++)
                                {
                                    RecipesSteps[i].Stepnumber--;
                                }
                            }
                            else if (newStepNumber < currentStepNumber)
                            {
                                for (int i = currentStepNumber-1; i >= newStepNumber; i--)
                                {
                                    RecipesSteps[i-1].Stepnumber++;
                                }

                            }
                            SelectedStep.Description = vm.Description;
                            SelectedStep.Stepnumber = newStepNumber;

                            db.RecipeSteps.AddRange(RecipesSteps);
                            db.SaveChanges();
                            RecipesSteps = new ObservableCollection<RecipeStep>((from steps in db.RecipeSteps where steps.Recipeid == CurrentRecipe.Id select steps));

                        }
                    },
                    (obj) => SelectedStep != null));
            }
        }

        private LambdaCommand newProdCommand;
        public LambdaCommand NewProdCommand
        {
            get
            {
                return newProdCommand ??
                    (newProdCommand = new LambdaCommand(obj =>
                    {
                        ChoiceDialogViewModel vm = new ChoiceDialogViewModel("Выберите продукт", "product");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var prod = (Product)vm.SelectedItem;
                            bool check = true;
                            foreach(var pr in Products)
                            {
                               if (pr.Product.Name == prod.Name) { check = false; break; }
                            }
                            if (check)
                            {
                                
                                var rp = new ProductRecipe();
                                rp.Recipeid = CurrentRecipe.Id;
                                rp.Mesureid = vm.SelectedMeasure.Id;
                                rp.Productid = prod.Id;
                                rp.Amount = Convert.ToInt32(vm.Amount);
                                Products.Add(rp);
                                db.ProductRecipes.Add(rp);
                                db.SaveChanges();
                                foreach (var prodd in Products)
                                {
                                    prodd.Product = (from pr in db.Products where pr.Id == prodd.Productid select pr).FirstOrDefault();
                                    prodd.Mesure = (from mr in db.Measures where mr.Id == prodd.Mesureid select mr).FirstOrDefault();
                                }
                            }
                            
                        }

                    }));
            }
        }
        #endregion
    }
}
