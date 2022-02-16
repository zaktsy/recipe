﻿using recipe.Infrastructure;
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
                        switch (obj)
                        {
                            case "users":
                                vm = ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));
                                if (vm == null)
                                {
                                    ViewModels.Add(new UsersViewModel(this));
                                    SelectedViewModel = ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));

                                    var svm = new UsersSideViewModel(this);
                                    SideViewModels.Add(svm);
                                    SelectedSideViewModel = SideViewModels.Find(x => x.GetType() == typeof(UsersSideViewModel));
                                }
                                else { SelectedViewModel = vm; }
                                break;
                            case "fridge":
                                vm = ViewModels.Find(x => x.GetType() == typeof(UsersViewModel));
                                if (vm != null)
                                {
                                    var uvm = (UsersViewModel)vm;
                                    SelectedViewModel = new FridgeViewModel(this, uvm.SelectedUser);
                                }
                                break;
                        }

                    }));
            }
        }
        #endregion
    }
}
