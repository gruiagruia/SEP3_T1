using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Interfaces;
using Domain.Model;

namespace HttpServices;

public class UserHttpClient : IUser
{

    public async Task SaveUserAsync(User user)
     {
         using HttpClient client = new();

         var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
         options.Converters.Add(new JsonStringEnumConverter());

         string userAsJson = JsonSerializer.Serialize(user, options);

         StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

         HttpResponseMessage response = await client.PostAsync("http://localhost:8080/users", content);
         string responseContent = await response.Content.ReadAsStringAsync();
         

         if (response.StatusCode == HttpStatusCode.BadRequest)
         {
             throw new Exception("Email already exists in system.");
         }
        
         if (response.StatusCode == HttpStatusCode.Conflict)
         {
             throw new Exception("Invalid password. Password must:"+
                                 "\n -Be between 8 and 20 characters in length."+
                                 "\n -Include at least one uppercase character."+
                                 "\n -Include at least one lowercase character."+
                                 "\n -Include at least one number."+
                                 "\n -Include one special character among @ # $ %.");
         }

         if (response.StatusCode == HttpStatusCode.InternalServerError)
         {
             throw new Exception("Email already exists in system.");
         }
         
         if (response.StatusCode == HttpStatusCode.OK)
         {
             throw new Exception("Registration was successful.");
         }
        
         String returned = JsonSerializer.Deserialize<String>(responseContent, new JsonSerializerOptions
             { PropertyNameCaseInsensitive = true })!;
         Console.WriteLine(returned);
     }

    public async Task DeleteUserAsync(User user)
    {
        using HttpClient client = new();

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter());

        string userAsJson = JsonSerializer.Serialize(user, options);

        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage responseMessage = await client.DeleteAsync($"http://localhost:8080/users/{user.Id}");
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        
        if (!responseMessage.IsSuccessStatusCode)
        { throw new Exception($"Error: {responseMessage.StatusCode}, {responseContent}"); }

        
    }

    public async Task UpdateUserAsync(User user)
    {
        using HttpClient client = new();
        
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter());
        
        string userAsJson = JsonSerializer.Serialize(user, options);

        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PutAsync($"http://localhost:8080/users/{user.Id}", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("User could not be updated.");
        }
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        using HttpClient client = new();
        
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/users/{email}");
        string responseContent = await response.Content.ReadAsStringAsync();
        
        
        if (!response.IsSuccessStatusCode)
        { throw new Exception($"Error: {response.StatusCode}, {responseContent}"); }
        
        User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true})!;
        

        return returned;
    }
    
    public async Task<User> GetUserByIdAsync(int id)
    {
        using HttpClient client = new();
        
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/users/find/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();

        
        
        if (!response.IsSuccessStatusCode)
        { throw new Exception($"Error: {response.StatusCode}, {responseContent}"); }
        
        User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true})!;

        return returned;
    }
    
}