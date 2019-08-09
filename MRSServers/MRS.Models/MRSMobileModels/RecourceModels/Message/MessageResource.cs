using MRS.Common.Mapping;
using MRS.Models.MRSMobileModels.BindingModels.Message;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models.MRSMobileModels.RecourceModels.Message
{
    public class MessageResource : IMapFrom<MessageAddBindingModel>
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string Condition { get; set; }
    }
}
