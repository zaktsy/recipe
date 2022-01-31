using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace recipe.Infrastructure.behaviors
{
    public class DragWindow: Behavior<DockPanel>
    {
        protected override void OnAttached() => AssociatedObject.MouseDown += OnMouseDown;

        protected override void OnDetaching() => AssociatedObject.MouseUp -= OnMouseDown;

        private void OnMouseDown(object Sender, RoutedEventArgs E)
        {
            var window = Application.Current.MainWindow;
            window.DragMove();
        }
    }
}
