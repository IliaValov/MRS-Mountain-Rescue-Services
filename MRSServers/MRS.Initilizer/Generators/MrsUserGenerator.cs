using MRS.Models;
using MRS.Models.Enums;
using System;

namespace MRS.Initilizer.Generators
{
    public static class MrsUserGenerator
    {
        internal static MrsUser[] CreateLocations()
        {
            Enum.TryParse("common", out RoleType result);

            var role = result;

            var user1 = new MrsUser
            {
                PhoneNumber = "0000000001",
                PhoneNumberConfirmed = true,
                Device = new MrsUserDevice
                {
                    Type = "Lenovo"
                },
                AuthanticationToken = new MrsUserAuthanticationToken(),
            };

           return null;
        }
    }
}
