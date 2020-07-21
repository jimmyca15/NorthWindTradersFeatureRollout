using System.Collections.Generic;

namespace Northwind.BikeShop.FeatureManagement
{
    public class BrowserFilterSettings
    {
        public IList<string> AllowedBrowsers { get; set; } = new List<string>();
    }
}
