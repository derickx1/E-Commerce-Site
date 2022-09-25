using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Model
{
    /*
   * Object representation of a  Item Transaction.
   * 
   * @author Joseph Boye
   * @version 8-10-2022
   */
    public class Item
    {
        // The ID of the order
        public int OrderId { get; set; }
        
        // The date that the order was made
        public DateTime order_date { get; set; }

        // Product/transaction ID
        public int id { get; set; }
        
        // Name of the product
        public string name { get; set; }

        // The pricing of the product
        public double price { get; set; }

        public Item() { }

        public Item(int orderId, DateTime order_date, int id, string name, double price)
        {
            this.OrderId = orderId;
            this.order_date = order_date;
            this.id = id;
            this.name = name;
            this.price = price;
        }
    }
}
