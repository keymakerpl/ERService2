using System;

namespace ERService.Mvvm.Base
{
    public abstract class AuditableWrapper<TId, TModel> : ModelWrapper<TModel>
    {
        public AuditableWrapper(TModel model) : base(model)
        {
        }

        public TId Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
