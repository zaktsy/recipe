using recipe.Infrastructure;
using recipe.Infrastructure.dialogs.DialogService;
using recipe.Infrastructure.dialogs.DialogYesNo;
using recipe.Infrastructure.dialogs.EditProductDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace recipe.ViewModels
{
    internal class ProductViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products { get { return products; } set { products = value; OnPropertyChanged("Products"); } }

        private Product selectedProduct;
        public Product SelectedProduct { get { return selectedProduct; } set { selectedProduct = value; OnPropertyChanged("SelectedProduct"); } }

        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged("Search"); SelectedProduct = Products.FirstOrDefault(x => x.Name.ToLower().StartsWith(Search.ToLower())); } }


        public ProductViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;
            name = "products";

            Products = new ObservableCollection<Product>(db.Products.ToList());
        }

        #region commands
        private LambdaCommand delProductCommand;
        public LambdaCommand DelProductCommand
        {
            get
            {
                return delProductCommand ??
                    (delProductCommand = new LambdaCommand(obj =>
                    {
                        DialogViewModelBase vm = new DialogYesNoViewModel("Удалить прием пищи?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var id = SelectedProduct.Id;
                            Product product = (from c in db.Products
                                               where c.Id == id
                                         select c).FirstOrDefault();
                            db.Products.Remove(product);
                            db.SaveChanges();
                            Products.Remove(SelectedProduct);
                            SelectedProduct = null;
                        }
                    },
                    (obj) => SelectedProduct != null));
            }
        }

        private LambdaCommand editProductCommand;
        public LambdaCommand EditProductCommand
        {
            get
            {
                return editProductCommand ??
                    (editProductCommand = new LambdaCommand(obj =>
                    {
                        EditProductDialogViewModel vm = new EditProductDialogViewModel("тест",SelectedProduct.Name,SelectedProduct.Description,SelectedProduct.Photo);
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            var description = vm.Description;
                            var photo = vm.Photo;
                            SelectedProduct.Name = name;
                            SelectedProduct.Description = description;
                            SelectedProduct.Photo = photo;
                            db.SaveChanges();
                            var product = SelectedProduct as Product;
                            Products.Remove(SelectedProduct);
                            Products.Add(product);
                            SelectedProduct = product;
                        }

                    },
                    (obj) => SelectedProduct != null));
            }
        }

        //private LambdaCommand newMealCommand;
        //public LambdaCommand NewMealCommand
        //{
        //    get
        //    {
        //        return newMealCommand ??
        //            (newMealCommand = new LambdaCommand(obj =>
        //            {
        //                EditNameDialogViewModel vm = new EditNameDialogViewModel("Имя для нового приема пищи:");
        //                DialogResult result = DialogService.OpenDialog(vm, obj as Window);
        //                if (result == DialogResult.Yes)
        //                {
        //                    var name = vm.Name;
        //                    Meal meal = new Meal();
        //                    meal.Name = name;
        //                    db.Meals.Add(meal);
        //                    db.SaveChanges();
        //                    Meals.Add(meal);
        //                }

        //            }));
        //    }
        //}
        #endregion
    }
}
