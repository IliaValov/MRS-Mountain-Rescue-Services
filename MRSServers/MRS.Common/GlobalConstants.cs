using System;

namespace MRS.Common
{
    public static class GlobalConstants
    {
        public const string AdministratorRoleName = "Administrator";

        public const string NormalUserType = "normal";

        public const string SaviorUserType = "savior";

        public const string JsonContentType = "application/json";

        public const string MrsMobileDbConnectionString = @"Server=.\SQLEXPRESS;Database=MrsMobileDb;Trusted_Connection=True;Integrated Security = True;";

        public const string MrsSpaDbConnectionString = @"Server=.\SQLEXPRESS;Database=MrsSpaDb;Trusted_Connection=True;Integrated Security = True;";
    }
}
