using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.BikeShop.FeatureManagement
{
    /// <summary>
    /// A feature filter that can enable features based on what browser a request has been sent from.
    /// </summary>
    public class BrowserFilter : IFeatureFilter
    {
        private const string Chrome = "Chrome";
        private const string Edge = "Edge";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrowserFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            BrowserFilterSettings settings = context.Parameters.Get<BrowserFilterSettings>() ?? new BrowserFilterSettings();

            if (settings.AllowedBrowsers.Any(browser => browser.Equals(Chrome, StringComparison.OrdinalIgnoreCase)) && IsChrome())
            {
                return Task.FromResult(true);
            }
            else if (settings.AllowedBrowsers.Any(browser => browser.Equals(Edge, StringComparison.OrdinalIgnoreCase)) && IsEdge())
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        private bool IsChrome()
        {
            string userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];

            return userAgent != null && userAgent.Contains("Chrome", StringComparison.OrdinalIgnoreCase) && !IsEdge();
        }

        private bool IsEdge()
        {
            // Return true if current request is sent from Edge browser
            string userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];

            return userAgent != null && userAgent.Contains("Edg/", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("Edge/", StringComparison.OrdinalIgnoreCase);
        }
    }
}
