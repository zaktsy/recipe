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
    internal class RecipeStepEditDialogViewModel : DialogYesNoViewModel
    {
        private int maxStep;

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string description;
        public string Description { get { return description; } set { description = value; } }


        public RecipeStepEditDialogViewModel(string message, string name, string description, int maxStep) : base(message)
        {
            this.Name = name;
            this.Description = description;
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
                        bool check1 = Description!=String.Empty;
                        bool check2 = Int32.TryParse(Name, out int result);
                        bool check3 = result < maxStep + 1;
                        return check1 && check2 && check3;
                    }));
            }
        }
    }
}
