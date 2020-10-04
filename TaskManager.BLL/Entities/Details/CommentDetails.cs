using PDCoreNew.Interfaces;
using PDCoreNew.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Basic;

namespace TaskManager.BLL.Entities.Details
{
    [DataContract(Name = "comment", Namespace = "")]
    public class CommentDetails : CommentBasic, IHasFiles
    {
        [DataMember(Name = "files")]
        public ICollection<FileModel> Files { get; set; }
    }
}
