using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using MRS.Models.MRSMobileModels.ViewModels.Account;

namespace MRS.Models.MRSMobileModels.ViewModels.Message
{
    public class MessageViewModel : IMapFrom<MrsMobileMessage>
    {
        public string Message { get; set; }

        public string Condition { get; set; }
    }
}
