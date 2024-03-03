using GoodGive.DAL.Entities;

namespace GoodGive.DAL.Seeders.Interfaces;

public interface IUserSeeder
{
    List<User> GetUserSeedData(DateTime currentDate);
}
