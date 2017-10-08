using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeRouteMarketeer.Models.Market
{
    public class Trade
    {
        public string Commodity { get; set; }
        
        public string Outlet { get; set; }
        
        public int TradePrice { get; set; }
        
        public int Amount { get; set; }

        public Trade(string commodity, string outlet, int tradePrice, int amount)
        {
            Commodity = commodity;
            Outlet = outlet;
            TradePrice = tradePrice;
            Amount = amount;
        }
    }
}
