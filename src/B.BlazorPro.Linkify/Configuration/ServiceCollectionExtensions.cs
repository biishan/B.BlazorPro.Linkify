using B.BlazorPro.Linkify.Common;
using B.BlazorPro.Linkify.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace B.BlazorPro.Linkify.Configuration
{
    /// <summary>
    /// Service Collection Extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add B.BlazorPro.Linkify options.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddBBlazorProLinkify(this IServiceCollection services, IConfiguration config)
        {
            if (config.GetSection(nameof(BBlazorProLinkifyOptions)).Exists())
            {
                services.Configure<BBlazorProLinkifyOptions>(x => config.GetSection(nameof(BBlazorProLinkifyOptions)).Bind(x));
            }
            else
            {
                services.Configure<BBlazorProLinkifyOptions>(x => new BBlazorProLinkifyOptions() { 
                    FilterType = FilterType.Blacklist,
                    UrlDomainsToFilter = new List<string>(),
                    EmailDomainsToFilter = new List<string>()
                });
            }
        }
    }
}