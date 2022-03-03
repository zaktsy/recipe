using recipe.Infrastructure.dialogs.DialogService;
using recipe.Infrastructure.dialogs.DialogYesNo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace recipe.Infrastructure.dialogs.RecipeStepEditDialog
{
    internal class RecipeStepNewDialogViewModel : DialogYesNoViewModel
    {
        private int maxStep;


        private string description;
        public string Description { get { return description; } set { description = value; } }


        public RecipeStepNewDialogViewModel(string message, int maxStep) : base(message)
        {
            this.maxStep = maxStep;
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
                        return Description != String.Empty;
                    }));
            }
        }
    }
}
