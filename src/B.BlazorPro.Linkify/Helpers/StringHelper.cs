using B.BlazorPro.Linkify.Common;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace B.BlazorPro.Linkify.Helpers
{
    internal static class StringHelper
    {
        /// <summary>
        /// Gets host name of a URL/Email address.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static string GetHostName(this string value, LinkType type)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (type == LinkType.Url)
                {
                    var urlPartMatchRegex = new Regex(Constant.PartsOfAUrlRegexPattern, RegexOptions.IgnoreCase);
                    return urlPartMatchRegex.Match(value).Groups["host"].Value;
                }
                else if (type == LinkType.Email)
                {
                    var mailAddress = new MailAddress(value);
                    return mailAddress.Host;
                }
            }

            return "";
        }
    }
}