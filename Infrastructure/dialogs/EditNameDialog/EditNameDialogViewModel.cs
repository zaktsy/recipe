using recipe.Infrastructure.dialogs.DialogService;
using recipe.Infrastructure.dialogs.DialogYesNo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace recipe.Infrastructure.dialogs.EditNameDialog
{
    internal class EditNameDialogViewModel : DialogYesNoViewModel
    {
        private string name;
        public string Name { get { return name; } set { name = value;} }

        public EditNameDialogViewModel(string message) : base(message)
        {
        }
    }
}
