using Basket.API.Exceptions;
using Basket.API.Models;
using BuildingBlocks.Exceptions;
using Marten;

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession sesion) : IBasketRepository
    {


        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await sesion.LoadAsync<ShoppingCart>(userName, cancellationToken);

            return basket is null ? throw new BasketNotFoundException(userName):basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            sesion.Store(basket);
            await sesion.SaveChangesAsync();
            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            sesion.Delete<ShoppingCart>(userName);
            await sesion.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
