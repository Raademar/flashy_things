using System.Collections.Generic;
using flashy_things.Models;
using flashy_things.Repositories;

namespace flashy_things.Services
{
    public class CartService
    {
        private readonly CartRepository cartRepository;
        
        public CartService(CartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        
        public List<CartItem> Get()
        {
            return this.cartRepository.Get();
        }

        public bool SubmitOrder(Cart cart)
        {
            return this.cartRepository.SubmitOrder(cart);
        }
    }
}