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
    internal class UsersViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;

        private ObservableCollection<User> users;
        public ObservableCollection<User> Users { get { return users; } set { users = value; OnPropertyChanged("Users"); } }

        private User selectedUser;
        public User SelectedUser { get { return selectedUser; } set { selectedUser = value; OnPropertyChanged("SelectedUser"); } }

        public UsersViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;
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
                        DialogViewModelBase vm =new DialogYesNoViewModel("Удалить пользователя?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if(result == DialogResult.Yes)
                        {
                            var id = SelectedUser.Id;
                            User User = (from user in db.Users
                                       where user.Id == id
                                       select user).FirstOrDefault();
                            db.Users.Remove(User);
                            db.SaveChanges();
                            Users.Remove(SelectedUser);
                            SelectedUser = null;
                        }
                    },
                    (obj) => SelectedUser != null));
            }
        }
        #endregion
    }
}
