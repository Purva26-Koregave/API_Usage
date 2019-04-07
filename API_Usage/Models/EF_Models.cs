using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_Usage.Models
{
    public class Company
    {
        [Key]
        public string symbol { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public bool isEnabled { get; set; }
        public string type { get; set; }
        public string iexId { get; set; }
        public List<Equity> Equities { get; set; }
    }

    public class Equity
    {
        public int EquityId { get; set; }
        public string date { get; set; }
        public float open { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float close { get; set; }
        public int volume { get; set; }
        public int unadjustedVolume { get; set; }
        public float change { get; set; }
        public float changePercent { get; set; }
        public float vwap { get; set; }
        public string label { get; set; }
        public float changeOverTime { get; set; }
        public string symbol { get; set; }
    }

    public class ChartRoot
    {
        public Equity[] chart { get; set; }
    }

    
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public long effectiveDate { get; set; }
        public string fullName { get; set; }
        public string reportedTitle { get; set; }
        public int tranPrice { get; set; }
        public int tranShares { get; set; }
        public int tranValue { get; set; }
    }



    public class Performance
    {
        [Key]
        public int PerformanceID { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public float performance { get; set; }
        public long lastUpdated { get; set; }
    }



    public class MarketVolume
    {
        [Key]
        public int MarketVolumeID { get; set; }
        public string mic { get; set; }
        public string tapeId { get; set; }
        public string venueName { get; set; }
        public long volume { get; set; }
        public int tapeA { get; set; }
        public int tapeB { get; set; }
        public int tapeC { get; set; }
        public float marketPercent { get; set; }
        public long lastUpdated { get; set; }
    }



    public class EffSpread
    {
        [Key]
        public int EffSpreadID { get; set; }
        public int volume { get; set; }
        public string venue { get; set; }
        public string venueName { get; set; }
        public float effectiveSpread { get; set; }
        public float effectiveQuoted { get; set; }
        public float priceImprovement { get; set; }
    }


    public class Gainers
    {
        [Key]
        public int GainersID { get; set; }
        public string symbol { get; set; }
        public string companyName { get; set; }
        public string primaryExchange { get; set; }
        public string sector { get; set; }
        public string calculationPrice { get; set; }
        public float open { get; set; }
        public long openTime { get; set; }
        public float close { get; set; }
        public long closeTime { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float latestPrice { get; set; }
        public string latestSource { get; set; }
        public string latestTime { get; set; }
        public long latestUpdate { get; set; }
        public int latestVolume { get; set; }
        public float iexRealtimePrice { get; set; }
        public float iexRealtimeSize { get; set; }
        public float iexLastUpdated { get; set; }
        public float delayedPrice { get; set; }
        public long delayedPriceTime { get; set; }
        public float extendedPrice { get; set; }
        public float extendedChange { get; set; }
        public float extendedChangePercent { get; set; }
        public long extendedPriceTime { get; set; }
        public float previousClose { get; set; }
        public float change { get; set; }
        public float changePercent { get; set; }
        public float iexMarketPercent { get; set; }
        public float iexVolume { get; set; }
        public int avgTotalVolume { get; set; }
        public float iexBidPrice { get; set; }
        public float iexBidSize { get; set; }
        public float iexAskPrice { get; set; }
        public float iexAskSize { get; set; }
        public long marketCap { get; set; }
        public float? peRatio { get; set; }
        public float week52High { get; set; }
        public float week52Low { get; set; }
        public float ytdChange { get; set; }
    }



}