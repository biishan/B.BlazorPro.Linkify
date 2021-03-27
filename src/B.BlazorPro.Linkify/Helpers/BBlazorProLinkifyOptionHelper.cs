using B.BlazorPro.Linkify.Common;
using B.BlazorPro.Linkify.Models;
using System;
using System.Linq;

namespace B.BlazorPro.Linkify.Helpers
{
    internal static class BBlazorProLinkifyOptionHelper
    {
        internal static bool ShouldLinkify(this BBlazorProLinkifyOptions options, string urlOrEmailHostName, LinkType type)
        {
            if (options.FilterType == FilterType.Blacklist)
            {
                if (type == LinkType.Url)
                {
                    return !options.UrlDomainsToFilter.Contains(urlOrEmailHostName, StringComparer.OrdinalIgnoreCase);
                }
                else if (type == LinkType.Email)
                {
                    return !options.EmailDomainsToFilter.Contains(urlOrEmailHostName, StringComparer.OrdinalIgnoreCase);
                }
            }
            else if (options.FilterType == FilterType.Whitelist)
            {
                if (type == LinkType.Url)
                {
                    return options.UrlDomainsToFilter.Contains(urlOrEmailHostName, StringComparer.OrdinalIgnoreCase);
                }
                else if (type == LinkType.Email)
                {
                    return options.EmailDomainsToFilter.Contains(urlOrEmailHostName, StringComparer.OrdinalIgnoreCase);
                }
            }

            return false;
        }
    }
}