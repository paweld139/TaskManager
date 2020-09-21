﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BLL.Entities.Briefs
{
    [DataContract(Name = "employee", Namespace = "")]
    public class EmployeeBrief
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "id")]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
    }
}
