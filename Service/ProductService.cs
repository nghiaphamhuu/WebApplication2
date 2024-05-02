using Microsoft.AspNetCore.Http;
using System.Globalization;
using WebApplication2.DAO;
using WebApplication2.Entity;
namespace WebApplication2.Service

{
    public class ProductService
    {
        public static List<Product> getAllProduct()
        {
            return ProductDAO.getAllProduct();
        }

        public static List<Product> getProductBySearchName(string keySearch)
        {
            return ProductDAO.getProductBySearchName(keySearch);
        }

        public static void addProduct(Product product)
        {
            ProductDAO.addProduct(product);
            return ;
        }

        public static Product getProductDetail(int id)
        {
            return ProductDAO.getProductDetail(id);
        }

        public static void updateProduct(Product product)
        {
            ProductDAO.updateProduct(product);
        }

        public static void deleteProduct(int id)
        {
            ProductDAO.deleteProduct(id);
        }

        public static List<Product> getProductsByCategory(string category)
        {
            if(string.IsNullOrEmpty(category))
            {
                return getAllProduct();
            }

            return ProductDAO.getProductsByCategory(category);
        }

        public static List<Product> getProductOutOfDate()
        {
            List<Product> products = getAllProduct();
            List<Product> result = new List<Product>();
            DateTime now = DateTime.Now;

            foreach(Product product in products)
            {
                
                if (DateTime.TryParseExact(product.date, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime err))
                {
                    DateTime date = DateTime.ParseExact(product.date, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                    if (now > date)
                    {
                        result.Add(product);
                    }
                }
            }

            return result;
        }
    }
}
