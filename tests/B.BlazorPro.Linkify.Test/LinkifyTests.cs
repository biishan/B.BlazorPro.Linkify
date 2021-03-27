using B.BlazorPro.Linkify.Models;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
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
            ctx.Services.Configure<BBlazorProLinkifyOptions>(x => new BBlazorProLinkifyOptions());

            // Act
            var linkifyComponent = ctx.RenderComponent<Linkify>(
                (nameof(Linkify.Text), "www.google.ca")
            );

            // Assert
            linkifyComponent.MarkupMatches(@"<p><a href=""https://www.google.ca"" target=""_blank"">https://www.google.ca</a></p>");
        }

        [Fact]
        public void CanLinkifyEmailAddress()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.Services.Configure<BBlazorProLinkifyOptions>(x => new BBlazorProLinkifyOptions());

            // Act
            var linkifyComponent = ctx.RenderComponent<Linkify>(
                (nameof(Linkify.Text), "test@domain.com")
            );

            // Assert
            linkifyComponent.MarkupMatches(@"<p><a href=""mailto:test@domain.com"" target=""_blank"">test@domain.com</a></p>");
        }

        [Fact]
        public void ShouldNotLinkifyUrl()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.Services.Configure<BBlazorProLinkifyOptions>(x => {
                x.FilterType = Common.FilterType.Blacklist;
                x.UrlDomainsToFilter = new List<string> { "www.google.ca" };
            });

            // Act
            var linkifyComponent = ctx.RenderComponent<Linkify>(
                (nameof(Linkify.Text), "www.google.ca")
            );

            // Assert
            linkifyComponent.MarkupMatches(@"<p>www.google.ca</p>");
        }

        [Fact]
        public void ShouldOnlyLinkifyWhitelistedUrl()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.Services.Configure<BBlazorProLinkifyOptions>(x => {
                x.FilterType = Common.FilterType.Whitelist;
                x.UrlDomainsToFilter = new List<string> { "www.google.ca" };
            });

            // Act
            var linkifyComponent = ctx.RenderComponent<Linkify>(
                (nameof(Linkify.Text), "www.blacklisted.com www.google.ca")
            );

            // Assert
            linkifyComponent.MarkupMatches(@"<p>www.blacklisted.com <a href=""https://www.google.ca"" target=""_blank"">https://www.google.ca</a></p>");
        }

        [Fact]
        public void ShouldNotLinkifyEmail()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.Services.Configure<BBlazorProLinkifyOptions>(x => {
                x.FilterType = Common.FilterType.Blacklist;
                x.EmailDomainsToFilter = new List<string> { "gmail.com" };
            });

            // Act
            var linkifyComponent = ctx.RenderComponent<Linkify>(
                (nameof(Linkify.Text), "test@gmail.com test2@gmail.com")
            );

            // Assert
            linkifyComponent.MarkupMatches(@"<p>test@gmail.com test2@gmail.com</p>");
        }

        [Fact]
        public void ShouldOnlyLinkifyWhitelistedEmail()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.Services.Configure<BBlazorProLinkifyOptions>(x => {
                x.FilterType = Common.FilterType.Whitelist;
                x.EmailDomainsToFilter = new List<string> { "gmail.com" };
            });

            // Act
            var linkifyComponent = ctx.RenderComponent<Linkify>(
                (nameof(Linkify.Text), "test@gmail.com spamemail@domain.com")
            );

            // Assert
            linkifyComponent.MarkupMatches(@"<p><a href=""mailto:test@gmail.com"" target=""_blank"">test@gmail.com</a> spamemail@domain.com</p>");
        }
    }
}