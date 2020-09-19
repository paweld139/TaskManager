using PDCore.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Basic;

namespace TaskManager.DAL.Entities
{
    [Table("Comment", Schema = "dbo")]
    [DataContract(Name = "comment", Namespace = "")]
    public class Comment : CommentBasic, IModificationHistory
    {
        #region Proxies

        [ForeignKey("EmployeeId")]
        [DataMember(Name = "employee")]
        public virtual Employee Employee { get; set; }

        //[ForeignKey("TicketId")]
        //[DataMember(Name = "ticket")]
        //public virtual Ticket Ticket { get; set; }

        #endregion


        [IgnoreDataMember]
        public DateTime DateModified { get; set; }

        [Timestamp]
        [IgnoreDataMember]
        public byte[] RowVersion { get; set; }

        [IgnoreDataMember]
        public bool IsDirty { get; set; }
    }
}
