using PDCoreNew.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.DAL.Entities
{
    public class File : FileModel
    {
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
