using System.Collections.Generic;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Entities.DTO;

namespace TaskManager.BLL.Models
{
    /// <summary>A collection of "lookup" lists</summary>
    /// <remarks>
    /// This is a DTO, not an entity backed by a database object.
    /// </remarks>
    [DataContract(Name = "lookups", Namespace = "")]
    public class Lookups
    {
        [DataMember(Name = "types")]
        public IList<DictionaryBrief> Types { get; set; }

        [DataMember(Name = "priorities")]
        public IList<DictionaryBrief> Priorities { get; set; }

        [DataMember(Name = "statuses")]
        public IList<DictionaryBrief> Statuses { get; set; }

        [DataMember(Name = "contrahents")]
        public IList<ContrahentBrief> Contrahents { get; set; }

        [DataMember(Name = "representatives")]
        public IList<EmployeeDTO> Representatives { get; set; }

        [DataMember(Name = "operators")]
        public IList<EmployeeDTO> Operators { get; set; }
    }
}
