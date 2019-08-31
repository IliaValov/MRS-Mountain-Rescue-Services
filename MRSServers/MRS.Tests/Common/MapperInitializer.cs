using MRS.Common.Mapping;
using MRS.Tests.MRS.Mobile.Tests.TestEntities;
using MRS.Tests.MRS.Spa.Tests.TestEntities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MRS.Tests.Common
{
    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(typeof(UserTestEntity).GetTypeInfo().Assembly);
        }

        public static void InitializeSpaMapper()
        {
            AutoMapperConfig.RegisterMappings(typeof(MissionLogTestEntity).GetTypeInfo().Assembly);
        }
    }
}
