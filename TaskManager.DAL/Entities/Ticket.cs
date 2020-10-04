using PDCore.Interfaces;
using PDCoreNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Basic;

namespace TaskManager.DAL.Entities
{
    [CollectionDataContract(Name = "tickets", Namespace = "")]
    public class Tickets : List<Ticket>
    {
        public Tickets(IEnumerable<Ticket> queryables) : base(queryables)
        {
        }

        public Tickets()
        {
        }
    }

    [Table("Ticket", Schema = "dbo")]
    [DataContract(Name = "ticket", Namespace = "")]
    public class Ticket : TicketBasic, IModificationHistory
    {
        #region Proxies

        [ForeignKey("TypeId")]
        [DataMember(Name = "type")]
        public virtual Dictionary Type { get; set; }

        [ForeignKey("PriorityId")]
        [DataMember(Name = "priority")]
        public virtual Dictionary Priority { get; set; }

        [ForeignKey("StatusId")]
        [DataMember(Name = "status")]
        public virtual Dictionary Status { get; set; }


        [ForeignKey("ContrahentId")]
        [DataMember(Name = "contrahent")]
        public virtual Contrahent Contrahent { get; set; }


        [ForeignKey("RepresentativeId")]
        [DataMember(Name = "representative")]
        public virtual Employee Representative { get; set; }

        [ForeignKey("OperatorId")]
        [DataMember(Name = "operator")]
        public virtual Employee Operator { get; set; }

        [DataMember(Name = "comments")]
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<File> Files { get; set; }

        #endregion


        [IgnoreDataMember]
        public bool IsDirty { get; set; }
    }
}
