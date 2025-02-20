using Medictionary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medictionary.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> LoadUsersAsync();
        Task SaveUsersAsync(List<User> users);
        Task<User> GetUserDetailsAsync(string username);
        bool Login(User user);
    }
}