﻿using System;
using System.Collections.Generic;

namespace recipe
{
    public partial class Peculiarity
    {
        public Peculiarity()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
