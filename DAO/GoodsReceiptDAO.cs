using Newtonsoft.Json;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
    public class GoodsReceiptDAO
    {
        private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\GoodsReceipt.txt";
        public static List<GoodsReceipt> getAllGoodsReceipt()
        {
            List<GoodsReceipt> goodsReceipts = new List<GoodsReceipt>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                GoodsReceipt goodsReceipt = JsonConvert.DeserializeObject<GoodsReceipt>(line);
                goodsReceipts.Add(goodsReceipt);
            }

            reader.Close();

            return goodsReceipts;
        }

        public static void addGoodsReceipt(GoodsReceipt goodsReceipt)
        {
            string json = JsonConvert.SerializeObject(goodsReceipt);

            StreamWriter writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public static GoodsReceipt getGoodsReceiptDetail(string goodsReceiptCode)
        {
            List<GoodsReceipt> goodsReceipts = getAllGoodsReceipt();
            return goodsReceipts.FirstOrDefault(x => x.goodsReceiptCode.Equals(goodsReceiptCode));
        }

        public static List<GoodsReceipt> getGoodsReceiptBySearchName(string keySearch)
        {
            List<GoodsReceipt> goodsReceipts = getAllGoodsReceipt();
            List<GoodsReceipt> result = new List<GoodsReceipt>();

            foreach (GoodsReceipt item in goodsReceipts)
            {
                if (item.goodsReceiptCode.Contains(keySearch))
                {
                    result.Add(item);
                }
            }

            return result;

        }

        public static void updateGoodsReceipt(GoodsReceipt goodsReceipt)
        {
            List<GoodsReceipt> goodsReceipts = getAllGoodsReceipt();
            StreamWriter writer = new StreamWriter(filePath);

            foreach (GoodsReceipt item in goodsReceipts)
            {
                string json = JsonConvert.SerializeObject(item);

                if (goodsReceipt.goodsReceiptCode.Equals(item.goodsReceiptCode))
                {
                    json = JsonConvert.SerializeObject(goodsReceipt);
                }

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public static void deleteGoodsReceipt(string goodsReceiptCode)
        {
            List<GoodsReceipt> goodsReceipts = getAllGoodsReceipt();

            StreamWriter writer = new StreamWriter(filePath);

            foreach (GoodsReceipt goodsReceipt in goodsReceipts)
            {
                if (goodsReceipt.goodsReceiptCode.Equals(goodsReceiptCode))
                {
                    continue;
                }

                string json = JsonConvert.SerializeObject(goodsReceipt);

                writer.WriteLine(json);
            }

            writer.Close();
        }
    }
}
