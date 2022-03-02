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

namespace recipe.Infrastructure.dialogs.СhoiceDialog
{
    /// <summary>
    /// Логика взаимодействия для ChoiceDialogView.xaml
    /// </summary>
    public partial class ChoiceDialogView : ContentControl
    {
        public ChoiceDialogView()
        {
            InitializeComponent();
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            listBox.ScrollIntoView(listBox.SelectedItem);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
