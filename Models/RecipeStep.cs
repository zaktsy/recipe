using System;
using System.Collections.Generic;

namespace recipe
{
    public partial class RecipeStep
    {
        public int Recipeid { get; set; }
        public int Stepnumber { get; set; }
        public byte[]? Photo { get; set; }
        public string? Description { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
