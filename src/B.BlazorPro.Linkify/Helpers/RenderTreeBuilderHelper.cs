using B.BlazorPro.Linkify.Common;
using B.BlazorPro.Linkify.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq;

namespace B.BlazorPro.Linkify.Helpers
{
    internal static class RenderTreeBuilderHelper
    {
        /// <summary>
        /// Create anchor tag.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seq"></param>
        /// <param name="linkType"></param>
        /// <param name="url"></param>
        /// <param name="cssClass"></param>
        /// <param name="receiver"></param>
        /// <param name="onLinkClicked"></param>
        /// <param name="openInNewTab"></param>
        /// <returns>Current sequence number</returns>
        public static int CreateAnchorTag(this RenderTreeBuilder builder, int seq, LinkType linkType,
            string url, string cssClass, object receiver, EventCallback<ClickEventArgs> onLinkClicked, bool openInNewTab = true)
        {

            builder.OpenElement(seq++, "a");
            builder.AddAttribute(seq++, "class", cssClass);
            builder.AddAttribute(seq++, "href", url);
            if (openInNewTab)
                builder.AddAttribute(seq++, "target", "_blank");
            if (onLinkClicked.HasDelegate)
            {
                builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(receiver, async () =>
                {
                    await onLinkClicked.InvokeAsync(new ClickEventArgs
                    {
                        Type = linkType,
                        Value = url
                    });
                }));
            }
            if (linkType == LinkType.Email)
            {
                var emailAddress = url.Split("mailto:").Last();
                builder.AddContent(seq++, emailAddress);
            }
            else
            {
                builder.AddContent(seq++, url);
            }
            builder.CloseElement();
            return seq;
        }
    }
}