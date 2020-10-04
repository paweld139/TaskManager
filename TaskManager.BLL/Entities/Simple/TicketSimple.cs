using PDCoreNew.Interfaces;
using PDCoreNew.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Basic;

namespace TaskManager.BLL.Entities.Simple
{
    [DataContract(Name = "ticket", Namespace = "")]
    public class TicketSimple : TicketBasic, IHasFiles
    {
        [DataMember(Name = "files")]
        public ICollection<FileModel> Files { get; set; }
    }
}
