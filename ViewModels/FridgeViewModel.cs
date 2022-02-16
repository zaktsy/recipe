using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
        }
    }
}
