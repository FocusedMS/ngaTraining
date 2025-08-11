using System.Collections.Generic;
using System.Linq;
using UserMngmentSystem.Models;
using UserMngmentSystem.Utils;

namespace UserMngmentSystem.Services
{
    public class AuthService
    {
        private readonly List<User> _users = new();
        private readonly EncryptionService _encryptionService;
        private readonly Logger _logger;

        public AuthService(EncryptionService encryptionService, Logger logger)
        {
            _encryptionService = encryptionService;
            _logger = logger;
        }

        public bool Register(string username, string password, string sensitiveInfo)
        {
            if (_users.Any(u => u.Username == username))
            {
                _logger.Log("Username already exists.");
                return false;
            }

            var user = new User
            {
                Username = username,
                HashedPassword = _encryptionService.HashPassword(password),
                SensitiveInfo = sensitiveInfo
            };

            _users.Add(user);
            _logger.Log($"User '{username}' registered successfully.");
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                _logger.Log("User not found.");
                return false;
            }

            bool isValid = _encryptionService.VerifyPassword(password, user.HashedPassword);
            _logger.Log(isValid ? "Login successful." : "Invalid password.");
            return isValid;
        }

        public List<User> GetAllUsers() => _users;
    }
}
