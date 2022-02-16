using recipe.Infrastructure;
using recipe.Infrastructure.commands;
using System.Linq;
using System.Windows;

namespace recipe.ViewModels
{
    internal class AuthViewModel : BaseViewModel
    {
        private recipesdbContext db;

        private User loginedUser;
        private string email = "zaktsy@mail.ru";
        private string password = "dnfhvdsd";
        public bool userLogin = false;
        private string message = "";

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
            name = "auth";
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
                        if (user == null) { Message = "Пользователь не найден"; }
                        else if (user.Password != Password) { Message = "Неверный пароль"; }
                        else if (user.Admin != true) { Message = "Вы не администратор"; }
                        else
                        {
                            LoginedUser = user;
                            userLogin = true;
                            Application.Current.MainWindow.Close();
                        }
                        
                    },
                    (obj) => Email!= null && Password != null));
            }
        }
        private BaseCommand closeAppCommand;
        public BaseCommand CloseAppCommand => new CloseAppCommand();

        private BaseCommand minimizeWindowCommand;
        public BaseCommand MinimizeWindowCommand => new MinimizeWindowCommand();
        #endregion
    }
}
