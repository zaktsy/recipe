using System.Windows.Controls;

namespace recipe.Views
{
    /// <summary>
    /// Логика взаимодействия для UsersView.xaml
    /// </summary>
    public partial class UsersView : ContentControl
    {
        public UsersView()
        {
            InitializeComponent();
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            listBox.ScrollIntoView(listBox.SelectedItem);
        }
    }
}
