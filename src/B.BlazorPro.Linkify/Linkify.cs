using B.BlazorPro.Linkify.Common;
using B.BlazorPro.Linkify.Events;
using B.BlazorPro.Linkify.Helpers;
using B.BlazorPro.Linkify.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace B.BlazorPro.Linkify
{
    public class Linkify : ComponentBase
    {
        /// <summary>
        /// Options.
        /// </summary>
        [Inject]
        protected IOptions<BBlazorProLinkifyOptions> Options { get; set; }
        /// <summary>
        /// Text to linkify.
        /// </summary>
        [Parameter]
        public string Text { get; set; }
        /// <summary>
        /// When set to 'true', links are opened in new tab.
        /// Default is true.
        /// </summary>
        [Parameter]
        public bool OpenInNewTab { get; set; } = true;
        /// <summary>
        /// Css class that is applied to all links found.
        /// </summary>
        [Parameter]
        public string CssClass { get; set; }
        /// <summary>
        /// A value representing the type of the element.
        /// </summary>
        [Parameter]
        public string ElementName { get; set; } = "p";
        /// <summary>
        /// Event Callback - On link clicked.
        /// </summary>
        [Parameter]
        public EventCallback<ClickEventArgs> OnLinkClicked { get; set; }

        private HtmlDocument _htmlDoc = new HtmlDocument();
        
        /// <summary>
        /// Build Render Tree.
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            var seq = -1;
            if (!string.IsNullOrEmpty(Text))
            {
                var html = Text;
                builder.OpenElement(seq++, ElementName);

                #region Replace all urls with anchor tag.
                
                var urlsMatches = Regex.Matches(html, Constant.UrlRegexPattern, RegexOptions.IgnoreCase);
                foreach (Match urlMatch in urlsMatches)
                {
                    var fullyQualifiedUrl = urlMatch.Value.StartsWith("www", StringComparison.OrdinalIgnoreCase) ? $"https://{urlMatch.Value}" : urlMatch.Value;
                    if (Options.Value.ShouldLinkify(fullyQualifiedUrl.GetHostName(LinkType.Url), LinkType.Url))
                    {
                        html = html.Replace(urlMatch.Value, @$"<a href=""{fullyQualifiedUrl}"">{fullyQualifiedUrl}</a>");
                    }
                }

                #endregion

                #region Replace all emails with anchor tag.

                var emailsMatches = Regex.Matches(html, Constant.EmailRegexPattern, RegexOptions.IgnoreCase);
                foreach (Match emailMatch in emailsMatches)
                {
                    if (emailMatch.Value.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase))
                    {
                        var emailAddressWithoutProtocol = emailMatch.Value.Replace("mailto:", "", StringComparison.OrdinalIgnoreCase);
                        if (Options.Value.ShouldLinkify(emailAddressWithoutProtocol.GetHostName(LinkType.Email), LinkType.Email))
                        {
                            html = html.Replace(emailMatch.Value, @$"<a href=""{emailMatch.Value}"">{emailMatch.Value}</a>");
                        }
                    }
                    else
                    {
                        if (Options.Value.ShouldLinkify(emailMatch.Value.GetHostName(LinkType.Email), LinkType.Email))
                        {
                            html = html.Replace(emailMatch.Value, @$"<a href=""mailto:{emailMatch.Value}"">{emailMatch.Value}</a>");
                        }
                    }
                }

                #endregion

                var anchorTagMatches = Regex.Matches(html, Constant.AnchorTagRegexPattern);
                var anchorTags = anchorTagMatches.Select(x => x.Value).ToList();
                if (anchorTags.Count > 0) // if there were anchor tags generated.
                {
                    var textAndAnchorTags = Regex.Split(html, "(" + string.Join('|', anchorTags) + "|\n)").ToList();
                    textAndAnchorTags.ForEach(textOrAnchorTag =>
                    {
                        _htmlDoc.LoadHtml(textOrAnchorTag);
                        var anchorTagNode = _htmlDoc.DocumentNode.SelectSingleNode("//a");
                        if (anchorTagNode != null) // is an anchor tag.
                        {
                            var href = anchorTagNode.Attributes["href"].Value;
                            if (href.StartsWith("mailto:"))
                            {
                                seq = builder.CreateAnchorTag(seq, LinkType.Email, href, CssClass, this, OnLinkClicked, OpenInNewTab);
                            }
                            else
                            {
                                seq = builder.CreateAnchorTag(seq, LinkType.Url, href, CssClass, this, OnLinkClicked, OpenInNewTab);
                            }
                        }
                        else if (textOrAnchorTag == "\n")
                        {
                            builder.OpenElement(seq++, "br");
                            builder.CloseElement();
                        }
                        else // is plain text.
                        {
                            builder.AddContent(seq++, textOrAnchorTag);
                        }
                    });
                }
                else // there were no links found.
                {
                    builder.AddContent(seq++, html);
                }

                builder.CloseElement();
            }
            else
            {
                builder.Clear();
            }
        }
    }
}