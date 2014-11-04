using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Entities
{
    public class Table
    {
        //By convention, if we use a property with the same name as the class, but
        //with a suffix of ID, then entity Framework will recognize that property
        //as mapping of the database table's Primary Key column
        public int TableID { get; set; }
        public byte TableNumber { get; set; }
        public bool Smoking { get; set; }
        public int Capacity {get;set;}
        public bool Available { get; set; }

        #region Navigation Properties
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        #endregion
    }
}
