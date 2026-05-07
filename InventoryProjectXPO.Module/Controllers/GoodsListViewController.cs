using DevExpress.ExpressApp;
using System.Diagnostics;
using InventoryProjectXPO.Module.BusinessObjects.Master;
using DevExpress.Data.Filtering;
using System;
using DevExpress.ExpressApp.SystemModule;

namespace InventoryProjectXPO.Module.Controllers
{
    public class GoodsListViewController : ViewController<ListView>
        //: ViewController<ListView>
    {
        public GoodsListViewController()
        {
            TargetObjectType = typeof(Goods);
            TargetViewType = ViewType.ListView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            Debug.WriteLine("Goods List View Controller Activated");
            //View.ObjectSpace.ObjectDeleting += ObjectSpace_ObjectDeleting;
            View.ObjectSpace.CustomDeleteObjects += ObjectSpace_CustomDeleteObjects;
            DeleteObjectsViewController delCont = Frame.GetController<DeleteObjectsViewController>();
            if (delCont != null)
            {
                delCont.Deleting += DelCont_Deleting;
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            View.ObjectSpace.ObjectDeleting -= ObjectSpace_ObjectDeleting;
        }

        private void DelCont_Deleting(object sender, DeletingEventArgs e)
        {
            Debug.WriteLine("Deleting from Goods LV Controller's DeleteObjectsViewController's DelCont_Deleting");
             var eClassName = e.Objects.GetType().Name;
            if(eClassName == "Goods")
            {
                foreach (var obj in e.Objects)
                {
                    Goods eGoods = (Goods)obj;
                    Inventory foundInventory = View.ObjectSpace.FindObject<Inventory>(new BinaryOperator("GoodFk", eGoods));
                    foundInventory?.Delete();
                    //foundInventory?.Session.CommitTransaction();
                }
                View.ObjectSpace.CommitChanges();
            }
        }

        private void ObjectSpace_CustomDeleteObjects(object sender, CustomDeleteObjectsEventArgs e)
        {
            Debug.WriteLine("Deleting from Goods LV Controller's Custom Delete Objects");
            var eClassName = e.Objects.GetType().Name;
            if(eClassName == "Goods")
            {
                foreach (var obj in e.Objects)
                {
                    Goods eGoods = (Goods)obj;
                    Inventory foundInventory = View.ObjectSpace.FindObject<Inventory>(new BinaryOperator("GoodFk", eGoods));
                    foundInventory?.Delete();
                    //foundInventory?.Session.CommitTransaction();
                }
                View.ObjectSpace.CommitChanges();
            }
        }

        

        private void ObjectSpace_ObjectDeleting(object sender, ObjectsManipulatingEventArgs e)
        {
            Debug.WriteLine("Deleting from GoodsListViewController");
            var eClassName = e.Objects.GetType().Name;
            if(eClassName == "Goods")
            {
                foreach (var obj in e.Objects)
                {
                    Goods eGoods = (Goods)obj;
                    Inventory foundInventory = View.ObjectSpace.FindObject<Inventory>(new BinaryOperator("GoodFk", eGoods));
                    foundInventory?.Delete();
                    //foundInventory?.Session.CommitTransaction();
                }
                View.ObjectSpace.CommitChanges();
            }

        }
    }
}
