using Domain.Model;

namespace Domain.Interfaces;

public interface IUser
{
    public Task<ICollection<User>> GetAllUsersAsync();
    public Task<User> SaveUserAsync(User user);
    public Task DeleteUserAsync(User user);
    public Task<User> UpdateUserAsync(User user);
    public Task<User> GetUserByEmailAsync(string email);
}