using Newtonsoft.Json;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
	public class InvoiceDAO
	{
        private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\Invoice.txt";
        public static List<Invoice> getAllInvoice()
        {
            List<Invoice> invoices = new List<Invoice>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                Invoice invoice = JsonConvert.DeserializeObject<Invoice>(line);
                invoices.Add(invoice);
            }

            reader.Close();

            return invoices;
        }

        public static void addInvoice(Invoice invoice)
        {
            StreamReader reader = new StreamReader(filePath);

            reader.Close();

            string json = JsonConvert.SerializeObject(invoice);

            StreamWriter writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public static Invoice getInvoiceDetail(string invoiceCode)
        {
            List<Invoice> invoices = getAllInvoice();
            return invoices.FirstOrDefault(x => x.invoiceCode.Equals(invoiceCode));
        }

        public static List<Invoice> getInvoiceBySearchName(string keySearch)
        {
            List<Invoice> invoices = getAllInvoice();
            List<Invoice> result = new List<Invoice>();

            foreach (Invoice item in invoices)
            {
                if (item.invoiceCode.Contains(keySearch))
                {
                    result.Add(item);
                }
            }

            return result;

        }

        public static void updateInvoice(Invoice invoice)
        {
            List<Invoice> invoices = getAllInvoice();
            StreamWriter writer = new StreamWriter(filePath);

            foreach (Invoice item in invoices)
            {
                string json = JsonConvert.SerializeObject(item);

                if (invoice.invoiceCode.Equals(item.invoiceCode))
                {
                    json = JsonConvert.SerializeObject(invoice);
                }

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public static void deleteInvoice(string invoiceCode)
        {
            List<Invoice> invoices = getAllInvoice();

            StreamWriter writer = new StreamWriter(filePath);

            foreach (Invoice invoice in invoices)
            {
                if (invoice.invoiceCode.Equals(invoiceCode))
                {
                    continue;
                }

                string json = JsonConvert.SerializeObject(invoice);

                writer.WriteLine(json);
            }

            writer.Close();
        }
    }
}
