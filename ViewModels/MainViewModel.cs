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

        private List<BaseViewModel> viewModels = new List<BaseViewModel>();
        public List<BaseViewModel> ViewModels { get { return viewModels; } set { viewModels = value; OnPropertyChanged("ViewModels"); } }

        public MainViewModel(User user)
        {
            db = new recipesdbContext();
            LoginedUser = user;
            selectedViewModel = new HelloViewModel(user);
            ViewModels.Add(selectedViewModel);
            ViewModels.Add(new UsersViewModel());
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
                        switch (obj)
                        {
                            case "users":
                                SelectedViewModel = ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));
                                break;
                        }

                    }));
            }
        }
        #endregion
    }
}
