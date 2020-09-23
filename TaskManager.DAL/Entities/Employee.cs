using PDCore.Interfaces;
using PDCore.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.DAL.Entities
{
    [Table("Employee", Schema = "dbo")]
    [DataContract(Name = "employee", Namespace = "")]
    public class Employee : EmployeeBasic, IModificationHistory
    {
        [ForeignKey("ContrahentId")]
        public virtual Contrahent Contrahent { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        //[Display(Name = "User", ResourceType = typeof(Resources.Common))]
        //[ForeignKey("ApplicationUser")]
        //public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }


        [IgnoreDataMember]
        public virtual ICollection<Comment> Comments { get; set; }


        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsDirty { get; set; }

        [NotMapped]
        public string FullName => StringUtils.GetFullName(FirstName, LastName);
    }
}
