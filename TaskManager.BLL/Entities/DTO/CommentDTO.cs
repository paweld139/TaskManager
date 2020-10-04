using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.BLL.Entities.DTO
{
    [DataContract(Name = "comment", Namespace = "")]
    public class CommentDTO : CommentBrief
    {
        [DataMember(Name = "employeeFirstName")]
        public string EmployeeFirstName { get; set; }

        [DataMember(Name = "employeeLastName")]
        public string EmployeeLastName { get; set; }
    }
}
