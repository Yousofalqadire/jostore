using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Models;

namespace Api.Interfaces
{
    public interface ISale
    {
        Task<SalesResponse> AddToSaleAsync(Sale sale);
        Task<IEnumerable<Sale>> GetSalesAsync();
        Task<IEnumerable<Sale>>GetSaleByDate(DateTime date);
        Task<SalesAmount> GetSalesAmount();

    }
}