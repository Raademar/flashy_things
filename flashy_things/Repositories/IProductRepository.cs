using System.Collections.Generic;
using flashy_things.Models;

namespace flashy_things.Repositories
{
    public interface IProductRepository
    {
        List<Product> Get();

        Product Get(int id);

        bool Add(Product product);

        bool Delete(int id);

        bool AddToCart(CartItem cartItem, int ProductId);
    }
}