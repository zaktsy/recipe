using recipe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.ViewModels
{
    internal class NewRecipeViewModel : BaseViewModel
    {
        private recipesdbContext db;

        private MainViewModel parent;

        private int id;

        private string nameStr;
        public string NameStr { get { return nameStr; } set { nameStr = value; OnPropertyChanged("NameStr"); } }

        private string namePr;
        public string NamePr { get { return namePr; } set { namePr = value; OnPropertyChanged("Name"); } }

        private string description;
        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }

        private string fat;
        public string Fat { get { return fat; } set { fat = value; OnPropertyChanged("Fat"); } }

        private string proteins;
        public string Proteins { get { return proteins; } set { proteins = value; OnPropertyChanged(nameof(Proteins)); } }

        private string carbohydrates;
        public string Carbohydrates { get { return carbohydrates; } set { carbohydrates = value; OnPropertyChanged("Carbohydrates"); } }

        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories { get { return categories; } set { categories = value; OnPropertyChanged(nameof(Categories)); } }

        private Category selectedCategory;
        public Category SelectedCategory { get { return selectedCategory; } set { selectedCategory=value; OnPropertyChanged(nameof(SelectedCategory)); } }

        private ObservableCollection<Kitchen> kitchens;
        public ObservableCollection<Kitchen > Kitchens { get { return kitchens; } set { kitchens = value; OnPropertyChanged("Kitchens"); } }

        private Kitchen selectedKitchen;
        public Kitchen SelectedKitchen { get { return selectedKitchen; } set { selectedKitchen=value; OnPropertyChanged(nameof(SelectedKitchen));} }

        private ObservableCollection<Meal> meals;
        public ObservableCollection<Meal> Meals { get { return meals; } set { meals= value; OnPropertyChanged(nameof(Meals));} }

        private Meal selectedMeal;
        public Meal SelectedMeal { get { return selectedMeal; } set { selectedMeal=value; OnPropertyChanged("SelectedMeal"); } }

        public NewRecipeViewModel(MainViewModel parent,string recipeName, string disc,int? fat, int? prot, int? car, int? mealid, int? kitchenid, int? catid, int id)
        {
            name = "editRecipe";
            this.id = id;
            this.parent = parent;
            GetInfo();
            NamePr = recipeName;
            NameStr = recipeName;
            Description = disc;
            if(fat != null) { Fat = fat.ToString(); }
            if (car != null) { Carbohydrates = car.ToString(); }
            if (prot != null) { Proteins = prot.ToString(); }
            if (mealid != null) { SelectedMeal = (from meal in db.Meals where meal.Id == mealid select meal).FirstOrDefault(); }
            if (kitchenid != null) { SelectedKitchen = (from kitchen in db.Kitchens where kitchen.Id == kitchenid select kitchen).FirstOrDefault(); }
            if (catid != null) { SelectedCategory = (from cat in db.Categories where cat.Id == catid select cat).FirstOrDefault(); }
        }
        public NewRecipeViewModel(MainViewModel parent)
        {
            name = "newRecipe";
            GetInfo();
            this.parent = parent;
            NameStr = "Новый рецепт";
        }

        private void GetInfo()
        {
            
            db = new recipesdbContext();
            NamePr = "";
            Description = "";
            Fat = "";
            Proteins = "";
            Carbohydrates = "";
            Meals = new ObservableCollection<Meal>(db.Meals.ToList());
            Kitchens = new ObservableCollection<Kitchen>(db.Kitchens.ToList());
            Categories = new ObservableCollection<Category>(db.Categories.ToList());

        }

        private LambdaCommand saveCommand;
        public LambdaCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new LambdaCommand(obj =>
                    {
                        if(name == "newRecipe")
                        {
                            Recipe recipe = new Recipe();
                            recipe.Name = NamePr;
                            recipe.Carbohydrates = Int32.Parse(Carbohydrates);
                            recipe.Fat = Int32.Parse(Fat);
                            recipe.Proteins = Int32.Parse(Proteins);
                            recipe.Description = Description;
                            recipe.Categoryid = SelectedCategory.Id;
                            recipe.Mealid = SelectedMeal.Id;
                            recipe.Kitchenid = SelectedKitchen.Id;
                            db.Recipes.Add(recipe);
                            db.SaveChanges();
                            parent.IsRecipesInfoChanched = true;
                            parent.ChangeViewModel.Execute("recipes");
                        }
                        if(name == "editRecipe")
                        {
                            Recipe recipe = (from rec in db.Recipes where rec.Id == id select rec).FirstOrDefault();
                            recipe.Name = NamePr;
                            recipe.Carbohydrates = Int32.Parse(Carbohydrates);
                            recipe.Fat = Int32.Parse(Fat);
                            recipe.Proteins = Int32.Parse(Proteins);
                            recipe.Description = Description;
                            recipe.Categoryid = SelectedCategory.Id;
                            recipe.Mealid = SelectedMeal.Id;
                            recipe.Kitchenid = SelectedKitchen.Id;
                            db.SaveChanges();
                            parent.IsRecipesInfoChanched = true;
                            parent.ChangeViewModel.Execute("recipes");
                        }
                        
                    },
                    (obj) =>
                    {
                        string st = NamePr.Replace(" ", "");
                        bool check1 = Int32.TryParse(Fat, out int result);
                        bool check2 = Int32.TryParse(Proteins, out int result2);
                        bool check3 = Int32.TryParse(Carbohydrates, out int result3);

                        return (SelectedMeal!=null) && (SelectedKitchen!=null) && (SelectedCategory!=null) && (check1) && (check2) && (check3) && (st.Length > 0);
                    }));
            }
        }

        private LambdaCommand cancelCommand;
        public LambdaCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                    (cancelCommand = new LambdaCommand(obj =>
                    {
                        parent.ChangeViewModel.Execute("recipes");
                    }));
            }
        }

    }
}
