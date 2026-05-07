using DevExpress.ExpressApp.SystemModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProjectXPO.Module.Controllers
{
    internal class GoodsDeleteController: DeleteObjectsViewController
    {
        public GoodsDeleteController()
            {
                TargetObjectType = typeof(BusinessObjects.Master.Goods);
                //TargetViewType = ViewType.ListView;
        }
    }
}
