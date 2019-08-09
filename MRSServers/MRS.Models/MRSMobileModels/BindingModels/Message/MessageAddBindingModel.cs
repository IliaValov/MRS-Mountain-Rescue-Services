using MRS.Common.Mapping;
using MRS.Models.MRSMobileModels.RecourceModels.Message;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models.MRSMobileModels.BindingModels.Message
{
    public class MessageAddBindingModel : IMapTo<MessageResource>
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string Condition { get; set; }
    }
}
