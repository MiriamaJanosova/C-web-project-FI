using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Config
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CannotBeEmpty : ValidationAttribute
    {
        private const string defaultError = "'{0}' must have at least one element.";

        public CannotBeEmpty() : base(defaultError) //
        {
        }

        public override bool IsValid(object value)
        {
            return (value is IList list && list.Count > 0);
        }

        public override string FormatErrorMessage ( string name )
        {
            return string.Format(this.ErrorMessageString, name);
        }
    } 
       
}