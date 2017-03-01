using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RecipeBox
{
  public class Recipe
  {
    private int _id;
    private string _name;
    private string _ingredients;
    private string _instructions;
    private int _rate;
    private string _time;

    public Recipe(string Name, string Ingredients, string Instructions, int Rate, string Time, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _ingredients = Ingredients;
      _instructions = Instructions;
      _rate = Rate;
      _time = Time;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetIngredients()
    {
      return _ingredients;
    }

    public string GetInstructions()
    {
      return _instructions;
    }
    public int GetRate()
    {
      return _rate;
    }
    public string GetTime()
    {
      return _time;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM recipes;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Recipe> GetAll()
    {
      List<Recipe> allrecipes = new List<Recipe>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM recipes;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string ingredients = rdr.GetString(2);
        string instructions = rdr.GetString(3);
        int rate = rdr.GetInt32(4);
        string time = rdr.GetString(5);
        Recipe newRecipe = new Recipe(name, ingredients, instructions, rate, time, id);
        allrecipes.Add(newRecipe);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allrecipes;
    }

    public override bool Equals(System.Object randomRecipe)
    {
      if(!(randomRecipe is Recipe))
      {
        return false;
      }
      else
      {
        Recipe newRecipe = (Recipe) randomRecipe;
        bool idEquality = (this.GetId() == newRecipe.GetId());
        bool nameEquality = (this.GetName() == newRecipe.GetName());
        bool ingredientsEquality = (this.GetIngredients() == newRecipe.GetIngredients());
        bool instructionsEquality = (this.GetInstructions() == newRecipe.GetInstructions());
        bool rateEquality = (this.GetRate() == newRecipe.GetRate());
        bool timeEquality = (this.GetTime() == newRecipe.GetTime());
        return (idEquality && nameEquality && ingredientsEquality && instructionsEquality && rateEquality && timeEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO recipes (name, ingredients, instructions, rate, time) OUTPUT INSERTED.id VALUES (@RecipeName, @RecipeIngredients, @RecipeInstructions, @RecipeRate, @RecipeTime);", conn);
      cmd.Parameters.Add(new SqlParameter("@RecipeName", this.GetName()));
      cmd.Parameters.Add(new SqlParameter("@RecipeIngredients", this.GetIngredients()));
      cmd.Parameters.Add(new SqlParameter("@RecipeInstructions", this.GetInstructions()));
      cmd.Parameters.Add(new SqlParameter("@RecipeRate", this.GetRate()));
      cmd.Parameters.Add(new SqlParameter("@RecipeTime", this.GetTime()));

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Recipe Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM recipes WHERE id = @RecipeId;", conn);
      cmd.Parameters.Add(new SqlParameter("@RecipeId", id.ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();

      int RecipeId = 0;
      string RecipeName = null;
      string RecipeIngredients = null;
      string RecipeInstructions = null;
      int RecipeRate = 0;
      string RecipeTime = null;

      while (rdr.Read())
      {
        RecipeId = rdr.GetInt32(0);
        RecipeName = rdr.GetString(1);
        RecipeIngredients = rdr.GetString(2);
        RecipeInstructions = rdr.GetString(3);
        RecipeRate = rdr.GetInt32(4);
        RecipeTime = rdr.GetString(5);
      }

      Recipe foundRecipe = new Recipe(RecipeName, RecipeIngredients, RecipeInstructions, RecipeRate, RecipeTime, RecipeId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundRecipe;
    }

    // public static List<Recipe> SearchName(string name)
    // {
    //   List<Recipe> foundRecipes = new List<Recipe>{};
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT * FROM recipes WHERE name = @RecipeName", conn);
    //   cmd.Parameters.Add(new SqlParameter("@RecipeName", name));
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   while (rdr.Read())
    //   {
    //     int RecipeId = rdr.GetInt32(0);
    //     string RecipeName = rdr.GetString(1);
    //     string RecipeDate = rdr.GetString(2);
    //     Recipe foundRecipe = new Recipe(RecipeName, RecipeDate, RecipeId);
    //     foundRecipes.Add(foundRecipe);
    //   }
    //
    //   if(rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //
    //   return foundRecipes;
    // }

    // public void AddTag(Tag newTag)
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlCommand cmd = new SqlCommand("INSERT INTO recipes_tags (Recipe_id, tag_id) VALUES (@RecipeId, @TagId);", conn);
    //   cmd.Parameters.Add(new SqlParameter("@RecipeId", this.GetId()));
    //   cmd.Parameters.Add(new SqlParameter("@TagId", newTag.GetId()));
    //   cmd.ExecuteNonQuery();
    //
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }
    //
    // public List<Tag> GetTag()
    // {
    //   List<Tag> allTags = new List<Tag>{};
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT tags.* FROM recipes JOIN recipes_tags ON (recipes.id = recipes_tags.recipe_id) JOIN tags ON (tags.id = recipes_tags.tag_id) WHERE recipes.id = @RecipeId;", conn);
    //   cmd.Parameters.Add(new SqlParameter("@RecipeId", this.GetId().ToString()));
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   while (rdr.Read())
    //   {
    //     int Id = rdr.GetInt32(0);
    //     string tagName = rdr.GetString(1);
    //     Tag newTag = new Tag(tagName, Id);
    //     allTags.Add(newTag);
    //   }
    //
    //   if(rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //
    //   return allTags;
    // }
  }
}
