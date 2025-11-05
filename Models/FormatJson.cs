using Newtonsoft.Json.Linq;

namespace VeganMeal.Models
{
    public class FormatJson
    {
        private static string AllIngredients { get; set; } = "";
        private static string allSteps { get; set; } = "";
        public static string FormatToNtfy(JObject JData)
        {
            JArray methodArray = JData["method"] as JArray;

            foreach (JObject step in methodArray)
            {
                foreach (var property in step.Properties())
                {
                    allSteps += $"{property.Name}:\n {property.Value}\n\n";
                }
            }
            foreach (JToken ingredient in JData["ingredients"])
            {
                AllIngredients += $"- {ingredient}\n";
            }

            string Message = $"description :3\n{JData["description"].ToString().ToLower()}\n"
                       + $"\nwhat we need :p\n{AllIngredients.ToLower()}"
                       + $"\nhow to make it :c \n{allSteps.ToLower()}";


            return Message;
        }
        public static string FormatToMd(JObject JData)
        {
            // Formats the Recipie String cute and adds the syntax for the md file UwU 
            string Recipie = $"# {JData["title"].ToString().ToLower()}\n  \t##  difficulty :o {JData["difficulty"].ToString().ToLower()}  |  ID:{JData["id"]}\n\n"
                        + $"**description :3**\n{JData["description"].ToString().ToLower()}\n"
                           + $"\n**what we need :p**\n{AllIngredients.ToLower()}"
                           // + $" \nZutaten:{JData["ingredients"].ToString().Replace("]", null).Replace("[", null)}"
                           + $"\n**how to make it :c** \n{allSteps.ToLower()}";               
            return Recipie;
        }
    }
}