﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using Newtonsoft.Json;
using TdInterface.Tda;
using TdInterface.Tda.Model;

namespace TdInterface.Tests
{
    [TestClass()]
    public class OrderHelperTests
    {
        [TestMethod()]
        public void CreateTriggerOcoOrderTest_Buy_Market()
        {
            var expectedOrderStrategyType = "TRIGGER";

            var expectedTriggerOrderType = "MARKET";
            var expectedSymbol = "AAPL";
            var expectedTriggerQuantity = 100;
            var expectedTriggerLimit = 0.0D;
            var expectedLimitQuantity = 25;
            var expectedInstruction = "BUY";
            var expectedStopPrice = 147.00D;
            var expectedLimitPrice = 148.00D;

            var expectedChildStrategy = "OCO";


            var actual = OrderHelper.CreateTriggerOcoOrder(expectedTriggerOrderType, expectedSymbol, expectedInstruction, expectedTriggerQuantity, expectedTriggerLimit, expectedLimitQuantity, expectedLimitPrice, expectedStopPrice);

            Assert.AreEqual(expectedOrderStrategyType, actual.orderStrategyType);
            Assert.AreEqual(expectedTriggerOrderType, actual.orderType);
            Assert.AreEqual(expectedSymbol, actual.orderLegCollection[0].instrument.symbol, expectedSymbol);
            Assert.AreEqual(expectedTriggerQuantity, actual.orderLegCollection[0].quantity);
            Assert.IsNull(actual.price);

            Assert.AreEqual(expectedChildStrategy, actual.childOrderStrategies[0].orderStrategyType);

            var stopOrder = actual.childOrderStrategies[0].childOrderStrategies.Where(o => o.orderType == "STOP").FirstOrDefault();
            Assert.IsNotNull(stopOrder);
            Assert.AreEqual(expectedSymbol, stopOrder.orderLegCollection[0].instrument.symbol);
            Assert.AreEqual("STOP", stopOrder.orderType);
            Assert.AreEqual("SELL", stopOrder.orderLegCollection[0].instruction);
            Assert.AreEqual(expectedStopPrice.ToString("0.00"), stopOrder.stopPrice);


            var limitOrder = actual.childOrderStrategies[0].childOrderStrategies.Where(o => o.orderType == "LIMIT").FirstOrDefault();
            Assert.IsNotNull(limitOrder);
            Assert.AreEqual(expectedSymbol, limitOrder.orderLegCollection[0].instrument.symbol);
            Assert.AreEqual("LIMIT", limitOrder.orderType);
            Assert.AreEqual("SELL", limitOrder.orderLegCollection[0].instruction);
            Assert.AreEqual(expectedLimitPrice.ToString("0.00"), limitOrder.price);


        }

        [TestMethod]
        public void CreateTriggerOcoOrderTest_Buy_Limit()
        {
            var expectedOrderStrategyType = "TRIGGER";

            var expectedTriggerOrderType = "LIMIT";
            var expectedSymbol = "AAPL";
            var expectedTriggerQuantity = 100;
            var expectedTriggerLimit = 147.50;
            var expectedLimitQuantity = 25;
            var expectedInstruction = "BUY";
            var expectedStopPrice = 147.00D;
            var expectedLimitPrice = 148.00D;

            var expectedChildStrategy = "OCO";


            var actual = OrderHelper.CreateTriggerOcoOrder(expectedTriggerOrderType, expectedSymbol, expectedInstruction, expectedTriggerQuantity, expectedTriggerLimit, expectedLimitQuantity, expectedLimitPrice, expectedStopPrice);

            Assert.AreEqual(expectedOrderStrategyType, actual.orderStrategyType);
            Assert.AreEqual(expectedTriggerOrderType, actual.orderType);
            Assert.AreEqual(expectedSymbol, actual.orderLegCollection[0].instrument.symbol, expectedSymbol);
            Assert.AreEqual(expectedTriggerQuantity, actual.orderLegCollection[0].quantity);
            Assert.AreEqual(expectedTriggerLimit.ToString("#.00"), actual.price);

            Assert.AreEqual(expectedChildStrategy, actual.childOrderStrategies[0].orderStrategyType);

            var stopOrder = actual.childOrderStrategies[0].childOrderStrategies.Where(o => o.orderType == "STOP").FirstOrDefault();
            Assert.IsNotNull(stopOrder);
            Assert.AreEqual(expectedSymbol, stopOrder.orderLegCollection[0].instrument.symbol);
            Assert.AreEqual("STOP", stopOrder.orderType);
            Assert.AreEqual("SELL", stopOrder.orderLegCollection[0].instruction);
            Assert.AreEqual(expectedStopPrice.ToString("0.00"), stopOrder.stopPrice);


            var limitOrder = actual.childOrderStrategies[0].childOrderStrategies.Where(o => o.orderType == "LIMIT").FirstOrDefault();
            Assert.IsNotNull(limitOrder);
            Assert.AreEqual(expectedSymbol, limitOrder.orderLegCollection[0].instrument.symbol);
            Assert.AreEqual("LIMIT", limitOrder.orderType);
            Assert.AreEqual("SELL", limitOrder.orderLegCollection[0].instruction);
            Assert.AreEqual(expectedLimitPrice.ToString("0.00"), limitOrder.price);
        }

        [TestMethod()]
        public void CreateTriggerOcoOrderTest_Sell_Market()
        {
            var expectedOrderStrategyType = "TRIGGER";

            var expectedTriggerOrderType = "MARKET";
            var expectedSymbol = "AAPL";
            var expectedTriggerQuantity = 100;
            var expectedTriggerLimit = 0.0D;
            var expectedLimitQuantity = 25;
            var expectedInstruction = "Sell";
            var expectedStopPrice = 148.00D;
            var expectedLimitPrice = 147.00D;

            var expectedChildStrategy = "OCO";


            var actual = OrderHelper.CreateTriggerOcoOrder(expectedTriggerOrderType, expectedSymbol, expectedInstruction, expectedTriggerQuantity, expectedTriggerLimit, expectedLimitQuantity, expectedLimitPrice, expectedStopPrice);

            Assert.AreEqual(expectedOrderStrategyType, actual.orderStrategyType);
            Assert.AreEqual(expectedTriggerOrderType, actual.orderType);
            Assert.AreEqual(expectedSymbol, actual.orderLegCollection[0].instrument.symbol, expectedSymbol);
            Assert.AreEqual(expectedTriggerQuantity, actual.orderLegCollection[0].quantity);
            Assert.IsNull(actual.price);

            Assert.AreEqual(expectedChildStrategy, actual.childOrderStrategies[0].orderStrategyType);

            var stopOrder = actual.childOrderStrategies[0].childOrderStrategies.Where(o => o.orderType == "STOP").FirstOrDefault();
            Assert.IsNotNull(stopOrder);
            Assert.AreEqual(expectedSymbol, stopOrder.orderLegCollection[0].instrument.symbol);
            Assert.AreEqual("STOP", stopOrder.orderType);
            Assert.AreEqual(OrderHelper.BUY_TO_COVER, stopOrder.orderLegCollection[0].instruction);
            Assert.AreEqual(expectedStopPrice.ToString("0.00"), stopOrder.stopPrice);


            var limitOrder = actual.childOrderStrategies[0].childOrderStrategies.Where(o => o.orderType == "LIMIT").FirstOrDefault();
            Assert.IsNotNull(limitOrder);
            Assert.AreEqual(expectedSymbol, limitOrder.orderLegCollection[0].instrument.symbol);
            Assert.AreEqual("LIMIT", limitOrder.orderType);
            Assert.AreEqual(OrderHelper.BUY_TO_COVER, limitOrder.orderLegCollection[0].instruction);
            Assert.AreEqual(expectedLimitPrice.ToString("0.00"), limitOrder.price);
        }

        [TestMethod]
        public void CreateTriggerOcoOrderTest_Sell_Limit()
        {
            var expectedOrderStrategyType = "TRIGGER";

            var expectedTriggerOrderType = "LIMIT";
            var expectedSymbol = "AAPL";
            var expectedTriggerQuantity = 100;
            var expectedTriggerLimit = 147.50;
            var expectedLimitQuantity = 25;
            var expectedInstruction = "SELL";
            var expectedStopPrice = 148.00D;
            var expectedLimitPrice = 147.00D;

            var expectedChildStrategy = "OCO";


            var actual = OrderHelper.CreateTriggerOcoOrder(expectedTriggerOrderType, expectedSymbol, expectedInstruction, expectedTriggerQuantity, expectedTriggerLimit, expectedLimitQuantity, expectedLimitPrice, expectedStopPrice);

            Assert.AreEqual(expectedOrderStrategyType, actual.orderStrategyType);
            Assert.AreEqual(expectedTriggerOrderType, actual.orderType);
            Assert.AreEqual(expectedSymbol, actual.orderLegCollection[0].instrument.symbol, expectedSymbol);
            Assert.AreEqual(expectedTriggerQuantity, actual.orderLegCollection[0].quantity);
            Assert.AreEqual(expectedTriggerLimit.ToString("#.00"), actual.price);

            Assert.AreEqual(expectedChildStrategy, actual.childOrderStrategies[0].orderStrategyType);

            var stopOrder = actual.childOrderStrategies[0].childOrderStrategies.Where(o => o.orderType == "STOP").FirstOrDefault();
            Assert.IsNotNull(stopOrder);
            Assert.AreEqual(expectedSymbol, stopOrder.orderLegCollection[0].instrument.symbol);
            Assert.AreEqual("STOP", stopOrder.orderType);
            Assert.AreEqual(OrderHelper.BUY_TO_COVER, stopOrder.orderLegCollection[0].instruction);
            Assert.AreEqual(expectedStopPrice.ToString("0.00"), stopOrder.stopPrice);


            var limitOrder = actual.childOrderStrategies[0].childOrderStrategies.Where(o => o.orderType == "LIMIT").FirstOrDefault();
            Assert.IsNotNull(limitOrder);
            Assert.AreEqual(expectedSymbol, limitOrder.orderLegCollection[0].instrument.symbol);
            Assert.AreEqual("LIMIT", limitOrder.orderType);
            Assert.AreEqual(OrderHelper.BUY_TO_COVER, limitOrder.orderLegCollection[0].instruction);
            Assert.AreEqual(expectedLimitPrice.ToString("0.00"), limitOrder.price);
        }


        [TestMethod]
        public void Flatten()
        {
            var json = "{\"type\":\"MARGIN\",\"accountId\":\"123456\",\"roundTrips\":102,\"isDayTrader\":true,\"isClosingOnlyRestricted\":false,\"positions\":null,\"initialBalances\":{\"accruedInterest\":0.0,\"availableFundsNonMarginableTrade\":25254.57,\"bondValue\":0.0,\"buyingPower\":64813.6,\"cashBalance\":25259.69,\"cashAvailableForTrading\":0.0,\"cashReceipts\":0.0,\"dayTradingBuyingPower\":101018.28,\"dayTradingBuyingPowerCall\":0.0,\"dayTradingEquityCall\":0.0,\"equity\":25254.57,\"equityPercentage\":100.0,\"liquidationValue\":25254.54,\"longMarginValue\":0.0,\"longOptionMarketValue\":0.0,\"longStockValue\":0.0,\"maintenanceCall\":0.0,\"maintenanceRequirement\":0.0,\"margin\":25259.69,\"marginEquity\":25259.69,\"moneyMarketFund\":0.0,\"mutualFundValue\":0.0,\"regTCall\":0.0,\"shortMarginValue\":0.0,\"shortOptionMarketValue\":0.0,\"shortStockValue\":0.0,\"totalCash\":25259.69,\"isInCall\":false,\"pendingDeposits\":0.0,\"marginBalance\":0.0,\"shortBalance\":-5.12,\"accountValue\":25254.54},\"currentBalances\":{\"accruedInterest\":0.0,\"cashBalance\":25255.27,\"cashReceipts\":0.0,\"longOptionMarketValue\":0.0,\"liquidationValue\":25267.52,\"longMarketValue\":0.0,\"moneyMarketFund\":0.0,\"savings\":0.0,\"shortMarketValue\":0.0,\"pendingDeposits\":0.0,\"availableFunds\":25267.52,\"availableFundsNonMarginableTrade\":25267.52,\"buyingPower\":64800.34,\"buyingPowerNonMarginableTrade\":24107.65,\"dayTradingBuyingPower\":97138.44,\"equity\":25267.52,\"equityPercentage\":100.0,\"longMarginValue\":0.0,\"maintenanceCall\":0.0,\"maintenanceRequirement\":0.0,\"marginBalance\":0.0,\"regTCall\":0.0,\"shortBalance\":12.25,\"shortMarginValue\":0.0,\"shortOptionMarketValue\":0.0,\"sma\":32400.17,\"mutualFundValue\":0.0,\"bondValue\":0.0},\"projectedBalances\":{\"availableFunds\":24107.65,\"availableFundsNonMarginableTrade\":24107.65,\"buyingPower\":60934.1,\"dayTradingBuyingPower\":97138.44,\"dayTradingBuyingPowerCall\":0.0,\"maintenanceCall\":0.0,\"regTCall\":0.0,\"isInCall\":false,\"stockBuyingPower\":60934.1},\"orderStrategies\":[{\"orderId\":\"5846530521\",\"orderType\":\"MARKET\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"TRIGGER\",\"status\":\"QUEUED\",\"orderLegCollection\":[{\"instruction\":\"BUY\",\"quantity\":28.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"037833100\",\"symbol\":\"AAPL\",\"description\":null}}],\"childOrderStrategies\":[{\"orderId\":\"5846530522\",\"orderType\":null,\"session\":null,\"duration\":null,\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"OCO\",\"status\":null,\"orderLegCollection\":null,\"childOrderStrategies\":[{\"orderId\":\"5846530524\",\"orderType\":\"LIMIT\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":\"138.26\",\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"SELL\",\"quantity\":7.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"037833100\",\"symbol\":\"AAPL\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5846530523\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"137.9\",\"orderStrategyType\":\"SINGLE\",\"status\":\"AWAITING_PARENT_ORDER\",\"orderLegCollection\":[{\"instruction\":\"SELL\",\"quantity\":28.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"037833100\",\"symbol\":\"AAPL\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5846522524\",\"orderType\":null,\"session\":null,\"duration\":null,\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"OCO\",\"status\":null,\"orderLegCollection\":null,\"childOrderStrategies\":[{\"orderId\":\"5846522526\",\"orderType\":\"LIMIT\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":\"138.25\",\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"AWAITING_PARENT_ORDER\",\"orderLegCollection\":[{\"instruction\":\"SELL\",\"quantity\":7.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"037833100\",\"symbol\":\"AAPL\",\"description\":null}}],\"childOrderStrategies\":null}]},{\"orderId\":\"5846522521\",\"orderType\":\"LIMIT\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":\"138.26\",\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"SELL\",\"quantity\":7.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"037833100\",\"symbol\":\"AAPL\",\"description\":null}}],\"childOrderStrategies\":null}]}]},{\"orderId\":\"5844574232\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"118.41\",\"orderStrategyType\":\"SINGLE\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":14.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844637723\",\"orderType\":\"MARKET\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"TRIGGER\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"SELL_SHORT\",\"quantity\":11.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":[{\"orderId\":\"5844637724\",\"orderType\":null,\"session\":null,\"duration\":null,\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"OCO\",\"status\":null,\"orderLegCollection\":null,\"childOrderStrategies\":[{\"orderId\":\"5844637726\",\"orderType\":\"LIMIT\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":\"117.32\",\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":3.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844637725\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"118.2\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":11.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null}]}]},{\"orderId\":\"5844683066\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"117.83\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":3.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844637780\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"117.83\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":8.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844853195\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"141.36\",\"orderStrategyType\":\"SINGLE\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":11.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"037833100\",\"symbol\":\"AAPL\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844675027\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"117.83\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":5.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5846522522\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"137.95\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"SELL_SHORT\",\"quantity\":21.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"023135106\",\"symbol\":\"AMZN\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844683058\",\"orderType\":\"MARKET\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":2.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844701555\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"117.2\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":2.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844701582\",\"orderType\":\"MARKET\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":2.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844841554\",\"orderType\":\"MARKET\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"TRIGGER\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"SELL_SHORT\",\"quantity\":15.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"037833100\",\"symbol\":\"AAPL\",\"description\":null}}],\"childOrderStrategies\":[{\"orderId\":\"5844841555\",\"orderType\":null,\"session\":null,\"duration\":null,\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"OCO\",\"status\":null,\"orderLegCollection\":null,\"childOrderStrategies\":[{\"orderId\":\"5844841557\",\"orderType\":\"LIMIT\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":\"141.04\",\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":4.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"037833100\",\"symbol\":\"AAPL\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844841556\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"141.7\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":15.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"037833100\",\"symbol\":\"AAPL\",\"description\":null}}],\"childOrderStrategies\":null}]}]},{\"orderId\":\"5844574212\",\"orderType\":\"MARKET\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"TRIGGER\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"SELL_SHORT\",\"quantity\":19.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":[{\"orderId\":\"5844574213\",\"orderType\":null,\"session\":null,\"duration\":null,\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"OCO\",\"status\":null,\"orderLegCollection\":null,\"childOrderStrategies\":[{\"orderId\":\"5844574215\",\"orderType\":\"LIMIT\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":\"118.16\",\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":5.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844574214\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"118.7\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":19.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null}]}]},{\"orderId\":\"5844688646\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"117.2\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":3.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844675009\",\"orderType\":\"MARKET\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":3.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844705217\",\"orderType\":\"MARKET\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":null,\"orderStrategyType\":\"SINGLE\",\"status\":\"FILLED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":1.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null},{\"orderId\":\"5844688672\",\"orderType\":\"STOP\",\"session\":\"NORMAL\",\"duration\":\"DAY\",\"price\":null,\"stopPrice\":\"116.8\",\"orderStrategyType\":\"SINGLE\",\"status\":\"CANCELED\",\"orderLegCollection\":[{\"instruction\":\"BUY_TO_COVER\",\"quantity\":2.0,\"instrument\":{\"assetType\":\"EQUITY\",\"cusip\":\"67066G104\",\"symbol\":\"NVDA\",\"description\":null}}],\"childOrderStrategies\":null}]}";

            var securitiesaccount = JsonConvert.DeserializeObject<Securitiesaccount>(json);

            var flatOrders =securitiesaccount.FlatOrders;
        }

    }
}