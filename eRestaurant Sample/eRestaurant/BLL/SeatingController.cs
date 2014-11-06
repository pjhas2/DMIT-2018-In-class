﻿using eRestaurant.DAL;
using eRestaurant.Entities;
using eRestaurant.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.BLL
{
    [DataObject]
    public class SeatingController
    {
        #region Query Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SeatingSummary> SeatingByDateTime(DateTime date, TimeSpan time)
        {
            using (var context = new RestaurantContext())
            {
                // code from my LinqPad explorations
                //Step 1 - Get the table info along with any walk-in bills and reservation bills for the specific time slot
                var step1 = from data in context.Tables
                            select new
                            {
                                Table = data.TableNumber,
                                Seating = data.Capacity,
                                // This sub-query gets the bills for walk-in customers
                                Bills = from billing in data.Bills
                                        where
                                              billing.BillDate.Year == date.Year
                                           && billing.BillDate.Month == date.Month
                                           && billing.BillDate.Day == date.Day
                                           //The following won't work in EF to Entities - it will return this exception.
                                           //"The specified type member 'TimeOfDay' is not supported..."
                                           //&& billing.BillDate.TimeOfDay <= time
                                           && DbFunctions.CreateTime(billing.BillDate.Hour,billing.BillDate.Minute,billing.BillDate.Second) <= time
                                           && (!billing.OrderPaid.HasValue || billing.OrderPaid >= time)
                                        //						   && (!billing.PaidStatus || billing.OrderPaid >= time)
                                        select billing,
                                //This sub-query gets the bill for reservations
                                Reservations = from booking in data.Reservations //drill to the first collection
                                               from billing in booking.Bills //drill to the next collection
                                               where
                                                    billing.BillDate.Year == date.Year
                                                && billing.BillDate.Month == date.Month
                                                && billing.BillDate.Day == date.Day
                                                   //The following won't work in EF to Entities - it will return this exception.
                                                   //"The specified type member 'TimeOfDay' is not supported..."
                                                   //&& billing.BillDate.TimeOfDay <= time
                                           && DbFunctions.CreateTime(billing.BillDate.Hour, billing.BillDate.Minute, billing.BillDate.Second) <= time
                                           && (!billing.OrderPaid.HasValue || billing.OrderPaid >= time)
                                                && (!billing.OrderPaid.HasValue || billing.OrderPaid >= time)
                                               select billing
                            };

                //Step 2 - Union the walk-in bills and the reservation bills while extracting the relevant bill info
                //.ToList() helps to resolve the "Types in union of Concat are constructed incompatibility" error
                var step2 = from data in step1.ToList() //.ToList() forces the firrst result set to be in memory
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                CommonBilling = from info in data.Bills.Union(data.Reservations)
                                                select new //info //changed to get only needed info, not entire entity
                                                {
                                                    BillID = info.BillID,
                                                    BillTotal = info.BillItems.Sum(bi => bi.Quantity * bi.SalePrice),
                                                    Waiter = info.Waiter.FirstName,
                                                    Reservation = info.Reservation
                                                }
                            };

                //Step - 3 Get just the first CommonBilling item
                //         presumes no overlaps can occur - i.e., two groups at the same time at the same time)
                var step3 = from data in step2
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.CommonBilling.Count() > 0,
                                // .FirstOrDefault() is effectively "flattening" my collection of 1 item into a 
                                // single object whose properties 1 can get in step 4 by using the dot(.) operator
                                CommonBilling = data.CommonBilling.FirstOrDefault()
                            };

                //Step 4 - Build our intended seating summary info
                var step4 = from data in step3
                            select new SeatingSummary() // my DTO
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.Taken,
                                //use a ternary expression to conditionally get the bill id (if it exists)
                                BillID = data.Taken ?			// if(data.Taken) ? means if and : means else
                                         data.CommonBilling.BillID // value to use if true
                                       : (int?)null, 		 // value to use if false
                                BillTotal = data.Taken ?
                                            data.CommonBilling.BillTotal : (decimal?)null,
                                Waiter = data.Taken ?
                                         data.CommonBilling.Waiter : (string)null,
                                ReservationName = data.Taken ?
                                                  (data.CommonBilling.Reservation != null ?
                                                   data.CommonBilling.Reservation.CustomerName : (string)null)
                                                   : (string)null
                            };
                return step4.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ReservationCollection> ReservationByTime(DateTime date)
        {
            using (var context = new RestaurantContext())
            {
                //Copy and paste our code from LinqPad
                var result = from data in context.Reservations
                             where data.ReservationDate.Year == date.Year
                                && data.ReservationDate.Month == date.Month
                                && data.ReservationDate.Day == date.Day
                                && data.ReservationStatus == Reservation.Booked //Reservation.Booked
                             select new ReservationSummary()//DTOs.ReservationSummary()
                             {
                                 Name = data.CustomerName,
                                 Date = data.ReservationDate,
                                 NumberInParty = data.NumberInParty,
                                 Status = data.ReservationStatus,
                                 Event = data.Event.Description,
                                 Contact = data.ContactPhone,
                                 Tables = from seat in data.Tables
                                          select seat.TableNumber

                             };
                var finalResult = from item in result
                                  orderby item.NumberInParty
                                  group item by item.Date.Hour into itemGroup
                                  select new ReservationCollection()
                                  {
                                      Time = itemGroup.Key,
                                      Reservations = itemGroup.ToList()
                                  };
                return finalResult.OrderBy(x => x.Time).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SeatingSummary> AvailableSeatingByDateTime(DateTime date, TimeSpan time)
        {
            var result = from seats in SeatingByDateTime(date, time)
                         where !seats.Taken
                         select seats;
            return result.ToList();
        }
        #endregion

        
        /// <summary>
        /// Seats a customer that is a walk-in
        /// </summary>
        /// <param name="when">A mock value of the date/time (Temporary - see remarks)</param>
        /// <param name="tableNumber">Table number to be seated</param>
        /// <param name="customerCount">Number of customers being seated</param>
        /// <param name="waiterId">Id of waiter that is serving</param>
        public void SeatCustomer(DateTime when, byte tableNumber, int customerCount, int waiterId)
        {
            var availableSeats = AvailableSeatingByDateTime(when.Date, when.TimeOfDay);
            using (var context = new RestaurantContext())
            {
                List<string> errors = new List<string>();
                // Rule checking:
                // - Table must be available - typically a direct check on the table, but proxied based on the mocked time here
                // - Table must be big enough for the # of customers
                if (!availableSeats.Exists(x => x.Table == tableNumber))
                    errors.Add("Table is currently not available");
                else if (!availableSeats.Exists(x => x.Table == tableNumber && x.Seating >= customerCount))
                    errors.Add("Insufficient seating capacity for number of customers.");
                //TODO: Check for these other possible errors
                //      -- negative number of customers :(
                //      -- waiter should exist as an active waiter
                if (errors.Count > 0)
                    throw new BusinessRuleException("Unable to seat customer", errors);
                Bill seatedCustomer = new Bill()
                {
                    BillDate = when,
                    NumberInParty = customerCount,
                    WaiterID = waiterId,
                    TableID = context.Tables.Single(x => x.TableNumber == tableNumber).TableID
                };
                context.Bills.Add(seatedCustomer);
                context.SaveChanges();
            }
        }
    }
}
