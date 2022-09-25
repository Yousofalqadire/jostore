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
    public class BillRepository:IBill
    {
        private readonly ApplicationDbContext db;

        public BillRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<SaveBillResponse> DeleteBill(int id)
        {
            var bill = await db.Bills.SingleOrDefaultAsync(x => x.Id == id);
            var bill_details = await db.BillDetails.Where(x => x.BillId == bill.Id).ToListAsync();
             db.BillDetails.RemoveRange(bill_details);
             await db.SaveChangesAsync();
             db.Bills.Remove(bill);
             await db.SaveChangesAsync();
            return new SaveBillResponse{Ok = true,Massage ="تمت العملية بنجاح"};
        }

        public async Task<Bill> GetBillById(int id)
        {
            return await db.Bills.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<BillDetails>> GetBillDetails(int id)
        {
            return await db.BillDetails.Where(x => x.BillId == id).ToListAsync();
        }

        public async Task<IEnumerable<Bill>> GetBillsAsync()
        {
            return await db.Bills.ToListAsync(); 
        }

        public async Task<SaveBillResponse> SaveBillAsync(BillDto bill)
        {
           await db.Bills.AddAsync(new Bill{UserName = bill.UserName,Date = DateTime.Now,
                                   Address = bill.Address,Phone = bill.Phone,Status = 0});
           await db.SaveChangesAsync();
           var billId = db.Bills.Select(x => x.Id).Max();
           var cartItems = await db.ShoppinCarts.Where(x => x.User == bill.UserName)
           .ToListAsync(); 
           foreach (var item in cartItems){
            var billDetailsItem = new BillDetails{
                BillId = billId,
                ProductBrand = item.ProductName,
                ProductId = item.ProductId,
                ProductPrice = item.ProductPric,
                Quantity = item.Quantity,
                SelectedSize = item.SelectedSize,
                TotalPrice = item.TotalPrice
            };
            await db.BillDetails.AddAsync(billDetailsItem);
            await db.SaveChangesAsync();
           }
           db.ShoppinCarts.RemoveRange(cartItems);
           await db.SaveChangesAsync();
           return new SaveBillResponse
           {Ok = true,
           Massage = "تم حفظ الفاتورة بنجاح سيتم التوصيل خلال مدة اقصاها ثلاثة أيام"
           } ;                      
            
        }    
    }
}