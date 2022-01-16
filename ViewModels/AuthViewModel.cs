using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class AuthViewModel : INotifyPropertyChanged
    {
        recipesdbContext db;
        private User _Text;
        public User Text
        {
            get { return _Text; }
            set 
            { 
                _Text = value;
                OnPropertyChanged("Text");
            }
        }

        public AuthViewModel()
        {
            db = new recipesdbContext();
            List<User> users = new List<User>();
            users.AddRange(db.Users);
            Text = users.FirstOrDefault();
        }
        #region Property
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
