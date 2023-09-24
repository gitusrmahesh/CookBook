using CookBook.Model;
using System.Collections.Generic;

namespace CookBook.Repositories
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAllRecipes();
        Recipe GetRecipeById(int? id);
        void UpdateRecipe(Recipe recipe);

        void AddRecipe(Recipe recipe);
        void DeleteRecipe(int? id);

    }
}
