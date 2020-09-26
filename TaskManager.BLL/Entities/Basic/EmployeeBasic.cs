using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.BLL.Entities.Basic
{
    [DataContract(Name = "employee", Namespace = "")]
    public class EmployeeBasic : EmployeeBrief
    {
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
    }
}
