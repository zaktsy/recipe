using System;
using System.Collections.Generic;

namespace recipe
{
    public partial class ProductRecipe
    {
        public int Recipeid { get; set; }
        public int Productid { get; set; }
        public int? Amount { get; set; }
        public int? Mesureid { get; set; }

        public virtual Measure? Mesure { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
