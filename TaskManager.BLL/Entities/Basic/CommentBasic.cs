﻿using PDCoreNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.BLL.Entities.Basic
{
    [DataContract(Name = "comment", Namespace = "")]
    public class CommentBasic : CommentBrief
    {
        [Display(Name = "Task", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "ticketId")]
        public int TicketId { get; set; }

        [NotMapped]
        [DataMember(Name = "files")]
        public IEnumerable<FileModel> Files { get; set; }
    }
}
