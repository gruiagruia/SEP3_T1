using Domain.Model;

namespace Domain.Interfaces;

public interface IUser
{
    public Task SaveUserAsync(User user);
    public Task DeleteUserAsync(User user);
    public Task UpdateUserAsync(User user);
    public Task<User> GetUserByEmailAsync(string email);
}