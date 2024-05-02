using Newtonsoft.Json;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
    public class CategoryDAO
    {
        private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\Category.txt";
        public static List<Category> getAllCategory()
        {
            List<Category> categorys = new List<Category>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                Category category = JsonConvert.DeserializeObject<Category>(line);
                categorys.Add(category);
            }

            reader.Close();

            return categorys;
        }

        public static void addCategory(Category category)
        {
            StreamReader reader = new StreamReader(filePath);

            reader.Close();

            string json = JsonConvert.SerializeObject(category);

            StreamWriter writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public static Category getCategoryDetail(string typeCd)
        {
            List<Category> categorys = getAllCategory();
            return categorys.FirstOrDefault(x => x.typeCd.Equals(typeCd));
        }

        public static List<Category> getCategoryBySearchName(string keySearch)
        {
            List<Category> categorys = getAllCategory();
            List<Category> result = new List<Category>();

            foreach (Category item in categorys)
            {
                if (item.typeDesc.Contains(keySearch))
                {
                    result.Add(item);
                }
            }

            return result;

        }

        public static void updateCategory(Category category)
        {
            List<Category> categorys = getAllCategory();
            StreamWriter writer = new StreamWriter(filePath);

            foreach (Category item in categorys)
            {
                string json = JsonConvert.SerializeObject(item);

                if (category.typeCd.Equals(item.typeCd))
                {
                    json = JsonConvert.SerializeObject(category);
                }

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public static void deleteCategory(string typeCd)
        {
            List<Category> categorys = getAllCategory();

            StreamWriter writer = new StreamWriter(filePath);

            foreach (Category category in categorys)
            {
                if (category.typeCd.Equals(typeCd))
                {
                    continue;
                }

                string json = JsonConvert.SerializeObject(category);

                writer.WriteLine(json);
            }

            writer.Close();
        }
    }
}
