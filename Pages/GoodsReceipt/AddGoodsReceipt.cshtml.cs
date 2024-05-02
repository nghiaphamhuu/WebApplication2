using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.GoodsReceipt
{
    public class AddGoodsReceiptModel : PageModel
    {
        public void OnGet()
        {

        }

        public void addStore(int id, int quanity)
        {
            List<Store> stores = HttpContext.Session.GetObject<List<Store>>("storesGoodsReceipt"); ;
            if (stores == null)
            {
                stores = new List<Store>();
            }
         
            Entity.Product product = ProductService.getProductDetail(id);
            Store store = new Store();
            store.productId = product.id;
            store.productName = product.name;
            store.typeCd = product.type;
            store.quantity = quanity;
            store.price = product.price;
            store.total = quanity*product.price;
            stores.Add(store);

            HttpContext.Session.SetObject<List<Store>>("storesGoodsReceipt", stores);
        }

        public void OnPost()
        {
        }
    }
}
