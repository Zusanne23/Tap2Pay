using System.Collections.Generic;
using Tap2PayAdmin.Model;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Repositories;

namespace Tap2PayAdmin.Services
{
    public class ProductService
    {
        private readonly IProductRepository repository;

        public ProductService()
        {
            repository = new ProductRepository();
        }

        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        public void AddProduct(Product product)
        {
            repository.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            repository.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            repository.DeleteProduct(id);
        }

        public int GetTotalProducts()
        {
            return repository.GetTotalProducts();
        }

        public int GetAvailableProducts()
        {
            return repository.GetAvailableProducts();
        }

        public int GetMealsCount()
        {
            return repository.GetMealsCount();
        }

        public int GetDrinksCount()
        {
            return repository.GetDrinksCount();
        }
    }
}