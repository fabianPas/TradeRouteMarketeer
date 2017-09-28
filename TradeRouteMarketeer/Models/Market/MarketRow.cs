using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeRouteMarketeer.Models.Market
{
    public class MarketRow
    {
        /// <summary>
        /// The commodity
        /// </summary>
        public string Commodity { get; set; }

        /// <summary>
        /// The place the commodity is traded from
        /// </summary>
        public string Outlet { get; set; }

        /// <summary>
        /// The price to outlet buys a commodity for
        /// </summary>
        public int BuyPrice { get; set; }

        /// <summary>
        /// The amount of the commodity the outlet will buy
        /// </summary>
        public int WillBuy { get; set; }

        /// <summary>
        /// The price the outlet sells the commodity for
        /// </summary>
        public int SellPrice { get; set; }

        /// <summary>
        /// The amount of the commodity the outlet will sell
        /// </summary>
        public int WillSell { get; set; }
    }
}
