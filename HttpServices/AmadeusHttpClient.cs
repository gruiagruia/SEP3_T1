using System.Text;
using amadeus;
using amadeus.util;
using Domain.Interfaces;
using Domain.Model;
using Newtonsoft.Json.Linq;

namespace HttpServices;

public class AmadeusHttpClient : IAmadeus
{
    private static string apikey = "o0cZMfDVaYNZRvxbX7AAvrsM9AtzlEox";
    private static string apisecret = "6GG1cGCtY7kthWEf";

    public async Task<ICollection<string>> GetAirportsAsync(String keyword)
    {
        ICollection<string> airports = new List<string>();
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/locations?keyword="+keyword);
        try
        {
            string myJsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
            JArray jsonObject = JArray.Parse(myJsonResponse);
            foreach (JObject item in jsonObject) // <-- Note that here we used JObject instead of usual JProperty
            {
                string name = item.GetValue("name").ToString();
                string iataCode = item.GetValue("iataCode").ToString();
                airports.Add(iataCode+": "+name);
            }
        }
        catch (Newtonsoft.Json.JsonReaderException e)
        {
            Console.WriteLine("no airport results");
        }
        client.Dispose();
        return airports;
    }

    public Task<Trip> GetTripAsync()
    {
        try
        {
            Configuration amadeusconfig = Amadeus.builder(apikey, apisecret);
            amadeusconfig.setLoglevel("debug");
            Amadeus amadeus = amadeusconfig.build();
            
            string token = getToken();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/flights?origin=SYD&destination=BKK&departDate=2022-11-01&adults=1");
            Task<HttpResponseMessage> response = client.SendAsync(request);
            string myJsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Result.Content.ReadAsStringAsync().Result).ToString();
            JObject jsonObject = JObject.Parse(myJsonResponse);

            Console.WriteLine(response);
            Console.WriteLine(myJsonResponse);
            Console.WriteLine(jsonObject);
            client.Dispose();
            
            
            amadeus.resources.Location[] locations = amadeus.referenceData.locations.get(Params
                .with("keyword", "LON")
                .and("subType", resources.referenceData.Locations.ANY));


            Console.WriteLine(AmadeusUtil.ArrayToStringGeneric(locations, "\n"));

        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR: " + e.ToString());
        }

        return null;
    }
    
    private static string getToken()
    {
        const string tokenURL = "https://test.api.amadeus.com/v1/security/oauth2/token";
        string postData = $"grant_type=client_credentials&client_id={apikey}&client_secret={apisecret}";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(tokenURL);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");
        request.Content = new StringContent(postData,
            Encoding.UTF8,
            "application/x-www-form-urlencoded");

        Task<HttpResponseMessage> response = client.SendAsync(request);
        if (response.Result.IsSuccessStatusCode)
        {
            string myJsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Result.Content.ReadAsStringAsync().Result).ToString();
            JObject jsonObject = JObject.Parse(myJsonResponse);
            client.Dispose();

            string token = (string)jsonObject["access_token"];
            return token;
        }
        else
        {
            Console.WriteLine("{0} ({1})", (int)response.Result.StatusCode, response.Result.ReasonPhrase);
            return response.Result.ReasonPhrase;
        }
    }
}