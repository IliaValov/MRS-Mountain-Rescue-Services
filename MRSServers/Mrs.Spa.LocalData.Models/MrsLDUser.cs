using System.Collections.Generic;

namespace Mrs.Spa.LocalData.Models
{
    public class MrsLDUser
    {
        public MrsLDUser()
        {
            this.Locations = new List<MrsLDLocation>();
            this.Messages = new List<MrsLDMessage>();
        }

        public string PhoneNumber { get; set; }

        public ICollection<MrsLDLocation> Locations { get; set; }

        public ICollection<MrsLDMessage> Messages { get; set; }
    }
}
