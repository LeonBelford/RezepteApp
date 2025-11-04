using VeganMeal.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
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
        public static async Task SendMsg(JObject JData)
        {
            var client = new Client("https://ntfy.sh");
            var message = new SendingMessage
            {
                Title = $"{JData["title"].ToString().ToLower()}  |  difficulty :o {JData["difficulty"].ToString().ToLower()}  |  ID:{JData["id"]}\n\n",

                Message = FormatJson.FormatToNtfy(JData),

                Actions = new ntfy.Actions.Action[]
                    {
                        new View("Img", new Uri("https://www.youtube.com/watch?v=E4WlUXrJgy4"))
                        {
                       }
                    }
            };

            // await client.Publish("VeganRecipiesApi", message);
        }
    }
}
