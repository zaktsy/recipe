using recipe.Infrastructure.dialogs.DialogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace recipe.Infrastructure.dialogs.DialogYesNo
{
    class DialogYesNoViewModel: DialogViewModelBase
    {
        public DialogYesNoViewModel(string message) : base(message)
        {
        }
        private LambdaCommand yesCommand;
        public LambdaCommand YesCommand
        {
            get
            {
                return yesCommand ??
                    (yesCommand = new LambdaCommand(obj =>
                    {
                        this.CloseDialogWithResult(obj as Window, DialogResult.Yes);
                    }
                    ));
            }
        }

        private LambdaCommand noCommand;
        public LambdaCommand NoCommand
        {
            get
            {
                return noCommand ??
                    (noCommand = new LambdaCommand(obj =>
                    {
                        this.CloseDialogWithResult(obj as Window, DialogResult.No);
                    }
                    ));
            }
        }

    }
}
