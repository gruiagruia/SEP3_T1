using Domain.Model;

namespace Domain.Interfaces;

public interface IAuth
{
    public Task<ICollection<User>> GetAllUsersAsync();
    public Task<bool> SaveUserAsync(User user);
    public Task DeleteUserAsync(User user);
    public Task<bool> UpdateUserAsync(User user);
    public Task<User> GetUserById(int id);

    public Task<User> GetUserByEmail(string email);
}