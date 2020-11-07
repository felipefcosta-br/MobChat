using MobChatApp.Helpers.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MobChatApp.Helpers.Validators.Rules
{
    public class IsOnlyNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        Regex RegexOnlyNumber { get; set; } = new Regex("^[0-9]*$");

        public bool Check(T value)
        {
            return RegexOnlyNumber.IsMatch($"{value}");
        }
    }
}
