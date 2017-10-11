using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeRouteMarketeer.Models.Market
{
    public class RouteCalculator
    {
        private readonly CommodityMarket _origin;
        private readonly CommodityMarket _destination;

        private readonly Dictionary<string, List<Trade>> _buys = new Dictionary<string, List<Trade>>();
        private readonly Dictionary<string, List<Trade>> _sales = new Dictionary<string, List<Trade>>();

        public RouteCalculator(CommodityMarket origin, CommodityMarket destination)
        {
            _origin = origin;
            _destination = destination;
        }

        public Route GetRoute()
        {
            var route = new Route(_origin.Island, _destination.Island);

            var destinationMarket = _destination.Rows.Where(x => x.WillBuy != 0 && x.BuyPrice != 0).OrderBy(x => x.Commodity);
            var originMarket = _origin.Rows.Where(x => x.WillSell != 0 && x.SellPrice != 0).OrderBy(x => x.Commodity);

            foreach (var row in destinationMarket)
            {
                var availableTrades = originMarket.Where(x => x.Commodity == row.Commodity && x.SellPrice < row.BuyPrice).ToList();
                
                if (!availableTrades.Any())
                    continue;

                var totalAmountToBuy = 0;

                foreach (var trade in availableTrades)
                {
                    if (row.WillBuy <= 0)
                        continue;

                    // Amount we should buy from the seller
                    int amountToBuy;

                    // Amount that is left to buy from the seller
                    int leftToBuy;

                    // Amount that can be sold to the buyer
                    int leftToSell;

                    if (trade.WillSell >= row.WillBuy)
                    {
                        amountToBuy = row.WillBuy;
                        leftToBuy = trade.WillSell + (row.WillBuy - trade.WillSell);
                        leftToSell = 0;
                    }
                    else
                    {
                        amountToBuy = trade.WillSell;
                        leftToBuy = 0;
                        leftToSell = Math.Max(0, row.WillBuy - trade.WillSell);
                    }

                    var purchase = new Trade(trade.Commodity, trade.Outlet, trade.SellPrice, amountToBuy);

                    // Add the buy order to the collection
                    if (_buys.ContainsKey(row.Commodity))
                        _buys[trade.Commodity].Add(purchase);
                    else
                        _buys.Add(row.Commodity, new List<Trade> { purchase });

                    // Set the amount we can still sell
                    trade.WillSell = leftToBuy;
                    row.WillBuy = leftToSell;

                    totalAmountToBuy += amountToBuy;
                }

                var sell = new Trade(row.Commodity, row.Outlet, row.BuyPrice, totalAmountToBuy);

                // Add the sell order to the collection
                if (_sales.ContainsKey(row.Commodity))
                    _sales[row.Commodity].Add(sell);
                else
                    _sales.Add(row.Commodity, new List<Trade> { sell });
            }

            route.ToBuy = _buys;
            route.ToSell = _sales;

            return route;
        }
    }
}
