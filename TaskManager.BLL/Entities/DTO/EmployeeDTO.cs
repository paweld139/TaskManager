using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.BLL.Entities.DTO
{
    [DataContract(Name = "employee", Namespace = "")]
    public class EmployeeDTO : EmployeeBrief
    {
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }
    }
}
