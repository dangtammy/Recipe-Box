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
    public void OverrideBool_SameStudent_ReturnsEqual()
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
    // [Fact]
    // public void AddCourse_OneStudent_CourseAddedToJoinTable()
    // {
    //   //Arrange
    //   Student testStudent = new Student ("Joe", "Fall 2017");
    //   testStudent.Save();
    //   Course testCourse = new Course("HIST101", "United States History to 1877");
    //   testCourse.Save();
    //   testStudent.AddCourse(testCourse);
    //
    //   //Act
    //   List<Course> output = testStudent.GetCourse();
    //   List<Course> verify = new List<Course>{testCourse};
    //
    //   //Assert
    //   Assert.Equal(verify, output);
    // }

  }
}
