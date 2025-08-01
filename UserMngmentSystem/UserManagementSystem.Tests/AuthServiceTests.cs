using NUnit.Framework;
using UserMngmentSystem.Models;
using UserMngmentSystem.Services;
using UserMngmentSystem.Utils;

namespace UserMngmentSystem.Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            var encryptionService = new EncryptionService();
            var logger = new Logger("application.log");
            _authService = new AuthService(encryptionService, logger);
        }

        [Test]
        public void Register_NewUser_ReturnsTrue()
        {
            bool result = _authService.Register("madhu", "madhu123", "admin");
            Assert.IsTrue(result);
        }

        [Test]
        public void Register_DuplicateUser_ReturnsFalse()
        {
            _authService.Register("madhu", "madhu123", "admin");
            bool result = _authService.Register("madhu", "anotherpass", "admin");
            Assert.IsFalse(result);
        }

        [Test]
        public void Login_CorrectCredentials_ReturnsTrue()
        {
            _authService.Register("madhu", "madhu123", "admin");
            bool result = _authService.Login("madhu", "madhu123");
            Assert.IsTrue(result);
        }

        [Test]
        public void Login_WrongPassword_ReturnsFalse()
        {
            _authService.Register("madhu", "madhu123", "admin");
            bool result = _authService.Login("madhu", "wrongpass");
            Assert.IsFalse(result);
        }

        [Test]
        public void Login_NonExistentUser_ReturnsFalse()
        {
            bool result = _authService.Login("ghost", "any");
            Assert.IsFalse(result);
        }
    }
}
