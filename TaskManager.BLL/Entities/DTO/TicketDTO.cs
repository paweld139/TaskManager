using System.Runtime.Serialization;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Translators;

namespace TaskManager.BLL.Entities.DTO
{
    [DataContract(Name = "ticket", Namespace = "")]
    public class TicketDTO : TicketBrief
    {
        [DataMember(Name = "type")]
        public virtual string TypeValue { get; set; }

        [DataMember(Name = "priority")]
        public virtual string PriorityValue { get; set; }

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
    }
}
