using PDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;

namespace TaskManager.BLL.Models
{
    [Table("Ticket", Schema = "dbo")]
    [DataContract(Name = "ticket")]
    public class Ticket : IModificationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Subject", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(80, MinimumLength = 4, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Description", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Html)]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Number", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 4, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "number")]
        public string Number { get; set; } //Surrogate key


        #region Dictionary

        [Display(Name = "Type", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "typeId")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public virtual Dictionary Type { get; set; }


        [Display(Name = "Priority", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "priorityId")]
        public int PriorityId { get; set; }

        [ForeignKey("PriorityId")]
        public virtual Dictionary Priority { get; set; }


        [Display(Name = "Status", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "statusId")]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual Dictionary Status { get; set; }

        #endregion


        #region ApplicationUser

        [Display(Name = "Representative", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "representativeId")]
        public int RepresentativeId { get; set; }

        [ForeignKey("RepresentativeId")]
        public virtual ApplicationUser Representative { get; set; }


        [Display(Name = "Operator", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "operatorId")]
        public int OperatorId { get; set; }

        [ForeignKey("OperatorId")]
        public virtual ApplicationUser Operator { get; set; }

        #endregion


        [Display(Name = "Contractor", ResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "contrahentId")]
        public int ContrahentId { get; set; }

        [ForeignKey("ContrahentId")]
        public virtual Contrahent Contrahent { get; set; }


        [Display(Name = "Budget", ResourceType = typeof(Resources.Common))]
        public decimal? Budget { get; set; }

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


        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsDirty { get; set; }
    }
}
