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
    public class RentalDetailController
    {
        public List<RentalDetailRecord> List_RentalDetail_forRental(int customerid, int employeeid, DateTime rentaldate)
        {
            using (var context = new eToolsContext())
            {
                var results = (from x in context.Rentals
                               where x.CustomerID.Equals(customerid) && x.EmployeeID.Equals(employeeid) && x.RentalDate == rentaldate
                               select x).FirstOrDefault();
                if (results == null)
                {
                    return null;
                }
                else
                {
                    var details = from x in context.RentalDetails
                                  where x.RentalID.Equals(results.RentalID)
                                  select new RentalDetailRecord
                                  {
                                      RentalEquipmentID = x.RentalEquipmentID,
                                      Days = x.Days,
                                      DailyRate = x.DailyRate,
                                      ConditionOut = x.ConditionOut,
                                      Paid = x.Paid
                                  };
                    return details.ToList();
                }
            }
        }

        public void Add_RentalDetail_toRental(Rental record, int rentalequipmentid)
        {
            List<string> reasons = new List<string>();
            using (var context = new eToolsContext())
            {
                Rental exists = context.Rentals
                    .Where(x => x.CustomerID.Equals(record.CustomerID) && x.EmployeeID.Equals(record.EmployeeID) && x.RentalDate == record.RentalDate)
                    .Select(x => x).FirstOrDefault();
                RentalDetail newRentalDetailRecord = null;

                newRentalDetailRecord.RentalEquipmentID = rentalequipmentid;


                exists.RentalDetails.Add(newRentalDetailRecord);
                context.SaveChanges();

            }
        }
    }
}
