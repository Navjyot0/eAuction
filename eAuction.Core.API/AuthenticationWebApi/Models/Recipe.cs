using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AuthenticationWebApi.Models
{
    public class Recipe
    {
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Ingredients")]
        public List<string> Ingredients { get; set; }

        [BsonElement("PrepTimeInMinutes")]
        public int PrepTimeInMinutes { get; set; }

        public Recipe(string name, List<string> ingredients, int prepTime)
        {
            this.Name = name;
            this.Ingredients = ingredients;
            this.PrepTimeInMinutes = prepTime;
        }

        //public static List<Recipe> GetRecipes()
        //{
        //    return new List<Recipe>()
        //    {
        //        new Recipe("elotes", new List<string>(){"corn", "mayonnaise", "cotija cheese", "sour cream", "lime" }, 35),
        //        new Recipe("loco moco", new List<string>(){"ground beef", "butter", "onion", "egg", "bread bun", "mushrooms" }, 54),
        //        new Recipe("patatas bravas", new List<string>(){"potato", "tomato", "olive oil", "onion", "garlic", "paprika" }, 80),
        //        new Recipe("fried rice", new List<string>(){"rice", "soy sauce", "egg", "onion", "pea", "carrot", "sesame oil" }, 40),
        //    };
        //}
    }
}
