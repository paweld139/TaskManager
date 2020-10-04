using PDCore.Interfaces;
using PDCoreNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual string TypeValue { get; set; }

        [DataMember(Name = "priority")]
        public virtual string PriorityValue { get; set; }

        [DataMember(Name = "status")]
        public virtual string StatusValue { get; set; }


        [DataMember(Name = "contrahent")]
        public string ContrahentName { get; set; }


        [DataMember(Name = "representative")]
        public string Representative { get; set; }


        [DataMember(Name = "operator")]
        public string Operator { get; set; }


        [DataMember(Name = "comments")]
        public virtual ICollection<CommentDTO> Comments { get; set; }

        [DataMember(Name = "representativeEmail")]
        public string RepresentativeApplicationUserEmail { get; set; }

        [DataMember(Name = "representativePhoneNumber")]
        public string RepresentativeApplicationUserPhoneNumber { get; set; }

        [NotMapped]
        public ICollection<FileModel> Files { get; set; }
    }
}
