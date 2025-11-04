using System.Text;
using Newtonsoft.Json.Linq;

namespace VeganMeal.Models
{
    public class SafeRecipie
    {
        private static string filePath = System.IO.Directory.GetCurrentDirectory();
        
        
        public static void Safe(JObject JData)
        {
            try
            {
                File.WriteAllText("output.json", JData.ToString());
                //Copys the Json File to Local Folder
                using (FileStream fs = File.Create($"{filePath}/GetIt/{JData["id"]}_{JData["title"]}_Formated.json"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(JData.ToString());
                    fs.Write(info, 0, info.Length);
                }

                //Creats .MD File of Reccipie ><
                using (FileStream fs = File.Create($"{filePath}/GetIt/{JData["id"]}_{JData["title"]}_Rezept.md"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(FormatJson.FormatToMd(JData));
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}