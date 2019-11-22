using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lifu.Filters
{

    /// <summary>
    /// Validation attribute that demands that a boolean value must be true.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public bool Value
        {
            get;
            set;
        }


        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }
    }
}