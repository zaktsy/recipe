using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class UsersSideViewModel: BaseViewModel
    {
        private MainViewModel parent;

        public UsersSideViewModel(MainViewModel parent)
        {
            this.parent = parent;
        }

    }
}
