using Microsoft.Extensions.Logging;
using Medictionary.Models;
using Medictionary.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Medictionary.Data;

namespace Medictionary.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<User>> LoadUsersAsync()
        {
            _logger.LogInformation("Loading users from database.");

            try
            {
                var users = await _context.Users.Include(u => u.ProfileImage).ToListAsync();
                _logger.LogInformation("Loaded users: {Users}", users);

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading users from database.");
                return new List<User>();
            }
        }

        public async Task SaveUsersAsync(List<User> users)
        {
            try
            {
                _context.Users.UpdateRange(users);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Users saved to database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving users to database.");
            }
        }

        public async Task<User> GetUserDetailsAsync(string username)
        {
            try
            {
                var user = await _context.Users.Include(u => u.ProfileImage).FirstOrDefaultAsync(u => u.UserName == username);
                if (user != null)
                {
                    _logger.LogInformation("User details retrieved for {UserName}.", username);
                }
                else
                {
                    _logger.LogWarning("User details not found for {UserName}.", username);
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user details for {UserName}.", username);
                return null;
            }
        }

        public bool Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.PasswordHash))
            {
                _logger.LogWarning("Login failed: Username or password is empty.");
                return false;
            }

            bool isAuthenticated = _context.Users.Any(u => u.UserName == user.UserName && u.PasswordHash == user.PasswordHash);
            if (isAuthenticated)
            {
                _logger.LogInformation("User {UserName} logged in successfully.", user.UserName);
            }
            else
            {
                _logger.LogWarning("Login failed: Invalid username or password for user {UserName}.", user.UserName);
            }

            return isAuthenticated;
        }
    }
}