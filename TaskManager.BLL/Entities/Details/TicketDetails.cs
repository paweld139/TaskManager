using PDCore.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.DTO;
using TaskManager.BLL.Translators;

namespace TaskManager.BLL.Entities.Details
{
    [DataContract(Name = "ticket", Namespace = "")]
    public class TicketDetails : TicketBasic
    {
        [DataMember(Name = "type")]
        public string TypeValue { get; set; }

        [DataMember(Name = "priority")]
        public string PriorityValue { get; set; }

        [DataMember(Name = "status")]
        public virtual string StatusValue { get; set; }


        [DataMember(Name = "contrahent")]
        public string ContrahentName { get; set; }


        [DataMember(Name = "representativeFirstName")]
        public string RepresentativeFirstName { get; set; }

        [DataMember(Name = "representativeLastName")]
        public string RepresentativeLastName { get; set; }


        [DataMember(Name = "operatorFirstName")]
        public string OperatorFirstName { get; set; }

        [DataMember(Name = "operatorLastName")]
        public string OperatorLastName { get; set; }

        [DataMember(Name = "comments")]
        public virtual ICollection<CommentDTO> Comments { get; set; }

        [DataMember(Name = "representativeEmail")]
        public string RepresentativeApplicationUserEmail { get; set; }

        [DataMember(Name = "representativePhoneNumber")]
        public string RepresentativeApplicationUserPhoneNumber { get; set; }
    }
}
