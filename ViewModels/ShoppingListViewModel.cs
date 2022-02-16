using recipe.Infrastructure;
using recipe.Infrastructure.dialogs.DialogService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class ShoppingListViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;
        private User user;

        private string nameOfSL;
        public string NameOfSL { get { return nameOfSL; } set { nameOfSL = value; OnPropertyChanged("NameOfFridge"); } }

        private ObservableCollection<ShoppingList> shoppingLists;
        public ObservableCollection<ShoppingList> ShoppingLists { get { return shoppingLists; } set { shoppingLists = value; OnPropertyChanged("ShoppingLists"); } }

        private ShoppingList selectedShoppingList;
        public ShoppingList SelectedShoppingList { get { return selectedShoppingList; } set { selectedShoppingList = value; OnPropertyChanged("SelectedShoppingList"); } }

        public ShoppingListViewModel(MainViewModel parent, User user)
        {
            db = new recipesdbContext();
            this.parent = parent;
            this.user = user;
            NameOfSL = "Список покупок пользователя " + user.Name;

            ShoppingLists = new ObservableCollection<ShoppingList>(db.ShoppingLists.Where(u => u.Userid == user.Id).ToList());
            for (int i = 0; i < ShoppingLists.Count; i++)
            {
                ShoppingLists[i].Product = db.Products.Where(u => u.Id == ShoppingLists[i].Productid).FirstOrDefault();
                ShoppingLists[i].Measure = db.Measures.Where(u => u.Id == ShoppingLists[i].Measureid).FirstOrDefault();
            }
        }

        #region commands


        private LambdaCommand delShoppingListCommand;
        public LambdaCommand DelShoppingListCommand
        {
            get
            {
                return delShoppingListCommand ??
                    (delShoppingListCommand = new LambdaCommand(obj =>
                    {
                        //DialogViewModelBase vm = new DialogYesNoViewModel("Удалить продукт из холодильника пользователя??");
                        //DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        //if (result == DialogResult.Yes)
                        //{
                        //    var uid = SelectedProduct.Userid;
                        //    var pid = SelectedProduct.Productid;
                        //    Fridge fr = (from fridge in db.Fridges
                        //                 where fridge.Userid == uid && fridge.Productid == pid
                        //                 select fridge).FirstOrDefault();
                        //    db.Fridges.Remove(fr);
                        //    db.SaveChanges();
                        //    Products.Remove(SelectedProduct);
                        //    SelectedProduct = null;
                        //}
                    },
                    (obj) => SelectedShoppingList != null));
            }
        }
        #endregion
    }
}
