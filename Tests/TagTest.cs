using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;


namespace RecipeBox
{
  public class TagTest: IDisposable
  {
    public TagTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=recipebox_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Tag.DeleteAll();
      Recipe.DeleteAll();
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_ZeroOutput()
    {
      //Arrange, Act
      int result = Tag.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void OverrideBool_SameTag_ReturnsEqual()
    {
      //Arrange, Act
      Tag tag1 = new Tag ("Italian");
      Tag tag2 = new Tag ("Italian");

      //Assert
      Assert.Equal(tag1, tag2);
    }

    [Fact]
    public void Save_OneTag_TagSavedToDatabase()
    {
      //Arrange
      Tag testTag = new Tag ("Japanese");

      //Act
      testTag.Save();
      List<Tag> output = Tag.GetAll();
      List<Tag> verify = new List<Tag>{testTag};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void Save_OneTag_TagSavedWithCorrectID()
    {
      //Arrange
      Tag testTag = new Tag ("Southern");
      testTag.Save();
      Tag savedTag = Tag.GetAll()[0];

      //Act
      int output = savedTag.GetId();
      int verify = testTag.GetId();

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void SaveGetAll_ManyTags_ReturnListOfTags()
    {
      //Arrange
      Tag tag1 = new Tag ("Chinese");
      tag1.Save();
      Tag tag2 = new Tag ("Japanese");
      tag2.Save();

      //Act
      List<Tag> output = Tag.GetAll();
      List<Tag> verify = new List<Tag>{tag1, tag2};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void Find_OneTagId_ReturnTagFromDatabase()
    {
      //Arrange
      Tag testTag = new Tag ("French");
      testTag.Save();

      //Act
      Tag foundTag = Tag.Find(testTag.GetId());

      //Assert
      Assert.Equal(testTag, foundTag);
    }

    // [Fact]
    // public void SearchName_Name_ReturnStudentFromDatabase()
    // {
    //   //Arrange
    //   Student testStudent = new Student ("Joe", "Fall 2017");
    //   testStudent.Save();
    //
    //   //Act
    //   List<Student> output = Student.SearchName("Joe");
    //   List<Student> verify = new List<Student>{testStudent};
    //
    //   //Assert
    //   Assert.Equal(verify, output);
    // }
    //
    // [Fact]
    // public void SearchEnrollDate_EnrollDate_ReturnStudentFromDatabase()
    // {
    //   //Arrange
    //   Student testStudent = new Student ("Joe", "Fall 2017");
    //   testStudent.Save();
    //
    //   //Act
    //   List<Student> output = Student.SearchEnrollDate("Fall 2017");
    //   List<Student> verify = new List<Student>{testStudent};
    //
    //   //Assert
    //   Assert.Equal(verify, output);
    // }
    //
    [Fact]
    public void AddRecipe_OneTag_RecipeAddedToJoinTable()
    {
      //Arrange
      Tag testTag = new Tag ("Japanese");
      testTag.Save();
      Recipe testRecipe = new Recipe("Spaghetti", "<Pasta, <Marinara Sauce", "Boil water, cook pasta, strain pasta, add sauce", 5, "30 mins");
      testRecipe.Save();
      testTag.AddRecipe(testRecipe);

      //Act
      List<Recipe> output = testTag.GetRecipe();
      List<Recipe> verify = new List<Recipe>{testRecipe};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void Test_DeleteTag_DeleteTagFromDatabase()
    {
      //Arrange
      Tag testTag = new Tag("Chinese");

      //Act
      testTag.Save();
      testTag.Delete();

      //Assert
      List<Tag> expectedResult = new List<Tag>{};
      List<Tag> actualResult = Tag.GetAll();

      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Test_Update_UpdateTagInDatabase()
    {
      Tag testTag = new Tag("Chinese");
      testTag.Save();

      string newTagName ="Chicken Soup";

      testTag.Update(newTagName);
      Tag actualResult = testTag;
      Tag expectedResult = new Tag(newTagName, testTag.GetId());

      Assert.Equal(expectedResult,actualResult);
    }

  }
}
