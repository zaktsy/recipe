using recipe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class UsersSideViewModel: BaseViewModel
    {
        private MainViewModel parent;

        public UsersSideViewModel(MainViewModel parent)
        {
            this.parent = parent;
            name = "usersSide";
            
        }


        #region commands
        private LambdaCommand fridgeCommand;
        public LambdaCommand FridgeCommand
        {
            get
            {
                return fridgeCommand ??
                    (fridgeCommand = new LambdaCommand(obj =>
                    {
                        parent.ChangeViewModel.Execute("fridge");
                    },
                    (obj) =>
                    {
                        var vm = parent.ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));
                        if (vm != null)
                        {
                            var vmUser = (UsersViewModel)vm;
                            if (vmUser.SelectedUser != null) { return true; }
                        }
                        return false;
                    }));
            }
        }


        private LambdaCommand shoppingListCommand;
        public LambdaCommand ShoppingListCommand
        {
            get
            {
                return shoppingListCommand ??
                    (shoppingListCommand = new LambdaCommand(obj =>
                    {
                        parent.ChangeViewModel.Execute("shoppingList");
                    },
                    (obj) =>
                    {
                        var vm = parent.ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));
                        if (vm != null)
                        {
                            var vmUser = (UsersViewModel)vm;
                            if (vmUser.SelectedUser != null) { return true; }
                        }
                        return false;
                    }));
            }
        }


        private LambdaCommand usersCommand;
        public LambdaCommand UsersCommand
        {
            get
            {
                return usersCommand ??
                    (usersCommand = new LambdaCommand(obj =>
                    {
                        parent.ChangeViewModel.Execute("users");
                    },
                    (obj) =>
                    {
                        var vm = parent.SelectedViewModel;
                        if(vm.name == "users") { return false; }
                        return true;
                        
                    }));
            }
        }
        #endregion
    }
}
