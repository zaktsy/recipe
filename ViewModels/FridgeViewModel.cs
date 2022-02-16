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

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products { get { return products; } set { products = value; OnPropertyChanged("Products"); } }

        private Product selectedProduct;
        public Product SelectedProduct { get { return selectedProduct; } set { selectedProduct = value; OnPropertyChanged("SelectedProduct"); } }

        public FridgeViewModel(MainViewModel parent, User user)
        {
            db = new recipesdbContext();
            this.parent = parent;
            this.user = user;
            NameOfFridge = "Холодильник пользователя "+ user.Name;
        }
    }
}
