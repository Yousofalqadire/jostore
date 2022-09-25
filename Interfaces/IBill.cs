using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Models;

namespace Api.Interfaces
{
    public interface IBill
    {
        Task<SaveBillResponse> SaveBillAsync(BillDto bill);
        Task<IEnumerable<Bill>> GetBillsAsync();
        Task<IEnumerable<BillDetails>> GetBillDetails(int id);
        Task<Bill> GetBillById(int id);
        Task<SaveBillResponse>  DeleteBill(int id);
    }
}