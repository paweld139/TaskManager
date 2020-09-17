//using PDCore.Interfaces;
//using System.ComponentModel.DataAnnotations;
//using System.Runtime.Serialization;

//namespace TaskManager.BLL.Models
//{
//    [DataContract(Name = "ticket", Namespace = "")]
//    public class TicketModel : IHasRowVersion
//    {
//        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
//        [DataMember(Name = "id")]
//        public int Id { get; set; }


//        [Display(Name = "Budget", ResourceType = typeof(Resources.Common))]
//        [DataMember(Name = "budget")]
//        public decimal? Budget { get; set; }

//        [Display(Name = "Priority", ResourceType = typeof(Resources.Common))]
//        [Range(1, int.MaxValue, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
//        [DataMember(Name = "priorityId")]
//        public int PriorityId { get; set; }

//        [Display(Name = "Operator", ResourceType = typeof(Resources.Common))]
//        [DataMember(Name = "operatorId")]
//        public int? OperatorId { get; set; }


//        [DataMember(Name = "rowVersion")]
//        public byte[] RowVersion { get; set; }
//    }
//}
