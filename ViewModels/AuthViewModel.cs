using recipe.Infrastructure;
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
        private User loginedUser;
        private string email;
        private string password;
        private bool userLogin = false;
        private string message = "hello";
        public User LoginedUser
        {
            get { return loginedUser; }
            set 
            { 
                loginedUser = value;
                if (loginedUser == null) { userLogin = false; }
            }
        }
        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }
        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        public bool UserLogin { get { return userLogin; } set { userLogin = value; OnPropertyChanged("UserLogin"); } }
        public string Message { get { return message; } set { message = value; OnPropertyChanged("Message"); } }
        public AuthViewModel()
        {
            db = new recipesdbContext();
            
        }

        #region commands
        private LambdaCommand checkUserCommand;
        public LambdaCommand CheckUserCommand
        {
            get
            {
                return checkUserCommand ??
                    (checkUserCommand = new LambdaCommand(obj =>
                    {
                        User user = db.Users.Where(u => u.Email == Email).FirstOrDefault();
                        if (user == null) { Message = "такого пользователя не существует"; }
                        else if (user.Password != Password) { Message = "Вы ввели неверный пароль"; }
                        else if (user.Admin != true) { Message = "Вы не являетесь администратором"; }
                        else
                        {
                            LoginedUser = user;
                            userLogin = true;
                            Message = "С возвращением, " + user.Name;
                        }
                        
                    },
                    (obj) => Email!= null && Password != null));
            }
        }
        #endregion
        #region Property
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
