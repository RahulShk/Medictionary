using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Medictionary.Models;
using Medictionary.Abstraction;

namespace Medictionary.Services
{
    public class UserService : UserBase
    {
        public const string SeedUsername = "admin@gmail.com";
        public const string SeedPassword = "password";
        private readonly List<User> _users;
        private readonly ILogger<UserService> _logger;

        public UserService(IWebHostEnvironment env, ILogger<UserService> logger) : base(env, logger)
        {
            _logger = logger;
            _users = LoadUsers();

            if (!_users.Any())
            {
                _logger.LogInformation("No users found. Seeding default user.");
                _users.Add(new User { Username = SeedUsername, Password = SeedPassword });
                SaveUsers(_users);
            }
        }

        public bool Login(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                _logger.LogWarning("Login failed: Username or password is empty.");
                return false;
            }

            bool isAuthenticated = _users.Any(u => u.Username == user.Username && u.Password == user.Password);
            if (isAuthenticated)
            {
                _logger.LogInformation("User {Username} logged in successfully.", user.Username);
            }
            else
            {
                _logger.LogWarning("Login failed: Invalid username or password for user {Username}.", user.Username);
            }

            return isAuthenticated;
        }
    }
}