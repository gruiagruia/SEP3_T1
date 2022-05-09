using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Interfaces;
using Domain.Model;

namespace HttpServices;

public class UserHttpClient : IUser
{
     public async Task<ICollection<User>> GetAllUsersAsync()
    {
        using HttpClient client = new();
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:8084/users");
        ValidateContent(responseMessage);
        string content = await responseMessage.Content.ReadAsStringAsync();

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {responseMessage.StatusCode}, {content}");
        }
        
        ICollection<User> users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return users;
    }

     public async Task<User> SaveUserAsync(User user)
     {
         using HttpClient client = new();

         var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
         options.Converters.Add(new JsonStringEnumConverter());

         string userAsJson = JsonSerializer.Serialize(user, options);

         StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

         HttpResponseMessage response = await client.PostAsync("http://localhost:8080/users", content);
         string responseContent = await response.Content.ReadAsStringAsync();
         

         if (!response.IsSuccessStatusCode)
         {
             throw new Exception($"Error: {response.StatusCode}, {responseContent}");
         }
        
         User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
             { PropertyNameCaseInsensitive = true })!;
         Console.WriteLine(returned + " pe client");
         
         return returned;
     }

    public async Task DeleteUserAsync(User user)
    {
        using HttpClient client = new();

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter());

        string userAsJson = JsonSerializer.Serialize(user, options);

        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage responseMessage = await client.DeleteAsync($"http://localhost:8084/users/{user.Id}");
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        
        if (!responseMessage.IsSuccessStatusCode)
        { throw new Exception($"Error: {responseMessage.StatusCode}, {responseContent}"); }

        
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        using HttpClient client = new();
        
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter());
        
        string userAsJson = JsonSerializer.Serialize(user, options);

        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PutAsync($"http://localhost:8080/users/{user.Id}", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        { throw new Exception($"Error: {response.StatusCode}, {responseContent}"); }
        
        User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true})!;

        return returned;
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }


    public async Task<User> GetUserById(int id)
    {
        using HttpClient client = new();
        
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8084/users/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        { throw new Exception($"Error: {response.StatusCode}, {responseContent}"); }
        
        User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true})!;

        return returned;
    }
    
    public async Task<User> GetUserByEmail(string email)
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
    
    public HttpContent ValidateContent(HttpResponseMessage response)
    {
        if(string.IsNullOrEmpty(response.Content?.ReadAsStringAsync().Result))
        {
            return response.Content= new StringContent("null",Encoding.UTF8, MediaTypeNames.Application.Json);
        }
        else
        {
            return response.Content;
        }
    }
}