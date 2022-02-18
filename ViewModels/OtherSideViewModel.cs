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

        private LambdaCommand mealsCommand;
        public LambdaCommand MealsCommand
        {
            get
            {
                return mealsCommand ??
                    (mealsCommand = new LambdaCommand(obj =>
                    {
                        parent.ChangeViewModel.Execute("meals");
                    },
                    (obj) =>
                    {
                        var vm = parent.SelectedViewModel;
                        if (vm.name == "meals") { return false; }
                        return true;

                    }));
            }
        }

        private LambdaCommand kitchensCommand;
        public LambdaCommand KitchensCommand
        {
            get
            {
                return kitchensCommand ??
                    (kitchensCommand = new LambdaCommand(obj =>
                    {
                        parent.ChangeViewModel.Execute("kitchens");
                    },
                    (obj) =>
                    {
                        var vm = parent.SelectedViewModel;
                        if (vm.name == "kitchens") { return false; }
                        return true;

                    }));
            }
        }

        private LambdaCommand peculiaritiesCommand;
        public LambdaCommand PeculiaritiesCommand
        {
            get
            {
                return peculiaritiesCommand ??
                    (peculiaritiesCommand = new LambdaCommand(obj =>
                    {
                        parent.ChangeViewModel.Execute("peculiarities");
                    },
                    (obj) =>
                    {
                        var vm = parent.SelectedViewModel;
                        if (vm.name == "peculiarities") { return false; }
                        return true;

                    }));
            }
        }
        #endregion
    }
}
