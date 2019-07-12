using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models.MRSMobileModels.BindingModels.Message
{
    public class MessageCreateBindingModel : IMapTo<MrsMobileMessage>
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string Condition { get; set; }
    }
}
