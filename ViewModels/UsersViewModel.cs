﻿using recipe.Infrastructure;
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
        private bool dialogVisible = false;
        public bool DialogVisible
        {
            get { return this.dialogVisible; }
            set
            {
                if (this.dialogVisible != value)
                {
                    this.dialogVisible = value;
                    OnPropertyChanged("DialogVisible");
                }
            }
        }



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
                        DialogViewModelBase vm =new DialogYesNoViewModel("Удалить пользователя?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if(result == DialogResult.Yes)
                        {
                            DialogViewModelBase vm1 = new DialogYesNoViewModel("Пользователь удален");
                            DialogResult result1 = DialogService.OpenDialog(vm1, obj as Window);
                        }
                    },
                    (obj) => SelectedUser != null));
            }
        }
        #endregion
    }
}
