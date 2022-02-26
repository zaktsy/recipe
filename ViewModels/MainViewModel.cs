using recipe.Infrastructure;
using recipe.Infrastructure.commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class MainViewModel: BaseViewModel
    {

        private recipesdbContext db;

        private User loginedUser;
        public User LoginedUser { get { return loginedUser; }  set { loginedUser = value; } }


        private BaseViewModel selectedViewModel;
        public BaseViewModel SelectedViewModel { get { return selectedViewModel; } set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); } }

        private BaseViewModel selectedSideViewModel;
        public BaseViewModel SelectedSideViewModel { get { return selectedSideViewModel; } set { selectedSideViewModel = value; OnPropertyChanged("SelectedSideViewModel"); } }

        private List<BaseViewModel> viewModels = new List<BaseViewModel>();
        public List<BaseViewModel> ViewModels { get { return viewModels; } set { viewModels = value; OnPropertyChanged("ViewModels"); } }

        private List<BaseViewModel> sideViewModels = new List<BaseViewModel>();
        public List<BaseViewModel> SideViewModels { get { return sideViewModels; } set { sideViewModels = value; OnPropertyChanged("SideViewModels"); } }


        public MainViewModel(User user)
        {
            db = new recipesdbContext();
            LoginedUser = user;
            selectedViewModel = new HelloViewModel(user);
            ViewModels.Add(selectedViewModel);
            name = "hello";
        }

        #region commands
        
        private BaseCommand closeAppCommand;
        public BaseCommand CloseAppCommand => new CloseAppCommand();

        private BaseCommand minimizeWindowCommand;
        public BaseCommand MinimizeWindowCommand => new MinimizeWindowCommand();

        private LambdaCommand changeViewModel;
        public LambdaCommand ChangeViewModel
        {
            get
            {
                return changeViewModel ??
                    (changeViewModel = new LambdaCommand(obj =>
                    {
                        var vm = SelectedViewModel;
                        var svm = SelectedSideViewModel;
                        switch (obj)
                        {
                            case "users":
                                vm = ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));
                                svm = SideViewModels.Find(x => x.GetType() == typeof(UsersSideViewModel));
                                if (vm == null)
                                {
                                    ViewModels.Add(new UsersViewModel(this));
                                    SelectedViewModel = ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));

                                    SideViewModels.Add(new UsersSideViewModel(this));
                                    SelectedSideViewModel = SideViewModels.Find(x => x.GetType() == typeof(UsersSideViewModel));
                                }
                                else 
                                {
                                    SelectedViewModel = vm;
                                    SelectedSideViewModel = svm;
                                }
                                break;

                            case "fridge":
                                vm = ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));
                                if (vm != null)
                                {
                                    var uvm = (UsersViewModel)vm;
                                    SelectedViewModel = new FridgeViewModel(this, uvm.SelectedUser);
                                    var fvm = (FridgeViewModel)SelectedViewModel;
                                    fvm.Products = new ObservableCollection<Fridge>(db.Fridges.Where(u => u.Userid == uvm.SelectedUser.Id).ToList());
                                    for (int i = 0; i < fvm.Products.Count; i++)
                                    {
                                        fvm.Products[i].Product = db.Products.Where(u => u.Id == fvm.Products[i].Productid).FirstOrDefault();
                                        fvm.Products[i].Measure = db.Measures.Where(u => u.Id == fvm.Products[i].Measureid).FirstOrDefault();
                                    }
                                }
                                break;

                            case "shoppingList":
                                vm = ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));
                                if (vm != null)
                                {
                                    var uvm = (UsersViewModel)vm;
                                    SelectedViewModel = new ShoppingListViewModel(this, uvm.SelectedUser);
                                    var slvm = (ShoppingListViewModel)SelectedViewModel;
                                    slvm.ShoppingLists = new ObservableCollection<ShoppingList>(db.ShoppingLists.Where(u => u.Userid == uvm.SelectedUser.Id).ToList());
                                    for (int i = 0; i < slvm.ShoppingLists.Count; i++)
                                    {
                                        slvm.ShoppingLists[i].Product = db.Products.Where(u => u.Id == slvm.ShoppingLists[i].Productid).FirstOrDefault();
                                        slvm.ShoppingLists[i].Measure = db.Measures.Where(u => u.Id == slvm.ShoppingLists[i].Measureid).FirstOrDefault();
                                    }
                                }
                                break;

                            case "сategories":
                                vm = ViewModels.Find(x => x.GetType() == typeof(CategoriesViewModel));
                                svm = SideViewModels.Find(x => x.GetType() == typeof(OtherSideViewModel));
                                if (vm == null)
                                {
                                    ViewModels.Add(new CategoriesViewModel(this));
                                    SelectedViewModel = ViewModels.Find(x => x.GetType() == typeof(CategoriesViewModel));

                                    SideViewModels.Add(new OtherSideViewModel(this));
                                    SelectedSideViewModel = SideViewModels.Find(x => x.GetType() == typeof(OtherSideViewModel));
                                }
                                else
                                {
                                    var cvm = (CategoriesViewModel)vm;
                                    SelectedViewModel = vm;
                                    SelectedSideViewModel = svm;
                                    cvm.Categories = new ObservableCollection<Category>(db.Categories.ToList());
                                }
                                break;

                            case "meals":
                                vm = ViewModels.Find(x => x.GetType() == typeof(MealViewModel));
                                if(vm == null)
                                {
                                    ViewModels.Add(new MealViewModel(this));
                                    SelectedViewModel = ViewModels.Find(x => x.GetType() == typeof(MealViewModel));
                                }
                                else
                                {
                                    var mvm = (MealViewModel)vm;
                                    mvm.Meals = new ObservableCollection<Meal>(db.Meals.ToList());
                                    SelectedViewModel = vm;
                                }
                                
                                break; 

                            case "kitchens":
                                vm = ViewModels.Find(x => x.GetType() == typeof(KitchensViewModel));
                                if (vm == null)
                                {
                                    ViewModels.Add(new KitchensViewModel(this));
                                    SelectedViewModel = ViewModels.Find(x => x.GetType() == typeof(KitchensViewModel));
                                }
                                else
                                {
                                    var kvm = (KitchensViewModel)vm;
                                    kvm.Kitchens = new ObservableCollection<Kitchen>(db.Kitchens.ToList());
                                    SelectedViewModel = vm;
                                }
                                break;

                            case "peculiarities":
                                vm = ViewModels.Find(x => x.GetType() == typeof(PeculiaritiesViewModel));
                                if (vm == null)
                                {
                                    ViewModels.Add(new PeculiaritiesViewModel(this));
                                    SelectedViewModel = ViewModels.Find(x => x.GetType() == typeof(PeculiaritiesViewModel));
                                }
                                else
                                {
                                    var pvm = (PeculiaritiesViewModel)vm;
                                    pvm.Peculiarities = new ObservableCollection<Peculiarity>(db.Peculiarities.ToList());
                                    SelectedViewModel = vm;
                                }
                                break;

                            case "measures":
                                vm = ViewModels.Find(x => x.GetType() == typeof(MeasuresViewModel));
                                if (vm == null)
                                {
                                    ViewModels.Add(new MeasuresViewModel(this));
                                    SelectedViewModel = ViewModels.Find(x => x.GetType() == typeof(MeasuresViewModel));
                                }
                                else
                                {
                                    var mvm = (MeasuresViewModel)vm;
                                    mvm.Measures = new ObservableCollection<Measure>(db.Measures.ToList());
                                    SelectedViewModel = vm;
                                }
                                break;

                            case "products":
                                vm = ViewModels.Find(x => x.GetType() == typeof(ProductViewModel));
                                if (vm == null)
                                {
                                    ViewModels.Add(new ProductViewModel(this));
                                    SelectedViewModel = ViewModels.Find(x => x.GetType() == typeof(ProductViewModel));

                                     }
                                else
                                {
                                    var pvm = (ProductViewModel)vm;
                                    
                                    SelectedViewModel = vm;
                                }
                                SelectedSideViewModel = null;
                                break;
                        }

                    }));
            }
        }
        #endregion
    }
}
