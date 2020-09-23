using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.BLL.Entities.Basic
{
    public class ContrahentBasic : ContrahentBrief
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "NIP", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(10, MinimumLength = 10, ErrorMessageResourceName = "StringLength_Equal", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "nip")]
        public string NIP { get; set; }

        [Display(Name = "IsOperator", ResourceType = typeof(Resources.Models.Contrahent))]
        [DataMember(Name = "isOperator")]
        public bool IsOperator { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "LicenseKey", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 20, ErrorMessageResourceName = "LicenseKeyInvalid", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "licenseKey")]
        public string LicenseKey { get; set; }
    }
}
