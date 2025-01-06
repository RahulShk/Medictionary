using System.Text.Json;
using Medictionary.Models;

namespace Medictionary.Abstraction
{
    public abstract class UserBase
    {
        private readonly string _filePath;
        private readonly ILogger<UserBase> _logger;

        protected UserBase(IWebHostEnvironment env, ILogger<UserBase> logger)
        {
            _filePath = Path.Combine(env.ContentRootPath, "App_Data", "users.json");
            _logger = logger;
        }

        protected List<User> LoadUsers()
        {
            _logger.LogInformation("Loading users from file: {FilePath}", _filePath);

            if (!File.Exists(_filePath))
            {
                _logger.LogWarning("File not found: {FilePath}", _filePath);
                return new List<User>();
            }

            try
            {
                var json = File.ReadAllText(_filePath);
                _logger.LogInformation("File content: {Json}", json);

                var users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
                _logger.LogInformation("Deserialized users: {Users}", users);

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading users from file: {FilePath}", _filePath);
                return new List<User>();
            }
        }

        // Method to save user data to the users.json file
        protected void SaveUsers(List<User> users)
        {
            try
            {
                // Serialize the list of User objects into a JSON string
                var json = JsonSerializer.Serialize(users);
                _logger.LogInformation("Serialized users: {Json}", json);

                // Write the JSON string to the users.json file
                File.WriteAllText(_filePath, json);
                _logger.LogInformation("Users saved to file: {FilePath}", _filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving users to file: {FilePath}", _filePath);
            }
        }
    }
}