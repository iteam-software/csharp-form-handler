using iTEAMConsulting.FormHandler.Abstractions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace iTEAMConsulting.FormHandler
{
    public class MakeEmails : IMakeEmails
    {
        /// <summary>
        /// Build accepts a plain object and produces a string of HTML suitable for use as an email message body.
        /// Build will reflect the object's public properties, use the property name as a key, and the property
        /// value as the value.  This set of properties will be used for the data in the HTML string.
        /// </summary>
        /// <param name="formData">An object with at least one public property.</param>
        /// <returns>An email friendly HTML string</returns>
        public string Build(object formData)
        {
            throw new NotImplementedException();
        }
    }
}
