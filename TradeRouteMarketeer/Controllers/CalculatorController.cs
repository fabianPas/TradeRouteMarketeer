using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using TradeRouteMarketeer.Models.Market;

namespace TradeRouteMarketeer.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpPost("route")]
        public IActionResult Index(IFormFile origin, IFormFile destination)
        {
            if (origin == null || destination == null)
                return View();

            var originMarket = GetMarket(origin);
            var destinationMarket = GetMarket(destination);

            var routeCalculator = new RouteCalculator(originMarket, destinationMarket);
            var route = routeCalculator.GetRoute();

            return View(route);
        }

        private CommodityMarket GetMarket(IFormFile market)
        {
            var stream = market.OpenReadStream();

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<CommodityMarket>(json);
            }
        }
    }
}
