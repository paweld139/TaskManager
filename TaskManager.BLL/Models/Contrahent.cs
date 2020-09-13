﻿using PDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TaskManager.BLL.Models
{
    [Table("Contrahent", Schema = "dbo")]
    [DataContract(Name = "contrahent")]
    public class Contrahent : IModificationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id", ResourceType = typeof(Resources.Common))]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Name", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [Display(Name = "Archived", ResourceType = typeof(Resources.Common))]
        public bool Archived { get; set; }

        [Required(ErrorMessage = "Nie wpisano NIPu")]
        [Display(Name = "NIP", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.Text)]
        [StringLength(10, MinimumLength = 10, ErrorMessageResourceName = "StringLength_Equal", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public string NIP { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Email", ResourceType = typeof(Resources.Common))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailInvalid", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [StringLength(150, MinimumLength = 5, ErrorMessageResourceName = "StringLength_GreaterAndLess", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public string Email { get; set; }

        [Display(Name = "IsOperator", ResourceType = typeof(Resources.Models.Contrahent))]
        public bool IsOperator { get; set; }

        [Required(ErrorMessage = "Nie uzupełniono pola '{0}'")]
        [Display(Name = "Klucz licencyjny")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 20, ErrorMessageResourceName = "LicenseKeyInvalid", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public string LicenseKey { get; set; }

        public virtual ICollection<Employee> Users { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }


        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsDirty { get; set; }
    }
}