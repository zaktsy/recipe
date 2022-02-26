using Microsoft.Win32;
using recipe.Infrastructure.dialogs.DialogService;
using recipe.Infrastructure.dialogs.DialogYesNo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace recipe.Infrastructure.dialogs.EditProductDialog
{
    internal class EditProductDialogViewModel: DialogYesNoViewModel
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string description;
        public string Description { get { return description; } set { description = value; } }

        //private string photo;
        //public string Photo { get { return photo; } set { photo = value; } }

        public EditProductDialogViewModel(string message, string name, string description /*string photo*/) : base(message)
        {
            this.Name = name;
            this.Description = description;
            //this.Photo = photo;
        }

        private LambdaCommand productyesCommand;
        public LambdaCommand ProductYesCommand
        {
            get
            {
                return productyesCommand ??
                    (productyesCommand = new LambdaCommand(obj =>
                    {
                        this.CloseDialogWithResult(obj as Window, DialogResult.Yes);
                    },
                    (obj) =>
                    {
                        string st = Name.Replace(" ", "");
                        return st !=null && st.Length > 0;
                    }));
            }
        }

        //private LambdaCommand changePhoto;
        //public LambdaCommand ChangePhoto
        //{
        //    get
        //    {
        //        return changePhoto ??
        //            (changePhoto = new LambdaCommand(obj =>
        //            {
        //                var dlg = new OpenFileDialog();
        //                dlg.Filter = "Файлы JPEG (*.jpeg, *.jpg)|*.jpeg;*.jpg| Файлы PNG (*.png)|*.png";
        //                if (dlg.ShowDialog() == true)
        //                {
        //                    try
        //                    {

        //                    }
        //                    catch
        //                    {
        //                        Console.WriteLine();
        //                    }
        //                }
        //                System.IO.File.Copy("откуда", "куда");
        //            }));
        //    }
        //}
    }
}
