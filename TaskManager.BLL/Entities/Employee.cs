using PDCore.Interfaces;
using PDCore.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TaskManager.BLL.Entities
{
    [Table("Employee", Schema = "dbo")]
    [DataContract(Name = "employee")]
    public class Employee : IModificationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Display(Name = "Contractor", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public int ContrahentId { get; set; }

        [ForeignKey("ContrahentId")]
        public virtual Contrahent Contrahent { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public string LastName { get; set; }


        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsDirty { get; set; }

        [NotMapped]
        [DataMember(Name = "fullName")]
        public string FullName => StringUtils.GetFullName(FirstName, LastName);
    }
}
