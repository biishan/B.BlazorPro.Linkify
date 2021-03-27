using System.ComponentModel;

namespace B.BlazorPro.Linkify.Common
{
    internal static class Constant
    {
        /// <summary>
        /// Regex pattern to find all URLs from the given text.
        /// </summary>
        public const string UrlRegexPattern = @"((www\.|(http|https|ftp|ftps|news|file)+\:\/\/)[_.a-z0-9-]+\.[a-z0-9\/_:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\|\n)])";
        /// <summary>
        /// Regex pattern to find all email addresses from the given text.
        /// </summary>
        public const string EmailRegexPattern = @"([mailto\:|\w\.\-]+)@((?!\.|\-)[\w\-]+)((\.(\w){2,3})+)?";
        /// <summary>
        /// Regex pattern to find all &lt;a&gt;...&lt;/a&gt; tags from the given text.
        /// </summary>
        public const string AnchorTagRegexPattern = @"<a[\s]+([^>]+)>((?:.(?!\<\/a\>))*.)</a>";
        /// <summary>
        /// Regex pattern to retrieve parts of a url.
        /// See <see cref="UrlPartPosition"/> to know part poisition.
        /// </summary>
        public const string PartsOfAUrlRegexPattern = @"((http[s]?|ftp[s]?|file|news):\/)?\/?(?'host'[0-9a-zA-Z-.]*)/?.*";
    }

    /// <summary>
    /// Urls/Email addresses filter type.
    /// </summary>
    public enum FilterType
    {
        Blacklist = 1,
        Whitelist = 2
    }

    /// <summary>
    /// Link type.
    /// </summary>
    public enum LinkType
    {
        Url = 1,
        Email = 2
    }

    internal enum UrlPartPosition
    {
        [Description("$1")]
        Url = 1,
        [Description("$2")]
        Protocol = 2,
        [Description("$3")]
        Host = 3,
        [Description("$4")]
        Path = 4,
        [Description("$5")]
        File = 5,
        [Description("$6")]
        Query = 6,
        [Description("$7")]
        Hash = 7
    }
}