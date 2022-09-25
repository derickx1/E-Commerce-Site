using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Model
{

    /*
    * Object representation of a Transaction.
    * 
    * @author Joseph Boye
    * @version 8-10-2022
    */
    public class Jewelry_transaction
    {
        // transaction ID
        public int id { get; set; }

        // Customer ID
        public int customer_id { get; set; }

        // Order ID
        public int order_id { get; set; }

        //Item ID
        public int Item_id { get; set; }

        public Jewelry_transaction() { }

        public Jewelry_transaction(int id, int customer_id, int order_id, int item_id)
        {
            this.id = id;
            this.customer_id = customer_id;
            this.order_id = order_id;
            this.Item_id = item_id;
        }

        public static void Add(Jewelry_transaction newJewelryTransaction)
        {
            throw new NotImplementedException();
        }
    }
}
