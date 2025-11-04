namespace VeganMeal.API;

using VeganMeal.Models;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class RezepteApi
{
	private static int Id { get; set; }
	private static int Length { get; set; } = 380;
	private static string filePath = System.IO.Directory.GetCurrentDirectory();
 	private	static HttpClient client = new HttpClient();


	public static async Task<JObject> GetRezepte()
	{
		Id = RandInt();
		JObject JData = new JObject();

		HttpRequestMessage request = new HttpRequestMessage
		{
			Method = HttpMethod.Get,
			RequestUri = new Uri($"https://the-vegan-recipes-db.p.rapidapi.com/{Id}"),
			Headers =
		{
			{ "x-rapidapi-key", "9234c4240bmsh63001e0ed959462p114753jsn63b15de86a5a" },
			{ "x-rapidapi-host", "the-vegan-recipes-db.p.rapidapi.com" },
		},
		};

		try //trys to call the API, if succesfull the JData objekt will return the content :3
		{
			using (var response = await client.SendAsync(request))
			{
				// Creats body where your api call data is stored, also creates a JObjekt :D 
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				JData = JObject.Parse(body);
				Console.WriteLine(JData);
			}

		}
		catch (Exception e) // if the Api call fails local FIle "output.json" will be loadet
		{
			Console.WriteLine(e.Message);
			try
			{
				JData = JObject.Parse(File.ReadAllText(@$"{filePath}/output.json"));
				Console.WriteLine($"!!! using local safed file !!! \n{JData["id"]} | {JData["title"]}");

			}
			catch (Exception E)
			{
				Console.WriteLine(E.Message);
			}
		}
		return JData;
	}
	
	private static int RandInt()
	{
		// Obvi ig, this Creats a random number and stores it in a array so for now there are no doubble Recipies :3
		// lowkey need to change that in the future lol ^^
		Random random = new Random();
		int newID = random.Next(1, Length);
		
		return newID;
	}
}


