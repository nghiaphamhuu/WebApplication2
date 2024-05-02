using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
	public class StoreService
	{

		public static Store getStoreDetail(int productId)
		{
			return StoreDAO.getStoreDetail(productId);
		}

		public static List<Store> getAllStore()
		{
			return StoreDAO.getAllStore();
		}

		public static List<Store> getStoresByCategory(string category)
		{
			List<Store> result = new List<Store>();
			List<Store> list = getAllStore();

			foreach (Store store in list)
			{
				Store item = store;

				if(item.typeCd.Equals(category))
				{
					result.Add(item);
				}
			}

			return result;
		}

    }
}
