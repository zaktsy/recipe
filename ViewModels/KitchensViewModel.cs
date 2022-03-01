using recipe.Infrastructure;
using recipe.Infrastructure.dialogs.DialogService;
using recipe.Infrastructure.dialogs.DialogYesNo;
using recipe.Infrastructure.dialogs.EditNameDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace recipe.ViewModels
{
    internal class KitchensViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;

        private ObservableCollection<Kitchen> kitchens;
        public ObservableCollection<Kitchen> Kitchens { get { return kitchens; } set { kitchens = value; OnPropertyChanged("Kitchens"); } }

        private Kitchen selectedKitchen;
        public Kitchen SelectedKitchen { get { return selectedKitchen; } set { selectedKitchen = value; OnPropertyChanged("SelectedKitchen"); } }

        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged("Search"); SelectedKitchen = Kitchens.FirstOrDefault(x => x.Name.ToLower().StartsWith(Search.ToLower())); } }

        public KitchensViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;
            name = "kitchens";

            Kitchens = new ObservableCollection<Kitchen>(db.Kitchens.ToList());
        }

        #region commands


        private LambdaCommand delKitchenCommand;
        public LambdaCommand DelKitchenCommand
        {
            get
            {
                return delKitchenCommand ??
                    (delKitchenCommand = new LambdaCommand(obj =>
                    {
                        DialogViewModelBase vm = new DialogYesNoViewModel("Удалить кухню?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var id = SelectedKitchen.Id;
                            Kitchen kitchen = (from c in db.Kitchens
                                         where c.Id == id
                                         select c).FirstOrDefault();
                            db.Kitchens.Remove(kitchen);
                            db.SaveChanges();
                            Kitchens.Remove(SelectedKitchen);
                            SelectedKitchen = null;
                        }
                    },
                    (obj) => SelectedKitchen != null));
            }
        }

        private LambdaCommand editKitchenCommand;
        public LambdaCommand EditKitchenCommand
        {
            get
            {
                return editKitchenCommand ??
                    (editKitchenCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Новое имя:", SelectedKitchen.Name);
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            var id = SelectedKitchen.Id;
                            Kitchen kitchen = (from c in db.Kitchens
                                            where c.Id == id
                                         select c).FirstOrDefault();
                            kitchen.Name = name;
                            db.SaveChanges();
                            Kitchens.Remove(SelectedKitchen);
                            Kitchens.Add(kitchen);
                            SelectedKitchen = kitchen;
                            parent.IsRecipesInfoChanched = true;
                        }

                    },
                    (obj) => SelectedKitchen != null));
            }
        }

        private LambdaCommand newKitchenCommand;
        public LambdaCommand NewKitchenCommand
        {
            get
            {
                return newKitchenCommand ??
                    (newKitchenCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Имя для нового типа кухни:","");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            Kitchen kitchen = new Kitchen();
                            kitchen.Name = name;
                            db.Kitchens.Add(kitchen);
                            db.SaveChanges();
                            Kitchens.Add(kitchen);
                        }

                    }));
            }
        }
        #endregion
    }
}
