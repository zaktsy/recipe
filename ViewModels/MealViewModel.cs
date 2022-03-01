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
    internal class MealViewModel: BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;

        private ObservableCollection<Meal> meals;
        public ObservableCollection<Meal> Meals { get { return meals; } set { meals = value; OnPropertyChanged("Meals"); } }

        private Meal selectedMeal;
        public Meal SelectedMeal { get { return selectedMeal; } set { selectedMeal = value; OnPropertyChanged("SelectedMeal"); } }

        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged("Search"); SelectedMeal = Meals.FirstOrDefault(x => x.Name.ToLower().StartsWith(Search.ToLower())); } }

        public MealViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;
            name = "meals";

            Meals = new ObservableCollection<Meal>(db.Meals.ToList());
        }

        #region commands


        private LambdaCommand delMealCommand;
        public LambdaCommand DelMealCommand
        {
            get
            {
                return delMealCommand ??
                    (delMealCommand = new LambdaCommand(obj =>
                    {
                        DialogViewModelBase vm = new DialogYesNoViewModel("Удалить прием пищи?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var id = SelectedMeal.Id;
                            Meal meal = (from c in db.Meals
                                            where c.Id == id
                                            select c).FirstOrDefault();
                            db.Meals.Remove(meal);
                            db.SaveChanges();
                            Meals.Remove(SelectedMeal);
                            SelectedMeal = null;
                        }
                    },
                    (obj) => SelectedMeal != null));
            }
        }

        private LambdaCommand editMealCommand;
        public LambdaCommand EditMealCommand
        {
            get
            {
                return editMealCommand ??
                    (editMealCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Новое имя:");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            var id = SelectedMeal.Id;
                            Meal meal = (from c in db.Meals
                                            where c.Id == id
                                            select c).FirstOrDefault();
                            meal.Name = name;
                            db.SaveChanges();
                            Meals.Remove(SelectedMeal);
                            Meals.Add(meal);
                            SelectedMeal = meal;
                            parent.IsRecipesInfoChanched = true;
                        }

                    },
                    (obj) => SelectedMeal != null));
            }
        }

        private LambdaCommand newMealCommand;
        public LambdaCommand NewMealCommand
        {
            get
            {
                return newMealCommand ??
                    (newMealCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Имя для нового приема пищи:");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            Meal meal = new Meal();
                            meal.Name = name;
                            db.Meals.Add(meal);
                            db.SaveChanges();
                            Meals.Add(meal);
                        }

                    }));
            }
        }
        #endregion
    }
}
