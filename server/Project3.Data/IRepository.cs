using Project3.Model;

namespace Project3.Data
{
    /*
     * Interface for classes who's job is to read from a database and output data needed for the client side.
     * 
     * @author Ellery R. De Jesus, Derick Xie
     * @version 8-6-2022
     */
    public interface IRepository
    {
        // Returns a list of Jewelry on the store
        public Task<List<Jewelry>> ListJewelry(int startrow, int endrow);
        // Returns a list of Jewelry based on a filter
        public Task<List<Jewelry>> ListFilteredJewelry(string material, string item_type);
        // Returns data pertaining to one of the jewelries
        public Task<Jewelry> GetJewelry(int ItemID);
        // Returns a list of reviews for one of the jewelries
        public Task<List<Review>> GetProductReviews(int ItemID);
        // Returns a list of reviews that has been made by one of the customers
        public Task<List<Review>> GetCustomerReviews(int CustomerID);
        // Creates a review to be added to the database
        public Task AddReview(Review review);
        // Returns the profile information for a customer
        public Task<Customer> GetCustomer(int CustomerID);
        // Modifies a field within the customer's profile (For now just Shipping Address)
        public Task ModifyCustomerProfile(int CustomerID, string field, string value);
        // Creates a customer to be added to the database
        public Task AddCustomer(string CName, string Shipping_address, string username, string password);
        // Creates a order containing the jewelry to be bought, the buyer's id, and the date in which the order was made
        public Task MakePurchase(int CustomerID);
        // Lists the purchases that a customer has made
        public Task<List<Order>> ListOrders(int CustomerID);
        // Retrieve the Customer with the specified Username
        public Task<Customer> LogInCustomer(string UserName, string Password);

        public Task<Customer> RegisterCustomer(string CName, string ShippingAddress, string Username, string Password);
        // Add a transaction with the buyer ID the order ID and the Item ID 
        public Task AddTransaction(int CustomerID, int OrderID, int ItemID);
        // List all transactions
        public Task<List<Jewelry_transaction>>ListTransactions();
        //Returns the Transaction information for a customer
        public Task<List<Item>> ListCustomerTransaction(int CustomerID);

    }
}
