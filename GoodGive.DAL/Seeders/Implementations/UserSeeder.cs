using GoodGive.DAL.Entities;
using GoodGive.DAL.Seeders.Interfaces;

namespace GoodGive.DAL.Seeders.Implementations;

public class UserSeeder : IUserSeeder
{
    public List<User> GetUserSeedData(DateTime currentDate)
    {
        var userSeedId = Guid.NewGuid();

        var users = new List<User>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                ConfirmEmail = "john.doe@example.com",
                Mobile = "123-456-7890",
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                ConfirmEmail = "jane.doe@example.com",
                Mobile = "098-765-4321",
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Johnson",
                Email = "alice.johnson@example.com",
                ConfirmEmail = "alice.johnson@example.com",
                Mobile = "555-555-5555",
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            }
        };

        return users;
    }
}
