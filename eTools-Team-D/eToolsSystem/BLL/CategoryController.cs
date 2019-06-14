using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel;
using eTools.Data.Entities;
using eToolsSystem.DAL;
#endregion

namespace eToolsSystem.BLL
{
    [DataObject]
    public class CategoryController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Category> Categories_List()
        {
            //create an transaction instance of your Context class
            using (var context = new eToolsContext())
            {
                return context.Categories.OrderBy(x => x.Description).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Category Categories_Get(int categoryid)
        {
            //create an transaction instance of your Context class
            using (var context = new eToolsContext())
            {
                return context.Categories.Find(categoryid);
            }
        }
    }
}
