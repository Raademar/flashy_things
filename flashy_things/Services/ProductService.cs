using System.Collections.Generic;
using flashy_things.Models;
using flashy_things.Repositories;

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
    }
}