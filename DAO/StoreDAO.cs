using Newtonsoft.Json;
using WebApplication2.Entity;
using static System.Formats.Asn1.AsnWriter;

namespace WebApplication2.DAO
{
	public class StoreDAO
	{
        private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\Store.txt";
        public static List<Store> getAllStore()
        {
            List<Store> stores = new List<Store>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                Store store = JsonConvert.DeserializeObject<Store>(line);
                stores.Add(store);
            }

            reader.Close();

            return stores;
        }

        public static void addStoreByGoodsReceipt(List<Store> stores)
        {
            List<Store> list = getAllStore();
            List<Store> addList = new List<Store>();

            //Update data in store with productId Has already in store
            foreach(Store store in list)
            {
                Store item = store;
                for ( int i = 0; i< stores.Count; i++)
                {
                    Store item2  = stores[i];
                    if (item2.productId == item.productId)
                    {
                        item.quantity = item2.quantity + item.quantity;
                        item.total = item2.total +item.total;
                        break;
                    }
                }
                addList.Add(item);
            }

            //Add data to store with new product id
            foreach(Store item in stores)
            {
                bool chk = true;
                foreach(Store item2 in list)
                {
                    if(item2.productId == item.productId)
                    {
                        chk = false;
                        break;
                    }
                }

                if(chk)
                {
                    addList.Add(item);
                }

            }

            StreamWriter writer = new StreamWriter(filePath);

            foreach (Store addItem in addList)
            {
                string json = JsonConvert.SerializeObject(addItem);

                writer.WriteLine(json);
            }

       
            writer.Close();
        }

        public static void deleteStoreByGoodsReceipt(List<Store> mergeMap)
        {
            List<Store> list = getAllStore();

            List<Store> deleteList = new List<Store>();

            foreach(Store item in list)
            {
                Store itemDelete = item;
                foreach(Store item2 in mergeMap)
                {
                    if(item2.productId == item.productId)
                    {
                        itemDelete.quantity = itemDelete.quantity - item2.quantity;
                        itemDelete.total = itemDelete.total - item2.total;
                        break;
                    }
                }

                if(itemDelete.quantity > 0)
                {
                    deleteList.Add(itemDelete);
                }
            }

            StreamWriter writer = new StreamWriter(filePath);

            foreach (Store deleteItem in deleteList)
            {
                string json = JsonConvert.SerializeObject(deleteItem);

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public static Store getStoreDetail(int productId)
        {
            Store result = new Store();

            List<Store> list = getAllStore();

            foreach(Store item in list)
            {
                if(productId == item.productId)
                {
                    return item;
                }
            }

            return result;
        }

		public static void addStoreByInvoice(List<Store> stores)
		{
            List<Store> list = getAllStore();
            List<Store> addList = new List<Store>();

            //Update data in store with productId Has already in store
            foreach (Store store in list)
            {
                Store item = store;
                for (int i = 0; i < stores.Count; i++)
                {
                    Store item2 = stores[i];
                    if (item2.productId == item.productId)
                    {
                        item.quantity =  item.quantity - item2.quantity ;
                        item.total = item.total - item2.total;
                        break;
                    }
                }

                if(item.quantity > 0)
                {
                    addList.Add(item);
                }
                
            }

            StreamWriter writer = new StreamWriter(filePath);

            foreach (Store addItem in addList)
            {
                string json = JsonConvert.SerializeObject(addItem);

                writer.WriteLine(json);
            }


            writer.Close();
        }

		public static void deleteStoreByInvoice(List<Store> mergeMap)
		{
            List<Store> list = getAllStore();

            List<Store> deleteList = new List<Store>();

            foreach (Store item in list)
            {
                Store itemDelete = item;
                foreach (Store item2 in mergeMap)
                {
                    if (item2.productId == item.productId)
                    {
                        itemDelete.quantity = itemDelete.quantity + item2.quantity;
                        itemDelete.total = itemDelete.total + item2.total;
                        break;
                    }
                }

                if (itemDelete.quantity > 0)
                {
                    deleteList.Add(itemDelete);
                }
            }

            StreamWriter writer = new StreamWriter(filePath);

            foreach (Store deleteItem in deleteList)
            {
                string json = JsonConvert.SerializeObject(deleteItem);

                writer.WriteLine(json);
            }

            writer.Close();
        }
	}
}
