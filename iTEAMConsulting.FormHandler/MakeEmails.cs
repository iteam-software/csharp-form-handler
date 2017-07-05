using iTEAMConsulting.FormHandler.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace iTEAMConsulting.FormHandler
{
    public class MakeEmails : IMakeEmails
    {
        public MakeEmailsOptions _options { get; set; }

        public MakeEmails(IOptions<MakeEmailsOptions> options)
        {
            _options = options.Value ?? new MakeEmailsOptions();
        }

        /// <summary>
        /// Build accepts a plain object and produces a string of HTML suitable for use as an email message body.
        /// Build will reflect the object's public properties, use the property name as a key, and the property
        /// value as the value.  This set of properties will be used for the data in the HTML string.
        /// </summary>
        /// <param name="formData">An object with at least one public property.</param>
        /// <returns>An email friendly HTML string</returns>
        public string Build(object formData)
        {
            if (formData == null)
            {
                throw new ArgumentNullException();
            }

            if (!formData.GetType().GetProperties().Any())
            {
                throw new ArgumentException();
            }

            ICollection<string> tableRows = new List<string>();

            // Build body of the message
            IList<PropertyInfo> props = new List<PropertyInfo>(formData.GetType().GetProperties());
            foreach (PropertyInfo prop in props)
            {
                // Get keys and values from formData and add a row to the body.
                string key = prop.Name;
                string value = prop.GetValue(formData, null).ToString();
                tableRows.Add(string.Format(@"
                    <tr>
                        <td style=""font-family: Arial, Verdana, sans-serif; padding: 0 0 0 20px;"">{0}</td>
                        <td style=""font-family: Arial, Verdana, sans-serif; padding: 0 0 0 20px;"">{1}</td>
                    </tr>
                ", key, value));
            }

            return string.Format(@"
                <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
                <html xmlns=""http://www.w3.org/1999/xhtml"">
                    <head>
                        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
                        <title>{0}</title>
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                    </head>
                    <body style=""margin: 0; padding: 0; font-family: Arial, sans-serif;"">
                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                            <tr>
                                <td style=""padding: 48px 16px 48px 16px;"">
                                    <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"" style=""border-collapse: collapse;"">
                                        <tr>
                                            <td style=""font-family: Arial, Verdana, sans-serif; padding: 0 0 0 20px;"">{1}</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style=""padding: 48px 16px 48px 16px;"">
                                    <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"" style=""border-collapse: collapse;"">
                                        {2}
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </body>
                </html>
            ", this._options.Title, this._options.Description, string.Join("", tableRows));
        }
    }
}
