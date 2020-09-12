using PDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BLL.Models
{
    [Table("Dictionary", Schema = "dbo")]
    [DataContract(Name = "dictionary")]
    public class Dictionary : IModificationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Name", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(150, MinimumLength = 4, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Value", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(150, MinimumLength = 4, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "value")]
        public string Value { get; set; }


        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsDirty { get; set; }
    }
}
