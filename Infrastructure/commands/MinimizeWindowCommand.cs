using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace recipe.Infrastructure.commands
{
    internal class MinimizeWindowCommand : BaseCommand
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            Window window = Application.Current.MainWindow;
            window.WindowState = WindowState.Minimized;
        }

    }
}
