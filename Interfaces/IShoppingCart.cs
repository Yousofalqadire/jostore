using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Models;

namespace Api.Interfaces
{
    public interface IShoppingCart
    {
        Task<AddToCartResponse> AddToShoppingCart(AddProductToCartDto model);
        Task<AddToCartResponse> AddListOfProductsToShippingCart(List<AddProductToCartDto> products);
        Task<IEnumerable<ShoppingCart>> GetShoppingCartsAssync(string user);
        Task DeleteItemAsync(int id);
    }
}