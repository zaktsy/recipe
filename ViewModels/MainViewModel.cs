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
        #endregion
    }
}
