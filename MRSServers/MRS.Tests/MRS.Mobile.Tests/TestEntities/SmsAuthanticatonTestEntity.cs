using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MRS.Tests.MRS.Mobile.Tests.TestEntities
{
    [ExcludeFromCodeCoverage]
    public class SmsAuthanticatonTestEntity : IMapFrom<MrsMobileSmsAuthantication>
    {
        public string Token { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthanticationCode { get; set; }

        public bool IsUsed { get; set; }

        public DateTime ExpiredAt { get; set; }

        public string UserId { get; set; }
        public UserTestEntity User { get; set; }
    }
}
