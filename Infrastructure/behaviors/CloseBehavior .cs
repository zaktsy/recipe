using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace recipe.Infrastructure.behaviors
{
    public class CloseBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Closing += AssociatedObject_Closing;
        }

        private void AssociatedObject_Closing(object sender, CancelEventArgs e)
        {
            Window window = sender as Window;
            window.Closing -= AssociatedObject_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.2));
            anim.Completed += (s, _) => window.Close();
            window.BeginAnimation(UIElement.OpacityProperty, anim);
        }
        protected override void OnDetaching()
        {
            AssociatedObject.Closing -= AssociatedObject_Closing;
        }
    }
}
