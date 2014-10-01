using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; //Needed for the Lambda version of the Include() method
using eRestaurant.Entities;
using eRestaurant.DAL;

namespace eRestaurant.BLL
{
    [DataObject]
    public class MenuController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Item> ListMenuItems()
        {
            using(var context = new RestaurantContext())
            {
                //Note: To use the Lambda or method style of Include, you need to use System.Data.Entity
                //Get the item data and include the Category data for each item
                //the .Include() method on the DbSet<T> class performs "eager or force loading" of data
                return context.Items.Include(it => it.Category).ToList();

            }

        }
    }
}
