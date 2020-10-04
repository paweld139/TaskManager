using PDCoreNew.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.DAL.Entities
{
    public class File : FileModel
    {
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


        public int? CommentId { get; set; }

        public Comment Comment { get; set; }


        public int? TicketId { get; set; }

        public Ticket Ticket { get; set; }
    }
}
