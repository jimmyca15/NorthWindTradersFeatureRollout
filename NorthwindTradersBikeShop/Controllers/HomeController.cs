using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.BikeShop.Models;
using Microsoft.FeatureManagement;
using Northwind.BikeShop.FeatureManagement;
using System.Threading.Tasks;

namespace Northwind.BikeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeatureManagerSnapshot _featureManager;
        private readonly ILogger<HomeController> _logger;


        public HomeController(IFeatureManagerSnapshot featureManager, ILogger<HomeController> logger)
        {
            _featureManager = featureManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _featureManager.IsEnabledAsync(Features.Beta) ?
                              "Index.beta" :
                              "Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
