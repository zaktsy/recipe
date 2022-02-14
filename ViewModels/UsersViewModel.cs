using recipe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class UsersViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private ObservableCollection<User> users;
        public ObservableCollection<User> Users { get { return users; } set { users = value; OnPropertyChanged("Users"); } }

        private User selectedUser;
        public User SelectedUser { get { return selectedUser; } set { selectedUser = value; OnPropertyChanged("SelectedUser"); } }

        public UsersViewModel()
        {
            db = new recipesdbContext();
            Users = new ObservableCollection<User>((from user in db.Users select user).ToList());
        }

        #region commands
        private LambdaCommand editUserCommand;
        public LambdaCommand EditUserCommand
        {
            get
            {
                return editUserCommand ??
                    (editUserCommand = new LambdaCommand(obj =>
                    {
                        

                    },
                    (obj) => SelectedUser!=null));
            }
        }

        private LambdaCommand delUserCommand;
        public LambdaCommand DelUserCommand
        {
            get
            {
                return delUserCommand ??
                    (delUserCommand = new LambdaCommand(obj =>
                    {


                    },
                    (obj) => SelectedUser != null));
            }
        }
        #endregion
    }
}
