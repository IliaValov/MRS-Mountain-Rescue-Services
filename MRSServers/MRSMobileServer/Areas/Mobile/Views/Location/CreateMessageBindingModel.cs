using MRS.Common.Mapping;
using MRSMobile.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MRSMobileServer.Areas.Mobile.Views.Location
{
    public class CreateMessageBindingModel : IMapTo<MrsMobileMessage>
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string Condition { get; set; }
    }
}
