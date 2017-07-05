using System.Collections.Generic;

namespace iTEAMConsulting.FormHandler.Abstractions
{
    public interface IMakeEmails
    {
        string Build(IEnumerable<KeyValuePair<string, string>> formData);
    }
}
