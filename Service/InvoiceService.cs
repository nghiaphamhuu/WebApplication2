using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
	public class InvoiceService
	{
        public static List<Invoice> getAllInvoice()
        {
            return InvoiceDAO.getAllInvoice();
        }

        public static List<Invoice> getInvoiceBySearchName(string keySearch)
        {
            return InvoiceDAO.getInvoiceBySearchName(keySearch);
        }

        public static void addInvoice(Invoice invoice, bool isAddInvoie)
        {
            List<Store> stores = invoice.stores;
            Dictionary<int, Store> map = new Dictionary<int, Store>();

            foreach (Store store in stores)
            {
                if (map.ContainsKey(store.productId))
                {
                    Store itemUpdate = store;
                    int quantity = map[store.productId].quantity;
                    int total = map[store.productId].total;

                    itemUpdate.quantity = quantity + itemUpdate.quantity;
                    itemUpdate.total = total + itemUpdate.total;

                    map[store.productId] = itemUpdate;
                }
                else
                {
                    map.Add(store.productId, store);
                }
            }

            List<Store> mergeMap = new List<Store>();

            foreach (int k in map.Keys)
            {
                mergeMap.Add(map[k]);
            }

            StoreDAO.addStoreByInvoice(mergeMap);

            if (isAddInvoie)
            {
                InvoiceDAO.addInvoice(invoice);
            }
        }

        public static Invoice getInvoiceDetail(string invoiceCode)
        {
            return InvoiceDAO.getInvoiceDetail(invoiceCode);
        }

        public static void updateInvoice(Invoice invoice)
        {
            deleteInvoice(invoice.invoiceCode, false);
            addInvoice(invoice, false);
            InvoiceDAO.updateInvoice(invoice);
        }

        public static void deleteInvoice(string invoiceCode, bool isDeleteInvoice)
        {
            List<Store> stores = getInvoiceDetail(invoiceCode).stores;
            Dictionary<int, Store> map = new Dictionary<int, Store>();

            foreach (Store store in stores)
            {
                if (map.ContainsKey(store.productId))
                {
                    Store itemUpdate = store;
                    int quantity = map[store.productId].quantity;
                    int total = map[store.productId].total;

                    itemUpdate.quantity = quantity + itemUpdate.quantity;
                    itemUpdate.total = total + itemUpdate.total;

                    map[store.productId] = itemUpdate;
                }
                else
                {
                    map.Add(store.productId, store);
                }
            }

            List<Store> mergeMap = new List<Store>();

            foreach (int k in map.Keys)
            {
                mergeMap.Add(map[k]);
            }

            StoreDAO.deleteStoreByInvoice(mergeMap);

            if (isDeleteInvoice)
            {
                InvoiceDAO.deleteInvoice(invoiceCode);
            }
        }

        public static int checkValid(Invoice invoice)
        {
            List<Store> stores = StoreService.getAllStore();
            List<Store> newList = invoice.stores;
            List<Store> oldList = getInvoiceDetail(invoice.invoiceCode).stores;
            Dictionary<int, Store> map1 = new Dictionary<int, Store>();
            Dictionary<int, Store> map2 = new Dictionary<int, Store>();
            Dictionary<int, Store> map3 = new Dictionary<int, Store>();

            foreach (Store store in newList)
            {
                if(map1.ContainsKey(store.productId))
                {
                    Store item = store;
                    item.quantity = item.quantity + map1[store.productId].quantity;

                    map1[store.productId] = item;
                }
                else
                {
                    map1.Add(store.productId, store);
                }
            }

            foreach (Store store in oldList)
            {
                if (map1.ContainsKey(store.productId))
                {
                    Store item = store;
                    item.quantity = item.quantity + map2[store.productId].quantity;

                    map2[store.productId] = item;
                }
                else
                {
                    map2.Add(store.productId, store);
                }
            }

            foreach (Store store in stores)
            {
                map3.Add(store.productId, store);
            }

            for(int i = 0; i< newList.Count; i++)
            {
                Store store = newList[i];
                if (map1[store.productId].quantity - map2[store.productId].quantity > map3[store.productId].quantity)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
