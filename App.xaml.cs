using recipe.ViewModels;
using recipe.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace recipe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        AuthViewModel authVM = new AuthViewModel();
        MainViewModel mainVM;
        User user;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var authWindow = new AuthWindow() { DataContext = authVM };
            authWindow.Closed += OnAuthorizationFinished;
            authWindow.Show();
        }

        void OnAuthorizationFinished(object sender, EventArgs e)
        {
            if (!authVM.userLogin)
                Shutdown();
            mainVM = new MainViewModel(authVM.LoginedUser);

            var mainWindow = new MainWindow() { DataContext = mainVM };
            mainWindow.Closed += OnMainWindowClosed;
            mainWindow.Show();
        }
        void OnMainWindowClosed(object sender, EventArgs e)
        {
            Shutdown();
        }

    }
}
