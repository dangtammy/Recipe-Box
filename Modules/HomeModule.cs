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
              Console.WriteLine("ingredient-name");
              thisRecipe.AddIngredient(Request.Form["ingredient-name"]);
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
            Post["/recipe/{id}"] = parameters => {
              Recipe thisRecipe = Recipe.Find(parameters.id);
              thisRecipe.Update(null, null, null, int.Parse(Request.Form["star"]), Request.Form["time"]);
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
              Dictionary<string, object> Model = new Dictionary<string, object>{};
              Model.Add("recipe", thisRecipe);
              Model.Add("ingredients", IngredientNames);
              Model.Add("instructions", Instructions);
              Model.Add("rate", thisRecipe.GetRate());
              Model.Add("time", thisRecipe.GetTime());
              return View["recipe-info.cshtml", Model];
            };
            Delete["/deleteall"] = _ => {
              Recipe.DeleteAll();
              List<Recipe> allRecipes = Recipe.GetAll();
                return View["index.cshtml", allRecipes];
            };
            Get["/recipe/{id}/info"] = parameters => {
              Recipe thisRecipe = Recipe.Find(parameters.id);
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
              Dictionary<string, object> Model = new Dictionary<string, object>{};
              Model.Add("recipe", thisRecipe);
              Model.Add("ingredients", IngredientNames);
              Model.Add("instructions", Instructions);
              Model.Add("rate", thisRecipe.GetRate());
              Model.Add("time", thisRecipe.GetTime());
              return View["recipe-info.cshtml", Model];
            };

            Delete["/recipe/{id}/delete"] = parameters => {
              Recipe thisRecipe = Recipe.Find(parameters.id);
              thisRecipe.Delete();
              List<Recipe> allRecipes = Recipe.GetAll();
              return View["index.cshtml", allRecipes];
            };

            Get["/recipe/{id}/update"] = parameters => {
              Recipe thisRecipe = Recipe.Find(parameters.id);
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
              Dictionary<string, object> Model = new Dictionary<string, object>{};
              Model.Add("recipe", thisRecipe);
              Model.Add("ingredients", IngredientNames);
              Model.Add("instructions", Instructions);
              Model.Add("rate", thisRecipe.GetRate());
              Model.Add("time", thisRecipe.GetTime());
              return View["recipe_update_form.cshtml", Model];
            };
            Patch["/{id}/update/ingredient/{IngredientName}"] = parameters => {
              Recipe thisRecipe = Recipe.Find(parameters.id);
              List<String> IngredientNames = new List<String>{};
              string Ingredients = thisRecipe.GetIngredients();
              string[] IngredientsArray = Ingredients.Split(' ');
              foreach(string name in IngredientsArray)
              {
                if (!((String.IsNullOrEmpty(name))||(name.Substring(1, name.Length-2) == (string)parameters.IngredientName)))
                {
                  IngredientNames.Add(name.Substring(1, name.Length-2));
                }
              }
              thisRecipe.AddIngredients(IngredientNames);
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
              Dictionary<string, object> Model = new Dictionary<string, object>{};
              Model.Add("recipe", thisRecipe);
              Model.Add("ingredients", IngredientNames);
              Model.Add("instructions", Instructions);
              Model.Add("rate", thisRecipe.GetRate());
              Model.Add("time", thisRecipe.GetTime());
              return View["recipe_update_form.cshtml", Model];
            };
        }
    }
}
