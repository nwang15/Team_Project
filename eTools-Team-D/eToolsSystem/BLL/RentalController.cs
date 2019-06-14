using eTools.Data.DTOs;
using eTools.Data.Entities;
using eTools.Data.POCOs;
using eToolsSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eToolsSystem.BLL
{
    [DataObject]
    public class RentalController
    {
        public void Create_newRentalRecord(int customerid, int employeeid, RentalRecord record)
        {
            using (var context = new eToolsContext())
            {
                decimal subtotal = 0;
                decimal taxamount = 0;
                Rental newRentalRecord = null;
                newRentalRecord.CustomerID = customerid;
                newRentalRecord.EmployeeID = employeeid;
                newRentalRecord.CouponID = record.CouponID;

                foreach (var item in record.Details)
                {
                    subtotal += item.DailyRate * (decimal)item.Days;
                }
                newRentalRecord.SubTotal = subtotal;

                taxamount = (decimal)0.05 * subtotal;
                newRentalRecord.TaxAmount = taxamount;

                newRentalRecord.RentalDate = record.RentalDate;
                newRentalRecord.PaymentType = "C";
                newRentalRecord.CreditCard = record.CreditCard;

                if (newRentalRecord != null)
                {
                    context.Rentals.Add(newRentalRecord);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Rental record was created failed.");
                }
            }
        }

        public void Delete_RentalRecord(int customerid, int employeeid, RentalRecord rentalRecord)
        {
            using (var context = new eToolsContext())
            {
                var exists = (from x in context.Rentals
                              where x.CustomerID.Equals(customerid) && x.EmployeeID.Equals(employeeid) && x.RentalDate == rentalRecord.RentalDate
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Rental record has been removed.");
                }
                else
                {
                    context.Rentals.Remove(exists);
                    context.SaveChanges();

                }

            }
        }
    }
}
