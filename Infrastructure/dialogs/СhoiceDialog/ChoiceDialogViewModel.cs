using recipe.Infrastructure.dialogs.DialogService;
using recipe.Infrastructure.dialogs.DialogYesNo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace recipe.Infrastructure.dialogs.СhoiceDialog
{
    internal class ChoiceDialogViewModel : DialogYesNoViewModel
    {
        private string type;

        private Object selectedItem;
        public Object SelectedItem { get { return selectedItem; } set { selectedItem = value;OnPropertyChanged("SelectedItem"); } }

        private ObservableCollection<Object> items;
        public ObservableCollection<Object> Items { get { return items; } set { items = value;OnPropertyChanged("Items"); } }

        private List<string> names;
        public List<string> Names { get { return names; } set { names = value;} }

        private ObservableCollection<Measure> measures;
        public ObservableCollection<Measure> Measures { get { return measures; } set { measures = value; OnPropertyChanged("Measures"); } }

        private Measure selectedMeasure;
        public Measure SelectedMeasure { get { return selectedMeasure; } set { selectedMeasure = value;OnPropertyChanged("SelectedMeasure"); } }

        private string amount;
        public string Amount { get { return amount; } set { amount = value;OnPropertyChanged("Amount"); } }

        private string search;
        public string Search 
        { 
            get { return search; } 
            set 
            {
                search = value; 
                OnPropertyChanged("Search");
                var name = Names.FirstOrDefault(x  => x.ToLower().StartsWith(Search.ToLower()));
                SelectedItem = null;
                int i = 0;
                Product prod;
                while ((SelectedItem == null) || (i == Items.Count))
                {
                    prod = (Product)Items[i];
                    if(prod != null && prod.Name == name)
                    {
                        SelectedItem = prod;
                    }
                    else { SelectedItem = null; }
                    i++;
                }
            } 
        }

        public ChoiceDialogViewModel(string message, string type) : base(message)
        {
            this.type = type;
            recipesdbContext db = new recipesdbContext();
            Names = new List<string>();
            Measures = new ObservableCollection<Measure>(db.Measures.ToList());
            switch (type)
            {
                case "product":
                    Items = new ObservableCollection<Object>(db.Products.ToList());
                    for (int i = 0; i < Items.Count; i++)
                    {
                        Product temp = (Product)Items[i];
                        Names.Add(temp.Name);
                    }
                    break;
            }
        }

        private LambdaCommand choiceyesCommand;
        public LambdaCommand ChoiceYesCommand
        {
            get
            {
                return choiceyesCommand ??
                    (choiceyesCommand = new LambdaCommand(obj =>
                    {
                        this.CloseDialogWithResult(obj as Window, DialogResult.Yes);
                    },
                    (obj) => 
                    {
                        bool check1 = SelectedItem != String.Empty;
                        int result;
                        bool check2 = Int32.TryParse(Amount, out result);
                        return check1 && check2 && SelectedMeasure!= null;
                    }));
            }
        }
    }
}
