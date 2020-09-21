using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BLL.Entities.Briefs
{
    [DataContract(Name = "comment", Namespace = "")]
    public class CommentBrief
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Content", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Html)]
        [DataMember(Name = "content")]
        public string Content { get; set; }

        [Display(Name = "Employee", ResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "employeeId")]
        public string EmployeeId { get; set; }


        [DataMember(Name = "dateCreated")]
        public DateTime DateCreated { get; set; }
    }
}
