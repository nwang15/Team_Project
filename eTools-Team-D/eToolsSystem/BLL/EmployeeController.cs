using eTools.Data.Entities;
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
    public class EmployeeController
    {
        public Employee Employee_Get(int employeeid)
        {
            using (var context = new eToolsContext())
            {
                return context.Employees.Find(employeeid);
            }
        }
    }
}
