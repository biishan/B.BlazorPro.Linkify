using B.BlazorPro.Linkify.Common;
using System.Collections.Generic;

namespace B.BlazorPro.Linkify.Models
{
    public class BBlazorProLinkifyOptions
    {
        public FilterType FilterType { get; set; } = FilterType.Blacklist;
        public List<string> UrlDomainsToFilter { get; set; } = new List<string>();
        public List<string> EmailDomainsToFilter { get; set; } = new List<string>();
    }
}