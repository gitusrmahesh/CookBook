using CookBook.Model;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CookBook.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {

        List<Recipe> recipes = new List<Recipe>();
        protected readonly IConfiguration _configuration;
        SqlConnection _connection;

        public RecipeRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public List<Recipe> GetAllRecipes()
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("spGetAllRecipes", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Recipe recipe = new Recipe();
                recipe.RecipeID = int.Parse(reader["RecipeId"].ToString());
                recipe.RecipeName = reader["RecipeName"].ToString();
                recipe.RecipeTypeID = int.Parse(reader["RecipeTypeID"].ToString());
                recipe.Description = reader["Description"].ToString();
                recipe.Source = reader["Source"].ToString();
                recipes.Add(recipe);

            }
            _connection.Close();

            return recipes;

        }
        public Recipe GetRecipeById(int? id)
        {
            Recipe re = new Recipe();
            SqlCommand cmd = new SqlCommand("spGetRecipeById", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            _connection.Open();
            cmd.Parameters.AddWithValue("@RecipeID", id);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                re.RecipeID = int.Parse(dr["RecipeId"].ToString());
                re.RecipeName = dr["RecipeName"].ToString();
                re.RecipeTypeID = int.Parse(dr["RecipeTypeID"].ToString());
                re.Description = dr["Description"].ToString();
                re.Source = dr["Source"].ToString();
            }
            return re;
        }
        public void UpdateRecipe(Recipe recipe)
        {
            SqlCommand cmd = new SqlCommand("spUpdateRecipe", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecipeID", recipe.RecipeID);
            cmd.Parameters.AddWithValue("@RecipeName", recipe.RecipeName);
            cmd.Parameters.AddWithValue("@RecipeTypeID", recipe.RecipeTypeID);
            cmd.Parameters.AddWithValue("@Description", recipe.Description);
            cmd.Parameters.AddWithValue("@Source", recipe.Source);
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
        public void AddRecipe(Recipe recipe)
        {
            SqlCommand cmd = new SqlCommand("spAddRecipe", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecipeId", recipe.RecipeID);
            cmd.Parameters.AddWithValue("@RecipeName", recipe.RecipeName);
            cmd.Parameters.AddWithValue("@RecipeTypeID", recipe.RecipeTypeID);
            cmd.Parameters.AddWithValue("@Description", recipe.Description);
            cmd.Parameters.AddWithValue("@Source", recipe.Source);
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
        public void DeleteRecipe(int? id)
        {
            SqlCommand cmd = new SqlCommand("spDeleteRecipe", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecipeID", id);
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();

        }
    }
}