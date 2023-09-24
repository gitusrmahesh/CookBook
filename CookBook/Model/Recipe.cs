using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Model
{
    public class Recipe
    {
        public int? RecipeID { get; set; }
        public int? Category { get; set; }
        public String RecipeName { get; set; }
        public String RecipeType { get; set; }
        public string Source { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
    }
}
