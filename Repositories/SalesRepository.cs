using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class SalesRepository:ISale
    {
      private readonly ApplicationDbContext db;

        public SalesRepository(ApplicationDbContext db)
        {
           this.db = db; 
        }

        public async Task<SalesResponse> AddToSaleAsync(Sale sale)
        {
            await db.Sales.AddAsync(sale);
            await db.SaveChangesAsync();
            return new SalesResponse{Ok=true,Massage = "تمت الاضافة بنجاح"};
        }

        public async Task<IEnumerable<Sale>> GetSaleByDate(DateTime date)
        {
            return await db.Sales.Where(x => x.Date == date).ToListAsync();
        }

        public async Task<SalesAmount> GetSalesAmount()
        {
           var sales = await db.Sales.ToListAsync();
           var amunt = new SalesAmount();
           foreach(var item in sales){
              amunt.Amount += item.Amount;
           }
           
           return amunt;
        }

        public async Task<IEnumerable<Sale>> GetSalesAsync()
        {
            return await db.Sales.ToListAsync();
        }
    }
}