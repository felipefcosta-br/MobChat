using System;
using System.Collections.Generic;
using System.Text;

namespace MobChatApp.Helpers.Validators.Interfaces
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }
        bool Check(T value);
    }
}
