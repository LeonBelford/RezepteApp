using System.Runtime.CompilerServices;
using Newtonsoft.Json;
namespace VeganMeal.API;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public static class RezepteApi
{
	private static int Id { get; set; }
	private static int[] OldIds = new int[400];

	static HttpClient client = new HttpClient();

	public static async Task<JObject> GetRezepte()
	{
		Id = RandInt();
		JObject JData;
		string filePath = System.IO.Directory.GetCurrentDirectory();
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

		using (var response = await client.SendAsync(request))
		{
			// Creats body where your api call data is stored, also creates a JObjekt :D 
			response.EnsureSuccessStatusCode();
			var body = await response.Content.ReadAsStringAsync();
			JData = JObject.Parse(body);
			Console.WriteLine(JData);
			File.WriteAllText("output.json", JData.ToString());
		}
		return JData;
	}
	private static int RandInt()
	{
		int newID;
		Random random = new Random();
		//  int TempId = random.Next(1, 381);

		while (true)
		{
			int TempId = random.Next(1, 381);

			if (!OldIds.Contains(TempId))
			{
				OldIds[Id] = TempId;
				newID = TempId;
				break;
			}
		}
		return newID;
	}
}


