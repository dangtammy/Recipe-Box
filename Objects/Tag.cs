using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RecipeBox
{
  public class Tag
  {
    private int _id;
    private string _name;

    public Tag(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM tags;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Tag> GetAll()
    {
      List<Tag> allTags = new List<Tag>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tags;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Tag newTag = new Tag(name, id);
        allTags.Add(newTag);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allTags;
    }

    public override bool Equals(System.Object randomTag)
    {
      if(!(randomTag is Tag))
      {
        return false;
      }
      else
      {
        Tag newTag = (Tag) randomTag;
        bool idEquality = (this.GetId() == newTag.GetId());
        bool nameEquality = (this.GetName() == newTag.GetName());
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO tags (name) OUTPUT INSERTED.id VALUES (@TagName);", conn);
      cmd.Parameters.Add(new SqlParameter("@TagName", this.GetName()));
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

    public static Tag Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tags WHERE id = @TagId;", conn);
      cmd.Parameters.Add(new SqlParameter("@TagId", id));
      SqlDataReader rdr = cmd.ExecuteReader();

      int studentId = 0;
      string studentName = null;

      while (rdr.Read())
      {
        studentId = rdr.GetInt32(0);
        studentName = rdr.GetString(1);
      }

      Tag foundTag = new Tag(studentName, studentId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundTag;
    }

    // public static List<Tag> SearchName(string name)
    // {
    //   List<Tag> foundTags = new List<Tag>{};
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE name = @TagName", conn);
    //   cmd.Parameters.Add(new SqlParameter("@TagName", name));
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   while (rdr.Read())
    //   {
    //     int studentId = rdr.GetInt32(0);
    //     string studentName = rdr.GetString(1);
    //     string studentDate = rdr.GetString(2);
    //     Tag foundTag = new Tag(studentName, studentDate, studentId);
    //     foundTags.Add(foundTag);
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
    //   return foundTags;
    // }
    //

    public void AddRecipe(Recipe newRecipe)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO recipes_tags (recipe_id, tag_id) VALUES (@TagId, @RecipeId);", conn);
      cmd.Parameters.Add(new SqlParameter("@TagId", this.GetId()));
      cmd.Parameters.Add(new SqlParameter("@RecipeId", newRecipe.GetId()));
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Recipe> GetRecipe()
    {
      List<Recipe> allRecipes = new List<Recipe>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT recipes.* FROM tags JOIN recipes_tags ON (tags.id = recipes_tags.recipe_id) JOIN recipes ON (recipes.id = recipes_tags.tag_id) WHERE tags.id = @TagId;", conn);
      cmd.Parameters.Add(new SqlParameter("@TagId", this.GetId()));
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        string RecipeName = rdr.GetString(1);
        string RecipeIngredients = rdr.GetString(2);
        string RecipeInstructions = rdr.GetString(3);
        int RecipeRate = rdr.GetInt32(4);
        string RecipeTime = rdr.GetString(5);

        Recipe newRecipe = new Recipe(RecipeName, RecipeIngredients, RecipeInstructions, RecipeRate, RecipeTime, Id);
        allRecipes.Add(newRecipe);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allRecipes;
    }
  }
}
