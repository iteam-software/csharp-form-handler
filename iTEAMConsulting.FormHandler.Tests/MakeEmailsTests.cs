using System.Collections.Generic;
using Xunit;

namespace iTEAMConsulting.FormHandler.Tests
{
    public class MakeEmailsTests
    {
        [Fact]
        public void ItShouldInstantiate_MakeEmails()
        {
            Assert.NotNull(new MakeEmails());
        }

        [Fact]
        public void BuildShould_CreateString()
        {
            var emails = new MakeEmails();

            var result = emails.Build(new[] { new KeyValuePair<string, string>("key", "value") });

            Assert.NotEmpty(result);
            Assert.NotNull(result);
        }
    }
}
