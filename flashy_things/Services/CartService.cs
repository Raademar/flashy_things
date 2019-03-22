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

        public bool SubmitOrder(Order order)
        {
            if (string.IsNullOrEmpty(order?.Name) ||
                string.IsNullOrEmpty(order?.Street) ||
                string.IsNullOrEmpty(order?.City) ||
                string.IsNullOrEmpty(order?.ZipCode) ||
                string.IsNullOrEmpty(order?.Telephone) ||
                string.IsNullOrEmpty(order?.Email))
            {
                return false;
            }
            return this.cartRepository.SubmitOrder(order);
        }
    }
}