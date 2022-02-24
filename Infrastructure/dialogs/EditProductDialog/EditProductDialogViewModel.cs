using recipe.Infrastructure.dialogs.DialogYesNo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe.Infrastructure.dialogs.EditProductDialog
{
    internal class EditProductDialogViewModel: DialogYesNoViewModel
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string description;
        public string Description { get { return description; } set { description = value; } }

        private string photo;
        public string Photo { get { return photo; } set { photo = value; } }

        public EditProductDialogViewModel(string message, string name, string description, string photo) : base(message)
        {
            this.Name = name;
            this.Description = description;
            this.Photo = photo;
        }
    }
}
