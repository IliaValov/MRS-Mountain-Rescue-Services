using MRS.Data;
using MRS.Initilizer.Generators;
using MRS.Models;

namespace MRS.Initilizer
{
    public class MrsDBInitilizer
    {
        public static string ResetDataBase(MRSContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed(context);

            return "PLS database was created sucessfuly";
        }

        private static void Seed(MRSContext context)
        {
            MrsUser[] users = MrsUserGenerator.CreateUsers();
            MrsLocation[] locations = LocationGenerator.CreateLocations();

            context.AddRange(users);
            context.SaveChanges();
            context.AddRange(locations);
            context.SaveChanges();

        }
    }
}
