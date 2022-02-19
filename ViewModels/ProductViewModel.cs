using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
