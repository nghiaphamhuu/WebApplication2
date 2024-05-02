using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
    public class CategoryService
    {
        public static List<Category> getAllCategory()
        {
            return CategoryDAO.getAllCategory();
        }

        public static List<Category> getCategoryBySearchName(string keySearch)
        {
            return CategoryDAO.getCategoryBySearchName(keySearch);
        }

        public static void addCategory(Category category)
        {
            CategoryDAO.addCategory(category);
            return;
        }

        public static Category getCategoryDetail(string typeCd)
        {
            return CategoryDAO.getCategoryDetail(typeCd);
        }

        public static void updateCategory(Category category)
        {
            CategoryDAO.updateCategory(category);
        }

        public static void deleteCategory(string typeCd)
        {
            CategoryDAO.deleteCategory(typeCd);
        }
    }
}
