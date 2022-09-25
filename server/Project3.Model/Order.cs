using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Model
{
    /*
     * Object representation of a order made by a customer.
     * 
     * @author Derick Xie, Ellery De Jesus
     * @version 8-05-2022
     */
    public class Order
    {
        // The ID of the order
        public int id { get; set; }
        // The ID of the customer who made the order
        public int customer_id { get; set; }
       
        // The date that the order was made
        public DateTime order_date { get; set; }

        public Order() { }

        public Order(int id, int customer_id, DateTime order_date)
        {
            this.id = id;
            this.customer_id = customer_id;
            this.order_date = order_date;
        }
    }
}
