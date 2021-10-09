using ERService.Contracts.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ERService.Mvvm
{
    public abstract class ModelWrapper<T> : NotifyDataErrorInfoBase, IModelWrapper<T>
    {
        public ModelWrapper(T model) => Model = model;

        public T Model { get; }

        protected virtual TValue GetProperty<TValue>([CallerMemberName] string propertyName = null) =>
            (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);

        protected override bool SetProperty<TValue>(ref TValue storage, TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            
            ValidatePropertyInternal(propertyName, value);
            return base.SetProperty(ref storage, value, propertyName: propertyName);
        }

        /// <summary>
        /// Two step validation - DataAdnotations and custom validations
        /// </summary>
        /// <param name="propertyName"></param>
        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);
            ValidateDataAnnotations(propertyName, currentValue);
            ValidateCustomErrors(propertyName);
        }

        private void ValidateDataAnnotations(string propertyName, object currentValue)
        {
            var context = new ValidationContext(Model) { MemberName = propertyName };
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(currentValue, context, results);
            results.ForEach(r => AddError(propertyName, r.ErrorMessage));
        }

        private void ValidateCustomErrors(string propertyName)
        {
            var errors = ValidateProperty(propertyName);
            if (errors == null) return;
            foreach (var error in errors)
            {
                AddError(propertyName, error);
            }
        }

        /// <summary>
        /// Additional validations
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns>Errors</returns>
        protected virtual IEnumerable<string> ValidateProperty(string propertyName) =>
            Array.Empty<string>();
    }
}