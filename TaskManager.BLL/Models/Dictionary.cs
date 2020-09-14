using PDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TaskManager.BLL.Models
{
    [Table("Dictionary", Schema = "dbo")]
    [DataContract(Name = "dictionary")]
    public class Dictionary : DictionaryBrief, IModificationHistory
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Name", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(150, MinimumLength = 3, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "name")]
        public string Name { get; set; }


        public virtual ICollection<Ticket> StatusTickets { get; set; }

        public virtual ICollection<Ticket> PriorityTickets { get; set; }

        public virtual ICollection<Ticket> TypeTickets { get; set; }


        public DateTime DateModified { get; set; }

        [DataMember(Name = "dateCreated")]
        public DateTime DateCreated { get; set; }

        [Timestamp]
        [DataMember(Name = "rowVersion")]
        public byte[] RowVersion { get; set; }
  
        public bool IsDirty { get; set; }
    }
}
