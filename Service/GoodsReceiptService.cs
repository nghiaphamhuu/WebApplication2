using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
    public class GoodsReceiptService
    {
        public static List<GoodsReceipt> getAllGoodsReceipt()
        {
            return GoodsReceiptDAO.getAllGoodsReceipt();
        }

        public static List<GoodsReceipt> getGoodsReceiptBySearchName(string keySearch)
        {
            return GoodsReceiptDAO.getGoodsReceiptBySearchName(keySearch);
        }

        public static void addGoodsReceipt(GoodsReceipt goodsReceipt, bool isAddGoodsReceipt)
        {
            List<Store> stores = goodsReceipt.stores;
            Dictionary<int, Store> map = new Dictionary<int, Store>();

            foreach(Store store in stores)
            {
                if (map.ContainsKey(store.productId))
                {
                    Store itemUpdate = store;
                    int quantity = map[store.productId].quantity;
                    int total = map[store.productId].total;

                    itemUpdate.quantity = quantity + itemUpdate.quantity;
                    itemUpdate.total = total+ itemUpdate.total;

                    map[store.productId] =  itemUpdate;
                }
                else
                {
                    map.Add(store.productId, store);
                }
            }

            List<Store> mergeMap = new List<Store>();

            foreach(int k in map.Keys)
            {
                mergeMap.Add(map[k]);
            }

            StoreDAO.addStoreByGoodsReceipt(mergeMap);

            if (isAddGoodsReceipt)
            {
                GoodsReceiptDAO.addGoodsReceipt(goodsReceipt);
            }
        }

        public static GoodsReceipt getGoodsReceiptDetail(string goodsReceiptCode)
        {
            return GoodsReceiptDAO.getGoodsReceiptDetail(goodsReceiptCode);
        }

        public static void updateGoodsReceipt(GoodsReceipt goodsReceipt)
        {
            deleteGoodsReceipt(goodsReceipt.goodsReceiptCode, false);
            addGoodsReceipt(goodsReceipt, false);

            GoodsReceiptDAO.updateGoodsReceipt(goodsReceipt);
        }

        public static void deleteGoodsReceipt(string goodsReceiptCode, bool isDeleteGoodsReceipt)
        {
            List<Store> stores = getGoodsReceiptDetail(goodsReceiptCode).stores;
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

            StoreDAO.deleteStoreByGoodsReceipt(mergeMap);

            if(isDeleteGoodsReceipt)
            {
                GoodsReceiptDAO.deleteGoodsReceipt(goodsReceiptCode);
            }
            
        }
    }
}
