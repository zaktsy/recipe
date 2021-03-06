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

        private string search;
        public string Search
        {
            get { return search; }
            set 
            { 
                search = value; 
                OnPropertyChanged("Search");
                var byname = Users.FirstOrDefault(x => x.Name.ToLower().StartsWith(Search.ToLower()));
                var bysurname = Users.FirstOrDefault(x => x.Surname.ToLower().StartsWith(Search.ToLower()));
                var byemail = Users.FirstOrDefault(x => x.Email.ToLower().StartsWith(Search.ToLower()));
                if(byname == null)
                {
                    if(bysurname == null)
                    {
                        SelectedUser = byemail;
                    }
                    else { SelectedUser = bysurname; }
                }
                else { SelectedUser = byname; }

            }

        }

        public UsersViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;
            Users = new ObservableCollection<User>((from user in db.Users select user).ToList());
            name = "users";
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

        private LambdaCommand adminUserCommand;
        public LambdaCommand AdminUserCommand
        {
            get
            {
                return adminUserCommand ??
                    (adminUserCommand = new LambdaCommand(obj =>
                    {
                        if (SelectedUser.Admin == true)
                        {
                            DialogViewModelBase vm = new DialogYesNoViewModel("Забрать статус администратора?");
                            DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                            if( result == DialogResult.Yes)
                            {
                                SelectedUser.Admin = false;
                                db.SaveChanges();
                                User user = SelectedUser;
                                Users.Remove(SelectedUser);
                                Users.Add(user);
                                SelectedUser = user;
                            }
                        }
                        else
                        {
                            DialogViewModelBase vm = new DialogYesNoViewModel("Сделать пользователя Администратором?");
                            DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                            if (result == DialogResult.Yes)
                            {
                                SelectedUser.Admin = true;
                                db.SaveChanges();
                                User user = SelectedUser;
                                Users.Remove(SelectedUser);
                                Users.Add(user);
                                SelectedUser = user;
                            }
                        }

                    },
                    (obj) => SelectedUser != null));
            }
        }
        #endregion
    }
}
