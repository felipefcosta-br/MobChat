﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MobChatApp.Helpers.Validators.Interfaces
{ 
    public interface IValidatable<T> : INotifyPropertyChanged
    {
        List<IValidationRule<T>> Validations { get; }
        List<string> Errors { get; set; }
        bool Validate();
        bool IsValid { get; set; }
    } 
}
