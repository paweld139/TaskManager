using PDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TaskManager.DAL.Entities
{
    [Table("Contrahent", Schema = "dbo")]
    [DataContract(Name = "contrahent", Namespace = "")]
    public class Contrahent : IModificationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Name", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [Display(Name = "Archived", ResourceType = typeof(Resources.Common))]
        [IgnoreDataMember]
        public bool Archived { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "NIP", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(10, MinimumLength = 10, ErrorMessageResourceName = "StringLength_Equal", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [IgnoreDataMember]
        public string NIP { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Email", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailInvalid", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [StringLength(150, MinimumLength = 5, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [IgnoreDataMember]
        public string Email { get; set; }

        [Display(Name = "IsOperator", ResourceType = typeof(Resources.Models.Contrahent))]
        [IgnoreDataMember]
        public bool IsOperator { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "LicenseKey", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 20, ErrorMessageResourceName = "LicenseKeyInvalid", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [IgnoreDataMember]
        public string LicenseKey { get; set; }


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
