using PDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.DAL.Entities
{
    [Table("Contrahent", Schema = "dbo")]
    [DataContract(Name = "contrahent", Namespace = "")]
    public class Contrahent : ContrahentBasic, IModificationHistory
    {
        [Display(Name = "Archived", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "Archived")]
        public bool Archived { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Email", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailInvalid", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [StringLength(150, MinimumLength = 5, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Employee> Employees { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Ticket> Tickets { get; set; }


        [IgnoreDataMember]
        public DateTime DateModified { get; set; }

        [IgnoreDataMember]
        public DateTime DateCreated { get; set; }

        [Timestamp]
        [IgnoreDataMember]
        public byte[] RowVersion { get; set; }

        [IgnoreDataMember]
        public bool IsDirty { get; set; }
    }
}
