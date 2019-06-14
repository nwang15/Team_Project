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
    public class RentalEquipmentController
    {
        public List<RentalEquipment> Get_AvailableEquipmentList()
        {
            using (var context = new eToolsContext())
            {
                var result = (from x in context.RentalEquipments
                              where x.Available == true
                              select x).ToList();
                return result;
            }
        }

        public RentalEquipment Equipment_Find_byID(int equipmentid)
        {
            using (var context = new eToolsContext())
            {
                var result = (from x in context.RentalEquipments
                              where x.RentalEquipmentID == equipmentid
                              select x).FirstOrDefault();
                return result;
            }
        }
    }
}
