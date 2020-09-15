using PDCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Entities;

namespace TaskManager.BLL.Models
{
    [DataContract(Name = "ticket")]
    public class TicketModel : TicketBrief
    {
        [DataMember(Name = "type")]
        public string  TypeValue { get; set; }

        [DataMember(Name = "priority")]
        public string PriorityValue { get; set; }

        [DataMember(Name = "status")]
        public string StatusValue { get; set; }


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
