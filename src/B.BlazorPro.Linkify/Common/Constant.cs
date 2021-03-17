namespace B.BlazorPro.Linkify.Common
{
    internal static class Constant
    {
        public const string UrlRegexPattern = @"((www\.|(http|https|ftp|news|file)+\:\/\/)[_.a-z0-9-]+\.[a-z0-9\/_:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\|\n)])";
        public const string EmailRegexPattern = @"([mailto\:|\w\.\-]+)@((?!\.|\-)[\w\-]+)((\.(\w){2,3})+)?";
        public const string AnchorTagRegexPattern = @"<a[\s]+([^>]+)>((?:.(?!\<\/a\>))*.)</a>";
    }

    public enum LinkType
    {
        Url = 1,
        Email = 2
    }
}