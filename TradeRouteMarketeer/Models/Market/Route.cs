using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeRouteMarketeer.Models.Market
{
    public class Route
    {
        /// <summary>
        /// The origin island
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// The destination island
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// The commodities to buy at the origin island
        /// </summary>
        public Dictionary<string, List<Trade>> ToBuy { get; set; }

        /// <summary>
        /// The commodities to sell at the destination island
        /// </summary>
        public Dictionary<string, List<Trade>> ToSell { get; set; }

        public Route(string origin, string destination)
        {
            Origin = origin;
            Destination = destination;
            ToBuy = new Dictionary<string, List<Trade>>();
            ToSell = new Dictionary<string, List<Trade>>();
        }
    }
}
