using System;
using Newtonsoft.Json;

namespace StockMarketApp
{
    public class StockData
    {
        [JsonProperty("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }

    }
}

