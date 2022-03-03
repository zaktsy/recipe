using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace recipe.Infrastructure.dialogs.RecipeStepEditDialog
{
    /// <summary>
    /// Логика взаимодействия для RecipeStepEditDialogView.xaml
    /// </summary>
    public partial class RecipeStepEditDialogView : ContentControl
    {
        public RecipeStepEditDialogView()
        {
            InitializeComponent();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
