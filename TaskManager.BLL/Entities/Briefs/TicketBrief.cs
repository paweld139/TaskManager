﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TaskManager.BLL.Entities.Briefs
{
    [DataContract(Name = "ticket", Namespace = "")]
    public class TicketBrief
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Number", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        //[StringLength(50, MinimumLength = 4, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "number")]
        public string Number { get; set; } //Surrogate key

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Subject", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(80, MinimumLength = 4, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [Display(Name = "ExecutionDate", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataMember(Name = "executionDate")]
        public DateTimeOffset? ExecutionDate { get; set; }

        [Display(Name = "ReceiptDate", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataMember(Name = "receiptDate")]
        public DateTimeOffset? ReceiptDate { get; set; }


        #region Dictionary

        [Display(Name = "Type", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "typeId")]
        public int TypeId { get; set; }

        [Display(Name = "Priority", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "priorityId")]
        public int PriorityId { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "statusId")]
        public int StatusId { get; set; }

        #endregion


        #region Employee

        [Display(Name = "Representative", ResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "representativeId")]
        public string RepresentativeId { get; set; }

        [DataMember(Name = "operatorId")]
        public string OperatorId { get; set; }

        #endregion


        [Display(Name = "Contractor", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "contrahentId")]
        public int ContrahentId { get; set; }


        [Display(Name = "DateCreated", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "dateCreated")]
        public DateTime DateCreated { get; set; }
    }
}