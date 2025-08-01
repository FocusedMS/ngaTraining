using NUnit.Framework;
using UserMngmentSystem.Services;

namespace UserMngmentSystem.Tests
{
    [TestFixture]
    public class EncryptionServiceTests
    {
        private EncryptionService _encryptionService;

        [SetUp]
        public void Setup()
        {
            _encryptionService = new EncryptionService();
        }

        [Test]
        public void HashPassword_InputString_ReturnsHashedString()
        {
            string password = "test123";
            string hashed = _encryptionService.HashPassword(password);

            Assert.IsNotNull(hashed);
            Assert.IsNotEmpty(hashed);
            Assert.AreNotEqual(password, hashed);
        }

        [Test]
        public void VerifyPassword_MatchingPassword_ReturnsTrue()
        {
            string password = "hello123";
            string hashed = _encryptionService.HashPassword(password);

            bool result = _encryptionService.VerifyPassword(password, hashed);

            Assert.IsTrue(result);
        }

        [Test]
        public void VerifyPassword_NonMatchingPassword_ReturnsFalse()
        {
            string original = "password123";
            string wrong = "wrong123";
            string hashed = _encryptionService.HashPassword(original);

            bool result = _encryptionService.VerifyPassword(wrong, hashed);

            Assert.IsFalse(result);
        }
    }
}
