using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerUnitTests.Services
{
    [TestClass]
    public class EntityServiceTests
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
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task AddUser_ShouldPersistUser()
        {
            using var context = new GradeTrackerContext(_dbContextOptions);
            var userService = new EntityService<UserEntity>(context);
            var user = new UserEntity
            {
                Username = "Cosbos",
                Password = "123//",
                FirstName = "Cosmin",
                LastName = "Popvici"
            };

            await userService.Create(user);

            var users = await userService.GetAll();
            Assert.AreEqual(1, users.Count);
        }
    }
}