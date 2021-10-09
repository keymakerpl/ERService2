using System;
using System.ComponentModel.DataAnnotations;

namespace ERService.Mvvm.Base
{
    public abstract class AuditableLookupItem<TId>
    {
        [Display(AutoGenerateField = false, Description = "ID field is not generated in UI")]
        public TId Id { get; set; }
        [Display(Name = "Utworzony przez", Description = "Rekord utworzony przez")]
        public string CreatedBy { get; set; }
        [Display(Name = "Utworzony dnia", Description = "Rekord utworzony dnia")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Zmodyfikowany przez", Description = "Rekord zmodyfikowany przez")]
        public string LastModifiedBy { get; set; }
        [Display(Name = "Zmodyfikowany dnia", Description = "Rekord zmodyfikowany dnia")]
        public DateTime? LastModifiedOn { get; set; }
    }
}
