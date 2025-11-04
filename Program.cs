using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using Newtonsoft.Json.Linq;
using VeganMeal.API;
using VeganMeal.Models;

namespace VeganMeal
{
    public class Program
    {
        public static async Task Main()
        {
            try
            {
                JObject JData = await RezepteApi.GetRezepte();
                await NtfyApi.SendMsg(JData);
                SafeRecipie.Safe(JData);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

