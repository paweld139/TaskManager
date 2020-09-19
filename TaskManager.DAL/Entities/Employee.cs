using PDCore.Interfaces;
using PDCore.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TaskManager.DAL.Entities
{
    [Table("Employee", Schema = "dbo")]
    [DataContract(Name = "employee", Namespace = "")]
    public class Employee : IModificationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "id")]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }


        [Display(Name = "Contractor", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public int ContrahentId { get; set; }

        [ForeignKey("ContrahentId")]
        public virtual Contrahent Contrahent { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        //[Display(Name = "User", ResourceType = typeof(Resources.Common))]
        //[ForeignKey("ApplicationUser")]
        //public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }


        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

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
