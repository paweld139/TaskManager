﻿using PDCore.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.BLL.Entities.Basic
{
    [DataContract(Name = "ticket", Namespace = "")]
    public class TicketBasic : TicketBrief, IHasRowVersion
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Description", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Html)]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [Display(Name = "Budget", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "budget")]
        public decimal? Budget { get; set; }

        [Display(Name = "DateModified", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "dateModified")]
        public DateTime DateModified { get; set; }

        [Timestamp]
        [DataMember(Name = "rowVersion")]
        public byte[] RowVersion { get; set; }
    }
}
