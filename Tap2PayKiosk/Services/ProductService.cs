using System.Collections.Generic;
using Tap2PayKiosk.Model;
using Tap2PayKiosk.Models;
using Tap2PayKiosk.Repositories;
using System.Data.SqlClient;

namespace Tap2PayKiosk.Services
{
    public class ProductService
    {
        private readonly ProductRepository repository = new ProductRepository();

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