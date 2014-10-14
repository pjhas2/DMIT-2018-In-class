using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Entities.DTOs
{
    public class Order
    {
        public int TableNumber { get; set; }
        public string Waiter { get; set; }
        public int WaiterID { get; set; }
        public int? BillID { get; set; }
        public bool Served { get; set; }
        public string OrderComments { get; set; }
        
        public decimal TotalAmount
        {
            get
            {
                decimal value = 0; //default value
                if (Items != null)
                    value = Items.Sum(x => x.ItemTotal); //See footnote #1

                return value;
            }
        }

        public IList<OrderItem> Items { get; set; }
    }
}
