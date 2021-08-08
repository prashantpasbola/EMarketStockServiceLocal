using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using stockservice.DBContext;
using stockservice.Model;
using stockservice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stockservice.Services
{
    public class StockService : IStockRepository
    {
        private readonly IMongoCollection<Company> _Company;
        private readonly StockMarketDBContext _context;

        public StockService(StockMarketDBContext context, IConfiguration config)
        {
            _context = context;
            var client = new MongoClient(config.GetConnectionString("MongoTestConnection"));
            var database = client.GetDatabase("EStockMarketDb");
            _Company = database.GetCollection<Company>("Company");
        }

        public List<Stock> GetStockList()
        {

            List<Company> company = new List<Company>();
            List<Stock> stock = new List<Stock>();

            company = _Company.Find(com => true).ToList();
            if (company.Count > 0)
            {
                var distinctCode = company.Select(m => m.CompanyCode).Distinct();
                stock = _context.Stocks.Where(x => distinctCode.Contains(x.CompanyCode)).ToList();
            }
            return stock;
        }

        public string AddStockPrice(Stock stock)
        {
            // Company company = _Company.Find(c => c.CompanyCode.Equals(stock.CompanyCode)).FirstOrDefault();
            Stock stockCheck = _context.Stocks.Where(c => c.StockId == stock.StockId).SingleOrDefault();
            string message = "";
            if (stockCheck == null)
            {
                _context.Stocks.Add(stock);
                _context.SaveChanges();
                message = "Stock Added Successfully !";
            }
            else
            {
                var stockDetails = stockCheck;
                stockDetails.StartDate = stock.StartDate;
                stockDetails.EndDate = stock.EndDate;
                stockDetails.StockPrice = stock.StockPrice;

                _context.SaveChanges();
                message = "Stock Updated Successfully !";

            }



            return message;
        }

        public List<Stock> GetStockDetails(string companyCode, DateTime startDate, DateTime endDate)
        {
            return _context.Stocks.Where(stock => stock.CompanyCode == companyCode && (stock.StartDate.Date >= startDate.Date && stock.StartDate.Date < endDate.Date)).ToList();
        }
    }
}
