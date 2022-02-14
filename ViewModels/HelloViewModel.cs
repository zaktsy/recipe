using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class HelloViewModel: BaseViewModel
    {
        private User loginedUser;
        public User LoginedUser { get { return loginedUser; } set { loginedUser = value; } }

        private string helloText;
        public string HelloText { get { return helloText; } set { helloText = value; } }

        public HelloViewModel(User user)
        {
            LoginedUser = user;
            HelloText = "C Возвращением, " + LoginedUser.Name + "!";
        }

    }
}
