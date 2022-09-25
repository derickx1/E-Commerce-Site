using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Model
{
    /*
     * Object representation of a Review on our site.
     * 
     * @author Derick Xie, Ellery De Jesus
     * @version 8-05-2022
     */
    public class Review
    {
        // Id of the review
        public int id { get; set; }
        // Id of the customer who wrote the review
        public int customer_id { get; set; }
        // Id of the item purchased
        public int jewelry_id { get; set; }
        // The date of the review. 
        public DateTime review_date { get; set; }
        // The review itself, should the customer write one
        public string? content { get; set; }
        // The rating of the item purchased made by the customer, should they rate their purchase
        public byte? rating { get; set; }


        public Review() { }

        public Review(int id, int jewelry_id, int customer_id, DateTime review_date, string? content, byte? rating)
        {
            this.id = id;
            this.jewelry_id = jewelry_id;
            this.customer_id = customer_id;
            this.review_date = review_date;
            this.content = content;
            this.rating = rating;
        }
    }
}
