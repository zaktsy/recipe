using recipe.Infrastructure;
using recipe.Infrastructure.dialogs.DialogService;
using recipe.Infrastructure.dialogs.DialogYesNo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace recipe.ViewModels
{
    internal class FridgeViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;
        private User user;

        private string nameOfFridge;
        public string NameOfFridge { get { return nameOfFridge; } set { nameOfFridge = value; OnPropertyChanged("NameOfFridge"); } }

        private ObservableCollection<Fridge> products;
        public ObservableCollection<Fridge> Products { get { return products; } set { products = value; OnPropertyChanged("Products"); } }

        private Fridge selectedProduct;
        public Fridge SelectedProduct { get { return selectedProduct; } set { selectedProduct = value; OnPropertyChanged("SelectedProduct"); } }

        public FridgeViewModel(MainViewModel parent, User user)
        {
            db = new recipesdbContext();
            this.parent = parent;
            this.user = user;
            NameOfFridge = "Холодильник пользователя "+ user.Name;

            Products = new ObservableCollection<Fridge>(db.Fridges.Where(u => u.Userid == user.Id).ToList());
            for(int i = 0; i< Products.Count; i++)
            {
                Products[i].Product = db.Products.Where(u => u.Id == Products[i].Productid).FirstOrDefault();
                Products[i].Measure = db.Measures.Where(u => u.Id == Products[i].Measureid).FirstOrDefault();
            }
        }

        #region commands
        

        private LambdaCommand delFridgeCommand;
        public LambdaCommand DelFridgeCommand
        {
            get
            {
                return delFridgeCommand ??
                    (delFridgeCommand = new LambdaCommand(obj =>
                    {
                        DialogViewModelBase vm = new DialogYesNoViewModel("Удалить продукт из холодильника пользователя??");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var uid = SelectedProduct.Userid;
                            var pid = SelectedProduct.Productid;
                            Fridge fr = (from fridge in db.Fridges
                                         where fridge.Userid == uid && fridge.Productid == pid
                                         select fridge).FirstOrDefault();
                            db.Fridges.Remove(fr);
                            db.SaveChanges();
                            Products.Remove(SelectedProduct);
                            SelectedProduct = null;
                        }
                    },
                    (obj) => SelectedProduct != null));
            }
        }
        #endregion
    }
}
