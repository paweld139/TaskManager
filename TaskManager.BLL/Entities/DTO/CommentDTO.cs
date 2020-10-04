using PDCoreNew.Models;
using System.Collections.Generic;
using System.IO;
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

        [DataMember(Name = "files")]
        public ICollection<FileBrief> Files { get; set; }
    }
}
