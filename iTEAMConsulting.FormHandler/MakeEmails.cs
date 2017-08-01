using iTEAMConsulting.FormHandler.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using WebMarkupMin.Core;

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

            // Variables from options
            string title = this._options.Title;
            string description = this._options.Description;
            string font = this._options.Font;
            int fontSize = this._options.FontSize;
            string fontColor = this._options.FontColor;
            int cellPadding = this._options.CellPadding;
            int cellBorder = this._options.CellBorder;

            // List of data table rows
            ICollection<string> tableRows = new List<string>();

            // Build body of the message
            IList<PropertyInfo> props = new List<PropertyInfo>(formData.GetType().GetProperties());
            foreach (PropertyInfo prop in props)
            {
                // Get keys and values from formData and add a row to the body.
                string key = prop.Name;
                string value = (prop.GetValue(formData, null) ?? "").ToString();
                tableRows.Add(string.Format(@"
                    <tr>
                        <td>{0}</td>
                        <td>{1}</td>
                    </tr>
                ", key, value));
            }

            var html = string.Format(@"
                <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
                <html xmlns=""http://www.w3.org/1999/xhtml"">
                    <head>
                        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
                        <title>{0}</title>
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                    </head>
                    <body style=""margin: 0; padding: 0; font-family: {3}; font-size: {4}px; color: {5};"">
                        <table border=""0"" cellpadding=""16"" cellspacing=""0"" width=""100%"">
                            <tr>
                                <td>
                                    <table
                                        align=""center""
                                        border=""0""
                                        cellpadding=""0""
                                        cellspacing=""0""
                                        width=""600""
                                        style=""border-collapse: collapse;""
                                    >
                                        <tr><td style=""font-size: 2em;"">{0}</td></tr>
                                        <tr><td>{1}</td></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table
                                        align=""center""
                                        border=""{6}""
                                        cellpadding=""{7}""
                                        cellspacing=""0""
                                        width=""600""
                                        style=""border-collapse: collapse;""
                                    >
                                        {2}
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </body>
                </html>",
                title,                      // 0
                description,                // 1
                string.Join("", tableRows), // 2
                font,                       // 3
                fontSize,                   // 4
                fontColor,                  // 5
                cellBorder,                 // 6
                cellPadding);               // 7

            var htmlMinifierSettings = new HtmlMinificationSettings() {
                RemoveOptionalEndTags = false
            };
            var minifiedHtml = new HtmlMinifier(htmlMinifierSettings).Minify(html).MinifiedContent;
            return minifiedHtml;
        }
    }
}
