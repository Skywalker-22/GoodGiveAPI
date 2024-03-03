using GoodGive.DAL.Entities;

namespace GoodGive.DAL.Seeders.Interfaces;

public interface ICharitySeeder
{
    List<Charity> GetCharitySeedData(DateTime currentDate);
}
