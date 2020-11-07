using MobChatApp.Helpers.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MobChatApp.Helpers.Validators.Rules
{
    public class IsOnlyLetterAndNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public Regex RegexLetterAndNumber { get; set; } = new Regex("[a-zA-Z0-9]");

        public bool Check(T value)
        {
            return (RegexLetterAndNumber.IsMatch($"{value}"));
        }
    }
}
