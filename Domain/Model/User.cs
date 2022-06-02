using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class User
{
    public int Id { get; set; }
    
    [Required] public string FirstName { get; set; }
   
    [Required] public string LastName { get; set; }
    
    [Required] public string Email { get; set; }
    
    [Required] public string Password { get; set; }
    public string Role { get; set; }
    
    public User(){}

    public User(string firstName, string lastName, string email, string password, string role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
    }

    public User(int id, string firstName, string lastName, string email, string password, string role)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
    }
    
    
    
    
}