using stockservice.Model;
using System;
using System.Collections.Generic;

namespace stockservice.Repositories
{
    public interface IStockRepository
    {
        List<Stock> GetStockList();
        string AddStockPrice(Stock stock);
        List<Stock> GetStockDetails(string companyCode, DateTime startDate, DateTime endDate);

    }
}
