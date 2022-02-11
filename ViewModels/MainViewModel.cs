using recipe.Infrastructure;
using recipe.Infrastructure.commands;
using System;
using System.Collections.Generic;
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

        public BaseViewModel SelectedViewModel { get { return selectedViewModel; } set { selectedViewModel = value; } }

        public MainViewModel(User user)
        {
            db = new recipesdbContext();
            LoginedUser = user;
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
                        

                    }));
            }
        }
        #endregion
    }
}
