using System;
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
            // Arrange
            var emails = new MakeEmails();

            // Act
            var result = emails.Build(new {
                key = "value"
            });

            // Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void BuildShouldThrowOn_NullArgument()
        {
            // Arrange
            var emails = new MakeEmails();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => { var html = emails.Build(null); });
        }

        [Fact]
        public void BuildShouldThrowOn_InvalidArgument()
        {
            // Arrange
            var emails = new MakeEmails();

            // Act and Assert
            Assert.Throws<ArgumentException>(() => { var html = emails.Build(new { }); });
        }
    }
}
