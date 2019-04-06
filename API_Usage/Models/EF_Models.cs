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

    public class Stats
    {
        public Volume volume { get; set; }
        public Symbolstraded symbolsTraded { get; set; }
        public Routedvolume routedVolume { get; set; }
        public Notional notional { get; set; }
        public Marketshare marketShare { get; set; }
    }

    public class Volume
    {
        public int value { get; set; }
        public long lastUpdated { get; set; }
    }

    public class Symbolstraded
    {
        public int value { get; set; }
        public long lastUpdated { get; set; }
    }

    public class Routedvolume
    {
        public int value { get; set; }
        public long lastUpdated { get; set; }
    }

    public class Notional
    {
        public int value { get; set; }
        public long lastUpdated { get; set; }
    }

    public class Marketshare
    {
        public float value { get; set; }
        public long lastUpdated { get; set; }
    }


    public class Transaction
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public long effectiveDate { get; set; }
        public string fullName { get; set; }
        public string reportedTitle { get; set; }
        public int tranPrice { get; set; }
        public int tranShares { get; set; }
        public int tranValue { get; set; }
    }


    public class MarketVolume
    {
        public Class2[] Property1 { get; set; }
    }

    public class Class2
    {
        public string mic { get; set; }
        public string tapeId { get; set; }
        public string venueName { get; set; }
        public int volume { get; set; }
        public int tapeA { get; set; }
        public int tapeB { get; set; }
        public int tapeC { get; set; }
        public float marketPercent { get; set; }
        public long lastUpdated { get; set; }
    }



}