using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace RecipeBox
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
              List<Recipe> allRecipes = Recipe.GetAll();
                return View["index.cshtml", allRecipes];
            };
            Get["/new-recipe"] = _ => {
              return View["recipe_form.cshtml"];
            };

            Post["/recipe/added"] = _ =>{
              Recipe newRecipe = new Recipe(Request.Form["recipe-name"], "", "", 0, "");
              newRecipe.Save();
              List<String> IngredientNames = new List<String>{};
              string Ingredients = newRecipe.GetIngredients();
              if (Ingredients != "")
              {
                string[] IngredientsArray = Ingredients.Split(' ');
                foreach(string name in IngredientsArray)
                {
                  IngredientNames.Add(name.Substring(1, name.Length-1));
                }
              }
              else
              {
                IngredientNames = new List<String>{"You have no ingredients so far"};
              }
              List<String> Instructions = new List<String>{};
              string InstructionsString = newRecipe.GetInstructions();
              if (InstructionsString != "")
              {
                string[]  InstructionsArray =  InstructionsString.Split(' ');
                foreach(string name in  InstructionsArray)
                {
                    Console.WriteLine(name);
                    Instructions.Add(name.Substring(1, name.Length-2));
                }
              }
              else
              {
                Instructions = new List<String>{"You have no steps of instructions so far"};
              }
              Dictionary<string, object> model = new Dictionary<string, object>{{"recipe", newRecipe},{"ingredients", IngredientNames},{"instruction", Instructions}};
              return View["recipe.cshtml", model];
            };

            Post["/{id}/ingredient/added"] = parameters =>{
              Recipe thisRecipe = Recipe.Find(parameters.id);
              thisRecipe.AddIngredient(Request.Form["ingredient-name"]);
              List<String> IngredientNames = new List<String>{};
              string Ingredients = thisRecipe.GetIngredients();
              string[] IngredientsArray = Ingredients.Split(' ');
              Console.WriteLine(IngredientsArray[0]);
              foreach(string name in IngredientsArray)
              {
                if ((!(String.IsNullOrEmpty(name))))
                {
                  IngredientNames.Add(name.Substring(1, name.Length-2));
                }
              }
              List<String> Instructions = new List<String>{};
              string  InstructionsString = thisRecipe.GetInstructions();
              string[]  InstructionsArray =  InstructionsString.Split('|');
              Console.WriteLine( InstructionsArray[0]);
              foreach(string name in  InstructionsArray)
              {
                if ((!(String.IsNullOrEmpty(name))))
                {
                  Instructions.Add(name.Substring(1, name.Length-2));
                }
              }
              Dictionary<string, object> model = new Dictionary<string, object>{{"recipe", thisRecipe},{"ingredients", IngredientNames},{"instruction", Instructions}};
              return View["recipe.cshtml", model];
            };

            Post["/{id}/instructions/added"] = parameters =>
            {
              Recipe thisRecipe = Recipe.Find(parameters.id);
              thisRecipe.AddInstruction(Request.Form["instructions"]);
              List<String> IngredientNames = new List<String>{};
              string Ingredients = thisRecipe.GetIngredients();
              string[] IngredientsArray = Ingredients.Split(' ');
              foreach(string name in IngredientsArray)
              {
                if ((!(String.IsNullOrEmpty(name))))
                {
                  IngredientNames.Add(name.Substring(1, name.Length-2));
                }
              }
              List<String> Instructions = new List<String>{};
              string  InstructionsString = thisRecipe.GetInstructions();
              string[]  InstructionsArray =  InstructionsString.Split('|');
              foreach(string name in  InstructionsArray)
              {
                if ((!(String.IsNullOrEmpty(name))))
                {
                  Instructions.Add(name.Substring(1, name.Length-2));
                }
              }
              Dictionary<string, object> model = new Dictionary<string, object>{{"recipe", thisRecipe},{"ingredients", IngredientNames},{"instruction", Instructions}};
              return View["recipe.cshtml", model];
            };
        }
    }
}
