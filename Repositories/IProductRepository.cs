using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Model;

namespace Tap2PayAdmin.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int productId);

        int GetTotalProducts();

        int GetAvailableProducts();

        int GetMealsCount();

        int GetDrinksCount();
    }
}