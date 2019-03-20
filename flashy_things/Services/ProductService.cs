using System.Collections.Generic;
using flashy_things.Models;
using flashy_things.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace flashy_things.Services

{
    public class ProductService
    {
        private readonly ProductRepository productRepository;
        
        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<Product> Get()
        {
            return this.productRepository.Get();
        }

        public Product Get(int id)
        {
            return this.productRepository.Get(id);
        }

        public bool Add(Product product)
        {
            var result = this.productRepository.Add(product);

            if (!result)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            return this.productRepository.Delete(id);
        }

        public bool AddToCart(CartItem cartItem, int id)
        {
            return this.productRepository.AddToCart(cartItem, id);
        }
    }
}