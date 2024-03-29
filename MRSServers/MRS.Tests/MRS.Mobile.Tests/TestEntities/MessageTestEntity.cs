﻿using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MRS.Tests.MRS.Mobile.Tests.TestEntities
{
    [ExcludeFromCodeCoverage]
    public class MessageTestEntity : IMapFrom<MrsMobileMessage>, IMapTo<MrsMobileMessage>
    {

        public string Message { get; set; }

        public string Condition { get; set; }

        public DateTime CreatedOn { get; set; }



    }
}
