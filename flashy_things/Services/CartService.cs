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
        
        public Cart Get(int id)
        {
            return this.cartRepository.Get(id);
        }

        public bool SubmitOrder(Cart cart, int customerId)
        {
            return this.cartRepository.SubmitOrder(cart, customerId);
        }
    }
}