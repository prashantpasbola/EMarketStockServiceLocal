using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stockservice.Model;
using stockservice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stockservice.Controllers
{
    [ApiController]
    [Route("api/v1.0/market/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
         public StocksController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        [HttpGet]
        [Route("getStock")]
        public ActionResult<List<Stock>> GetStockList()
        {
            var result = _stockRepository.GetStockList();
            return result;
        }



        [HttpPost]
        [Route("addStocks")]
        public IActionResult AddStockPrice([FromBody] Stock stock)
        {
            //stock.CompanyCode = companyCode;
            var result = _stockRepository.AddStockPrice(stock);
            return Ok(result);
        }

        [HttpGet]
        [Route("get/{companyCode}/{startDate}/{endDate}")]
        public IActionResult GetStocks(string companyCode, DateTime startDate, DateTime endDate)
        {
            var request = new StockDetails();
            var result = _stockRepository.GetStockDetails(companyCode, startDate, endDate);
            if (result != null && result.Count != 0)
            {
                request.Stocks = result;
                request.Min = (double)result.Min(x => x.StockPrice);
                request.Max = (double)result.Max(x => x.StockPrice);
                request.Avg = (double)result.Average(x => x.StockPrice);
            }

            else
            {
                request.Stocks = result;
            }
            return Ok(request);
        }

    }
}
