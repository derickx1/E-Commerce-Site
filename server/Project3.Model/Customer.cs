namespace Project3.Model
{
    /*
     * Object representation of a Customer that uses our site.
     * 
     * @author Derick Xie, Ellery De Jesus
     * @version 8-05-2022
     */


    public class Customer
    {
        // The customer's ID
        public int id { get; set; }
        // The customer's name
        public string name { get; set; }
        // The customer's shipping address
        public string shipping_address { get; set; }

        public Customer() { }

        public Customer(int id, string name, string shipping_address)
        {
            this.id = id;
            this.name = name;
            this.shipping_address = shipping_address;
        }
    }
}