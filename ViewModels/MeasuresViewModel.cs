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
    internal class MeasuresViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;

        private ObservableCollection<Measure> measures;
        public ObservableCollection<Measure> Measures { get { return measures; } set { measures = value; OnPropertyChanged("Measures"); } }

        private Measure selectedMeasure;
        public Measure SelectedMeasure { get { return selectedMeasure; } set { selectedMeasure = value; OnPropertyChanged("SelectedMeasure"); } }

        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged("Search"); SelectedMeasure = Measures.FirstOrDefault(x => x.Name.ToLower().StartsWith(Search.ToLower())); } }

        public MeasuresViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;
            name = "measures";

            Measures = new ObservableCollection<Measure>(db.Measures.ToList());
        }

        #region commands


        private LambdaCommand delMeasureCommand;
        public LambdaCommand DelMeasureCommand
        {
            get
            {
                return delMeasureCommand ??
                    (delMeasureCommand = new LambdaCommand(obj =>
                    {
                        DialogViewModelBase vm = new DialogYesNoViewModel("Удалить меру измерения?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var id = SelectedMeasure.Id;
                            Measure measure = (from c in db.Measures
                                                       where c.Id == id
                                                       select c).FirstOrDefault();
                            db.Measures.Remove(measure);
                            db.SaveChanges();
                            Measures.Remove(SelectedMeasure);
                            SelectedMeasure = null;
                            parent.IsRecipesInfoChanched = true;
                        }
                    },
                    (obj) => SelectedMeasure != null));
            }
        }

        private LambdaCommand editMeasureCommand;
        public LambdaCommand EditMeasureCommand
        {
            get
            {
                return editMeasureCommand ??
                    (editMeasureCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Новое имя:",SelectedMeasure.Name);
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            var id = SelectedMeasure.Id;
                            Measure measure = (from c in db.Measures
                                                       where c.Id == id
                                                       select c).FirstOrDefault();
                            measure.Name = name;
                            db.SaveChanges();
                            Measures.Remove(SelectedMeasure);
                            Measures.Add(measure);
                            SelectedMeasure = measure;
                        }

                    },
                    (obj) => SelectedMeasure != null));
            }
        }

        private LambdaCommand newMeasureCommand;
        public LambdaCommand NewMeasureCommand
        {
            get
            {
                return newMeasureCommand ??
                    (newMeasureCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Имя для новой меры измерения:","");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            Measure measure = new Measure();
                            measure.Name = name;
                            db.Measures.Add(measure);
                            db.SaveChanges();
                            Measures.Add(measure);
                        }

                    }));
            }
        }
        #endregion
    }
}
