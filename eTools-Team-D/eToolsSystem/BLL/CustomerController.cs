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
    public class CustomerController
    {
        public List<Customer> Customer_Get_By_PhoneNumber(string ContactPhone)
        {
            if (ContactPhone != null)
            {
                if(ContactPhone.Length == 10)
                {
                    ContactPhone = ContactPhone.Substring(0, 3) + "."
                                + ContactPhone.Substring(3, 3) + "."
                                + ContactPhone.Substring(6, 4);
                }
            }

            using (var context = new eToolsContext())
            {
                return context.Customers
                    .Where(x => x.ContactPhone.Equals(ContactPhone))
                    .OrderBy(x => x.LastName)
                    .ToList();
            }
        }

        public Customer Customer_Get_By_Customerid(int customerid)
        {
            using (var context = new eToolsContext())
            {
                return context.Customers.Find(customerid);
            }
        }
    }
}
