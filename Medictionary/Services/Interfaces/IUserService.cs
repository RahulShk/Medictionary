using Medictionary.Models;

namespace Medictionary.Services.Interfaces
{
    public interface IUserService
    {
        bool Login(User user);
    }
}