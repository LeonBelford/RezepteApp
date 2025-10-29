using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using VeganMeal.API;

namespace VeganMeal
{
    public class Program
    {
        public static async Task Main()
        {
            await RezepteApi.GetRezepte();
        }


    }


}