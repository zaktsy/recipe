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
    internal class PeculiaritiesViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;

        private ObservableCollection<Peculiarity> peculiarities;
        public ObservableCollection<Peculiarity> Peculiarities { get { return peculiarities; } set { peculiarities = value; OnPropertyChanged("Peculiarities"); } }

        private Peculiarity selectedPeculiarity;
        public Peculiarity SelectedPeculiarity { get { return selectedPeculiarity; } set { selectedPeculiarity = value; OnPropertyChanged("selectedPeculiarity"); } }

        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged("Search"); SelectedPeculiarity = Peculiarities.FirstOrDefault(x => x.Name.ToLower().StartsWith(Search.ToLower())); } }

        public PeculiaritiesViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;
            name = "peculiarities";

            Peculiarities = new ObservableCollection<Peculiarity>(db.Peculiarities.ToList());
        }

        #region commands


        private LambdaCommand delPeculiarityCommand;
        public LambdaCommand DelPeculiarityCommand
        {
            get
            {
                return delPeculiarityCommand ??
                    (delPeculiarityCommand = new LambdaCommand(obj =>
                    {
                        DialogViewModelBase vm = new DialogYesNoViewModel("Удалить кухню?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var id = SelectedPeculiarity.Id;
                            Peculiarity peculiarity = (from c in db.Peculiarities
                                                       where c.Id == id
                                               select c).FirstOrDefault();
                            db.Peculiarities.Remove(peculiarity);
                            db.SaveChanges();
                            Peculiarities.Remove(SelectedPeculiarity);
                            SelectedPeculiarity = null;
                            parent.IsRecipesInfoChanched = true;
                        }
                    },
                    (obj) => SelectedPeculiarity != null));
            }
        }

        private LambdaCommand editPeculiarityCommand;
        public LambdaCommand EditPeculiarityCommand
        {
            get
            {
                return editPeculiarityCommand ??
                    (editPeculiarityCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Новое имя:",SelectedPeculiarity.Name);
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            var id = SelectedPeculiarity.Id;
                            Peculiarity peculiarity = (from c in db.Peculiarities
                                                   where c.Id == id
                                               select c).FirstOrDefault();
                            peculiarity.Name = name;
                            db.SaveChanges();
                            Peculiarities.Remove(SelectedPeculiarity);
                            Peculiarities.Add(peculiarity);
                            SelectedPeculiarity = peculiarity;
                            parent.IsRecipesInfoChanched = true;
                        }

                    },
                    (obj) => SelectedPeculiarity != null));
            }
        }

        private LambdaCommand newPeculiarityCommand;
        public LambdaCommand NewPeculiarityCommand
        {
            get
            {
                return newPeculiarityCommand ??
                    (newPeculiarityCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Имя для новой особенности:","");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            Peculiarity peculiarity = new Peculiarity();
                            peculiarity.Name = name;
                            db.Peculiarities.Add(peculiarity);
                            db.SaveChanges();
                            Peculiarities.Add(peculiarity);
                        }

                    }));
            }
        }
        #endregion
    }
}
