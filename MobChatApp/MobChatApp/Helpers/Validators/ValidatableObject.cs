using MobChatApp.Helpers.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MobChatApp.Helpers.Validators
{
    public class ValidatableObject<T> : IValidatable<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<IValidationRule<T>> Validations { get; } = new List<IValidationRule<T>>();

        public List<string> Errors { get; set; } = new List<string>();
        public bool IsValid { get; set; } = true;
        public bool CleanOnChange { get; set; } = true;

        T value;
        public T Value
        {
            get => this.value;
            set
            {
                this.value = value;
                if (CleanOnChange)
                    IsValid = true;
            }
        }

        public virtual bool Validate()
        {
            Errors.Clear();
            IEnumerable<string> erros = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = erros.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;

        }
        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
