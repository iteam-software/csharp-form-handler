using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace iTEAMConsulting.FormHandler.Tests
{
    class TestObject
    {
        public int TestProp { get; set; } = 1;
    }

    public class MakeEmailsTests
    {
        public IOptions<MakeEmailsOptions> _options;

        public MakeEmailsTests()
        {
            var options = new MakeEmailsOptions
            {
                Title = "Test",
                Description = "Test Description"
            };

            var mock = new Mock<IOptions<MakeEmailsOptions>>();
            mock.Setup(i => i.Value).Returns(options);

            _options = mock.Object;
        }

        [Fact]
        public void ItShouldInstantiate_MakeEmails()
        {
            Assert.NotNull(new MakeEmails(_options));
        }

        [Fact]
        public void BuildShould_CreateString()
        {
            // Arrange
            var emails = new MakeEmails(_options);

            // Act
            var result = emails.Build(new TestObject());

            // Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void BuildShouldThrowOn_NullArgument()
        {
            // Arrange
            var emails = new MakeEmails(_options);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => { var html = emails.Build(null); });
        }

        [Fact]
        public void BuildShouldThrowOn_InvalidArgument()
        {
            // Arrange
            var emails = new MakeEmails(_options);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => { var html = emails.Build(new { }); });
        }
    }
}
