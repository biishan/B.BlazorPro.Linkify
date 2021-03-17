using Bunit;
using Xunit;

namespace B.BlazorPro.Linkify.Test
{
    public class LinkifyTests
    {
        [Fact]
        public void CanLinkifyUrl()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var linkifyComponent = ctx.RenderComponent<Linkify>(
                (nameof(Linkify.Text), "www.google.ca")
            );

            // Assert
            linkifyComponent.MarkupMatches(@"<p><a href=""https://www.google.ca""  target=""_blank"">https://www.google.ca</a></p>");
        }

        [Fact]
        public void CanLinkifyEmailAddress()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var linkifyComponent = ctx.RenderComponent<Linkify>(
                (nameof(Linkify.Text), "test@domain.com")
            );

            // Assert
            linkifyComponent.MarkupMatches(@"<p><a href=""mailto:test@domain.com""  target=""_blank"">test@domain.com</a></p>");
        }
    }
}