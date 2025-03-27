using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GradeTrackerUnitTests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private DbContextOptions<GradeTrackerContext> _dbContextOptions = null!;

        [TestInitialize]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<GradeTrackerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestCleanup]
        public void Cleanup()
        {
            using var context = new GradeTrackerContext(_dbContextOptions);
            context.Database.EnsureDeleted(); // Delete the in-memory database after each test
        }

        [TestMethod]
        public async Task LoginMethod_ShouldReturnValidUserEntity()
        {
            // Arrange
            using var context = new GradeTrackerContext(_dbContextOptions);
            var userService = new UserService(context);
            UserEntity user = new UserEntity
            {
                Username = "Cosbos",
                Password = "123//",
                FirstName = "Cosmin",
                LastName = "Popvici"
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            UserEntity? result = await userService.Login("Cosbos", "123//");

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Username == "Cosbos");


        }
        [TestMethod]
        public async Task ChangePasswordMethod_ShouldUpdatePassword()
        {
            using var context = new GradeTrackerContext(_dbContextOptions);
            var userService = new UserService(context);
            UserEntity user = new UserEntity
            {
                Id = 1,
                Username = "Cosbos",
                Password = "123//",
                FirstName = "Cosmin",
                LastName = "Popvici"
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            await userService.ResetPassword("Cosbos", "newPass");

            var updatedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == 1);

            Assert.IsNotNull(updatedUser);
            Assert.IsTrue(updatedUser.Password == "newPass");
        }
    }
}
