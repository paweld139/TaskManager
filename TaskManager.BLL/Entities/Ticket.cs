﻿using PDCore.Interfaces;
using PDCoreNew.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TaskManager.BLL.Entities
{
    [Table("Ticket", Schema = "dbo")]
    [DataContract(Name = "ticket")]
    public class Ticket : TicketBrief, IModificationHistory
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Description", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Html)]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [Display(Name = "Budget", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "budget")]
        public decimal? Budget { get; set; }


        [ForeignKey("TypeId")]
        [DataMember(Name = "type")]
        public virtual Dictionary Type { get; set; }

        [ForeignKey("PriorityId")]
        [DataMember(Name = "priority")]
        public virtual Dictionary Priority { get; set; }

        [ForeignKey("StatusId")]
        [DataMember(Name = "status")]
        public virtual Dictionary Status { get; set; }


        [ForeignKey("ContrahentId")]
        [DataMember(Name = "contrahent")]
        public virtual Contrahent Contrahent { get; set; }


        [ForeignKey("RepresentativeId")]
        [DataMember(Name = "representative")]
        public virtual Employee Representative { get; set; }

        [ForeignKey("OperatorId")]
        [DataMember(Name = "operator")]
        public virtual Employee Operator { get; set; }

        [Display(Name = "DateModified", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "dateModified")]
        public DateTime DateModified { get; set; }

        [Timestamp]
        [DataMember(Name = "rowVersion")]
        public byte[] RowVersion { get; set; }

        [IgnoreDataMember]
        public bool IsDirty { get; set; }
    }
}