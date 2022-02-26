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
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        public EditNameDialogViewModel(string message) : base(message)
        {
        }
        private LambdaCommand productyesCommand;
        public LambdaCommand ProductYesCommand
        {
            get
            {
                return productyesCommand ??
                    (productyesCommand = new LambdaCommand(obj =>
                    {
                        this.CloseDialogWithResult(obj as Window, DialogResult.Yes);
                    },
                    (obj) =>
                    {
                        string st = null;
                        if (Name != null) { st = Name.Replace(" ", ""); }
                        return st != null && st.Length > 0;
                    }));
            }
        }
    }
}
