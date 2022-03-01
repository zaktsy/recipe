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
    internal class CategoriesViewModel : BaseViewModel
    {
        private recipesdbContext db;
        
        private MainViewModel parent;

        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories { get { return categories; } set { categories = value; OnPropertyChanged("Categories"); } }

        private Category selectedCategory;
        public Category SelectedCategory { get { return selectedCategory; } set { selectedCategory = value; OnPropertyChanged("SelectedCategory"); } }

        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged("Search"); SelectedCategory = Categories.FirstOrDefault(x => x.Name.ToLower().StartsWith(Search.ToLower())); } }

        public CategoriesViewModel(MainViewModel parent)
        {
            db = new recipesdbContext();
            this.parent = parent;
            name = "сategories";

            Categories = new ObservableCollection<Category>(db.Categories.ToList());
        }

        #region commands


        private LambdaCommand delCategoryCommand;
        public LambdaCommand DelCategoryCommand
        {
            get
            {
                return delCategoryCommand ??
                    (delCategoryCommand = new LambdaCommand(obj =>
                    {
                        DialogViewModelBase vm = new DialogYesNoViewModel("Удалить категорию?");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var id = SelectedCategory.Id;
                            Category cat = (from c in db.Categories
                                            where c.Id == id
                                            select c).FirstOrDefault();
                            db.Categories.Remove(cat);
                            db.SaveChanges();
                            Categories.Remove(SelectedCategory);
                            SelectedCategory = null;
                            parent.IsRecipesInfoChanched = true;
                        }
                    },
                    (obj) => SelectedCategory != null));
            }
        }

        private LambdaCommand editCategoryCommand;
        public LambdaCommand EditCategoryCommand
        {
            get
            {
                return editCategoryCommand ??
                    (editCategoryCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Новое имя:", SelectedCategory.Name);
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            var id = SelectedCategory.Id;
                            Category cat = (from c in db.Categories
                                            where c.Id == id
                                            select c).FirstOrDefault();
                            cat.Name = name;
                            db.SaveChanges();
                            Categories.Remove(SelectedCategory);
                            Categories.Add(cat);
                            SelectedCategory = cat;
                            parent.IsRecipesInfoChanched = true;
                        }

                    },
                    (obj) => SelectedCategory != null));
            }
        }

        private LambdaCommand newCategoryCommand;
        public LambdaCommand NewCategoryCommand
        {
            get
            {
                return newCategoryCommand ??
                    (newCategoryCommand = new LambdaCommand(obj =>
                    {
                        EditNameDialogViewModel vm = new EditNameDialogViewModel("Имя для новой категории:","");
                        DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                        if (result == DialogResult.Yes)
                        {
                            var name = vm.Name;
                            Category cat = new Category();
                            cat.Name = name;
                            db.Categories.Add(cat);
                            db.SaveChanges();
                            Categories.Add(cat);
                        }

                    }));
            }
        }
        #endregion
    }
}
