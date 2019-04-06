using Microsoft.AspNetCore.Mvc;
using API_Usage.DataAccess;
using API_Usage.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

/*
 * Acknowledgments
 *  v1 of the project was created for the Fall 2018 class by Dhruv Dhiman, MS BAIS '18
 *    This example showed how to use v1 of the IEXTrading API
 *    
 *  Kartikay Bali (MS BAIS '19) extended the project for Spring 2019 by demonstrating 
 *    how to use similar methods to access Azure ML models
*/

namespace API_Usage.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext dbContext;

        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /// <summary>
        /// Initialize the database connection and HttpClient object
        /// </summary>
        /// <param name="context"></param>
        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IActionResult Index()
        {
            return View();
        }

        /****
         * The Symbols action calls the GetSymbols method that returns a list of Companies.
         * This list of Companies is passed to the Symbols View.
        ****/
        public IActionResult Symbols()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            List<Company> companies = GetSymbols();

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Companies"] = JsonConvert.SerializeObject(companies);
            //TempData["Companies"] = companies;

            return View(companies);
        }

        public IActionResult Statistics()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            List<Stats> stats = GetStats();

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Statistics"] = JsonConvert.SerializeObject(stats);
            //TempData["Companies"] = companies;

            return View(stats);
        }

        public IActionResult Transactions(String symbol)
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            List<Transaction> trans = GetTransaction(symbol);

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Trans"] = JsonConvert.SerializeObject(trans);
            //TempData["Companies"] = companies;

            return View(trans);
        }

        public IActionResult MarketVolume()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            List<MarketVolume> mv = GetMarketVolume();

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["MarketVolume"] = JsonConvert.SerializeObject(mv);
            //TempData["Companies"] = companies;

            return View(mv);
        }

        /****
         * The Chart action calls the GetChart method that returns 1 year's equities for the passed symbol.
         * A ViewModel CompaniesEquities containing the list of companies, prices, volumes, avg price and volume.
         * This ViewModel is passed to the Chart view.
        ****/
        /// <summary>
        /// The Chart action calls the GetChart method that returns 1 year's equities for the passed symbol.
        /// A ViewModel CompaniesEquities containing the list of companies, prices, volumes, avg price and volume.
        /// This ViewModel is passed to the Chart view.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public IActionResult Chart(string symbol)
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessChart = 0;
            List<Equity> equities = new List<Equity>();

            if (symbol != null)
            {
                equities = GetChart(symbol);
                equities = equities.OrderBy(c => c.date).ToList(); //Make sure the data is in ascending order of date.
            }

            CompaniesEquities companiesEquities = getCompaniesEquitiesModel(equities);

            return View(companiesEquities);
        }

        /// <summary>
        /// Calls the IEX reference API to get the list of symbols
        /// </summary>
        /// <returns>A list of the companies whose information is available</returns>
        public List<Company> GetSymbols()
        {
            string IEXTrading_API_PATH = BASE_URL + "ref-data/symbols";
            string companyList = "";
            List<Company> companies = null;

            // connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                companyList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // now, parse the Json strings as C# objects
            if (!companyList.Equals(""))
            {
                // https://stackoverflow.com/a/46280739
                //JObject result = JsonConvert.DeserializeObject<JObject>(companyList);
                companies = JsonConvert.DeserializeObject<List<Company>>(companyList);
                companies = companies.GetRange(0, 50);
            }

            return companies;
        }

        public List<Transaction> GetTransaction(String symbol)
        {
            string IEXTrading_API_PATH = BASE_URL + "stock/" +symbol+ "insider-summary";
            string transList = "";
            List<Transaction> transactions = null;

            // connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                transList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // now, parse the Json strings as C# objects
            if (!transList.Equals(""))
            {
                // https://stackoverflow.com/a/46280739
                //JObject result = JsonConvert.DeserializeObject<JObject>(companyList);
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(transList);
                transactions = transactions.GetRange(0, 50);
            }

            return transactions;
        }

        public List<Stats> GetStats()
        {
            string IEXTrading_API_PATH = BASE_URL + "stats/intraday";
            string statList = "";
            List<Stats> stats = null;

            // connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                statList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // now, parse the Json strings as C# objects
            if (!statList.Equals(""))
            {
                // https://stackoverflow.com/a/46280739
                //JObject result = JsonConvert.DeserializeObject<JObject>(companyList);
                stats = JsonConvert.DeserializeObject<List<Stats>>(statList);
                stats = stats.GetRange(0, 50);
            }

            return stats;
        }


        public List<MarketVolume> GetMarketVolume()
        {
            string IEXTrading_API_PATH = BASE_URL + "market";
            string volumeList = "";
            List<MarketVolume> marketVolumes = null;

            // connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                volumeList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // now, parse the Json strings as C# objects
            if (!volumeList.Equals(""))
            {
                // https://stackoverflow.com/a/46280739
                //JObject result = JsonConvert.DeserializeObject<JObject>(companyList);
                marketVolumes = JsonConvert.DeserializeObject<List<MarketVolume>>(volumeList);
                //marketVolumes = marketVolumes.GetRange(0, 50);
            }

            return marketVolumes;
        }


        /// <summary>
        /// Calls the IEX stock API to get 1 year's chart for the supplied symbol
        /// </summary>
        /// <param name="symbol">Stock symbol of the company whose quotes are to be retrieved</param>
        /// <returns></returns>
        public List<Equity> GetChart(string symbol)
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "stock/" + symbol + "/batch?types=chart&range=1y";

            // initialize objects needed to gather data
            string charts = "";
            List<Equity> Equities = new List<Equity>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                charts = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!charts.Equals(""))
            {
                ChartRoot root = JsonConvert.DeserializeObject<ChartRoot>(charts,
                  new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                Equities = root.chart.ToList();
            }

            // fix the relations. By default the quotes do not have the company symbol
            //  this symbol serves as the foreign key in the database and connects the quote to the company
            foreach (Equity Equity in Equities)
            {
                Equity.symbol = symbol;
            }

            return Equities;
        }

        /// <summary>
        /// Call the ClearTables method to delete records from a table or all tables.
        ///  Count of current records for each table is passed to the Refresh View
        /// </summary>
        /// <param name="tableToDel">Table to clear</param>
        /// <returns>Refresh view</returns>
        public IActionResult Refresh(string tableToDel)
        {
            ClearTables(tableToDel);
            Dictionary<string, int> tableCount = new Dictionary<string, int>();
            tableCount.Add("Companies", dbContext.Companies.Count());
            tableCount.Add("Charts", dbContext.Equities.Count());
            return View(tableCount);
        }

        /// <summary>
        /// save the quotes (equities) in the database
        /// </summary>
        /// <param name="symbol">Company whose quotes are to be saved</param>
        /// <returns>Chart view for the company</returns>
        public IActionResult SaveCharts(string symbol)
        {
            List<Equity> equities = GetChart(symbol);

            // save the quote if the quote has not already been saved in the database
            foreach (Equity equity in equities)
            {
                if (dbContext.Equities.Where(c => c.date.Equals(equity.date)).Count() == 0)
                {
                    dbContext.Equities.Add(equity);
                }
            }

            // persist the data
            dbContext.SaveChanges();

            // populate the models to render in the view
            ViewBag.dbSuccessChart = 1;
            CompaniesEquities companiesEquities = getCompaniesEquitiesModel(equities);
            return View("Chart", companiesEquities);
        }

        /// <summary>
        /// Use the data provided to assemble the ViewModel
        /// </summary>
        /// <param name="equities">Quotes to dsiplay</param>
        /// <returns>The view model to include </returns>
        public CompaniesEquities getCompaniesEquitiesModel(List<Equity> equities)
        {
            List<Company> companies = dbContext.Companies.ToList();

            if (equities.Count == 0)
            {
                return new CompaniesEquities(companies, null, "", "", "", 0, 0);
            }

            Equity current = equities.Last();

            // create appropriately formatted strings for use by chart.js
            string dates = string.Join(",", equities.Select(e => e.date));
            string prices = string.Join(",", equities.Select(e => e.high));
            float avgprice = equities.Average(e => e.high);

            //Divide volumes by million to scale appropriately
            string volumes = string.Join(",", equities.Select(e => e.volume / 1000000));
            double avgvol = equities.Average(e => e.volume) / 1000000;

            return new CompaniesEquities(companies, equities.Last(), dates, prices, volumes, avgprice, avgvol);
        }

        /// <summary>
        /// Save the available symbols in the database
        /// </summary>
        /// <returns></returns>
        public IActionResult PopulateSymbols()
        {
            // retrieve the companies that were saved in the symbols method
            // saving in TempData is extremely inefficient - the data circles back from the browser
            // better methods would be to serialize to the hard disk, or save directly into the database
            //  in the symbols method. This example has been structured to demonstrate one way to save object data
            //  and retrieve it later
            List<Company> companies = JsonConvert.DeserializeObject<List<Company>>(TempData["Companies"].ToString());

            foreach (Company company in companies)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.Companies.Where(c => c.symbol.Equals(company.symbol)).Count() == 0)
                {
                    dbContext.Companies.Add(company);
                }
            }

            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("Symbols", companies);
        }

        /// <summary>
        /// Delete all records from tables
        /// </summary>
        /// <param name="tableToDel">Table to clear</param>
        public void ClearTables(string tableToDel)
        {
            if ("all".Equals(tableToDel))
            {
                //First remove equities and then the companies
                dbContext.Equities.RemoveRange(dbContext.Equities);
                dbContext.Companies.RemoveRange(dbContext.Companies);
            }
            else if ("Companies".Equals(tableToDel))
            {
                //Remove only those companies that don't have related quotes stored in the Equities table
                dbContext.Companies.RemoveRange(dbContext.Companies
                                                         .Where(c => c.Equities.Count == 0)
                                                                      );
            }
            else if ("Charts".Equals(tableToDel))
            {
                dbContext.Equities.RemoveRange(dbContext.Equities);
            }
            dbContext.SaveChanges();
        }
    }
}




public class Rootobject
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
    public long value { get; set; }
    public long lastUpdated { get; set; }
}

public class Marketshare
{
    public float value { get; set; }
    public long lastUpdated { get; set; }
}
