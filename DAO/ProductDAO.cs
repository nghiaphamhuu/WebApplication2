using Newtonsoft.Json;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
    public class ProductDAO
    {
        private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\Product.txt";
        public static List<Product> getAllProduct()
        {
            List<Product> products = new List<Product>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                Product product = JsonConvert.DeserializeObject<Product>(line);
                products.Add(product);
            }

            reader.Close();

            return products;
        }

        public static void addProduct(Product product)
        {
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            int count = 1;
            while ((line = reader.ReadLine()) != null)
            {
                count++;
            }

            reader.Close();

            product.id = count;
            string json = JsonConvert.SerializeObject(product);

            StreamWriter writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public static Product getProductDetail(int id)
        {
            List<Product> products = getAllProduct();
            return products.FirstOrDefault(x => x.id == id);
        }

        public static List<Product> getProductBySearchName(string keySearch)
        {
            List<Product> products = getAllProduct();
            List<Product> result = new List<Product>();

            foreach(Product item in products)
            {
                if(item.name.Contains(keySearch))
                {
                    result.Add(item);
                }
            }

            return result;

        }

        public static void updateProduct(Product product)
        {
            List<Product> products = getAllProduct();
            StreamWriter writer = new StreamWriter(filePath);

            foreach (Product item in products)
            {
                string json = JsonConvert.SerializeObject(item);

                if (product.id == item.id)
                {
                    json = JsonConvert.SerializeObject(product);
                }
               
                writer.WriteLine(json);
            }

            writer.Close();
        }

        public static void deleteProduct(int id)
        {
            List<Product> products = getAllProduct();

            StreamWriter writer = new StreamWriter(filePath);
            int idx = 0;

            foreach(Product product in products)
            {
                if (product.id == id)
                {
                    continue;
                }

                Product product2 = product;
                product2.id = idx;
                idx++;

                string json = JsonConvert.SerializeObject(product2);

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public static List<Product> getProductsByCategory(string category)
        {
            List<Product> list = getAllProduct();
            List<Product> result = new List<Product>();

            foreach(Product item in list)
            {
                if(item.type.Equals(category))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
