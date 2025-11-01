
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using ntfy;
using ntfy.Actions;
using ntfy.Requests;
using System.Text.Json;
using System.Text.Json.Nodes;



namespace VeganMeal.API
{

    public static class NtfyApi
    {


        // Create a new client

        // Publish a message to the "test" topic
        public static async Task SendMsg(JObject JData)
        {
           
            string allSteps = "";
            var methodArray = JData["method"] as JArray;
            foreach (JObject step in methodArray)
            {
                foreach (var property in step.Properties())
                {
                    allSteps += $"{property.Name}:\n {property.Value}\n\n";
                }
            }
            



            var client = new Client("https://ntfy.sh");
            var message = new SendingMessage
            {
                Title = $"{JData["title"]}"
                      + $" Difficulty:{JData["difficulty"]}    ID:{JData["id"]}",

                Message = $" Beschreibung: {JData["description"]}"
                        + $" \nZutaten:{JData["ingredients"].ToString().Replace("]", null).Replace("[", null)}"
                        + $"\nZubereitung: \n{allSteps}",


                Actions = new ntfy.Actions.Action[]
                    {
                new Broadcast("label")
                {
                },
                new View("label2", new Uri("https://google.com"))
                {
                }
                    }
            };
            await client.Publish("VeganRecipiesApi", message);
        }
    }
}
