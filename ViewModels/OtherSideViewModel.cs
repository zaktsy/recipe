using recipe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class OtherSideViewModel : BaseViewModel
    {
        private MainViewModel parent;

        public OtherSideViewModel(MainViewModel parent)
        {
            this.parent = parent;
            name = "otherSide";
        }


        #region commands

        private LambdaCommand сategoriesCommand;
        public LambdaCommand CategoriesCommand
        {
            get
            {
                return сategoriesCommand ??
                    (сategoriesCommand = new LambdaCommand(obj =>
                    {
                        parent.ChangeViewModel.Execute("сategories");
                    },
                    (obj) =>
                    {
                        var vm = parent.SelectedViewModel;
                        if (vm.name == "сategories") { return false; }
                        return true;

                    }));
            }
        }
        #endregion
    }
}
