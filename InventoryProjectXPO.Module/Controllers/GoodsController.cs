using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DevExpress.ExpressApp.SystemModule;
using InventoryProjectXPO.Module.BusinessObjects.Master;
using DevExpress.Data.Filtering;

namespace InventoryProjectXPO.Module.Controllers
{
    public class GoodsController : ViewController
    {
        public GoodsController()
        {
            TargetObjectType = typeof(Goods);
            //TargetViewType = ViewType.ListView;
            TargetViewType = ViewType.DetailView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            //View.ObjectSpace.ObjectSaved += ObjectSpace_ObjectSaved;
            Debug.WriteLine("On Activated!");
            //ModificationsController modCont = Frame.GetController<ModificationsController>();
            //if (modCont != null)
            //{
            //    Debug.WriteLine(modCont);
            //    modCont.SaveAction.Execute += SaveAction_Execute;
            //}

            //View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            View.ObjectSpace.ObjectSaving += ObjectSpace_ObjectSaving;
        }

        //protected override void OnDeactivated()
        //{
        //    base.OnDeactivated();
        //    View.ObjectSpace.ObjectSaving -= ObjectSpace_ObjectSaving;
        //}

        private void ObjectSpace_ObjectSaving(object sender, ObjectManipulatingEventArgs e)
        {
            Debug.WriteLine("Object Saving... whilst also update invenotyr");
            Debug.WriteLine(sender);
            Debug.WriteLine(e);

            var inventoryExists = View.ObjectSpace.FindObject<Inventory>(new BinaryOperator("GoodFk", e.Object));
            //Debug.WriteLine(inventoryExists);
            Debug.WriteLine(e.Object);
            if (inventoryExists == null)
            {
                Debug.WriteLine("Inventory no exists");
                var obj = (Inventory)e.Object;
                obj.CurrentStock = 0;
                Debug.WriteLine("Obj");
                Debug.WriteLine(obj);
                //Inventory newInventory = View.ObjectSpace.CreateObject<Inventory>();
                //newInventory.GoodFk = (Goods)e.Object;
                //newInventory.CurrentStock = 0;
                //newInventory.Save();
            } else
            {
                Debug.WriteLine("Inventory does exists : ");
            }

        }

        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            Debug.WriteLine("Object Changed!");
            //throw new NotImplementedException();
        }

        private void SaveAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            Debug.WriteLine("Save action executed");
            Debug.WriteLine(sender);
            Debug.WriteLine(e);

            var currentObject = View.CurrentObject;
            IObjectSpace objectSpace = View.ObjectSpace;

        }

        //private void ObjectSpace_ObjectSaved(object sender, EventArgs e)
        //{
        //    Debug.WriteLine("ObjectSpace Saving");
        //    Debug.WriteLine(sender);
        //    Debug.WriteLine(e);
        //    // TODO: kalua lihat dari contoh Haermes, ada tambahan e.Object.GetType = GetType(FileDataEx), entah untuk apa
        //    if (View.CurrentObject.GetType() == TargetObjectType)
        //    {
        //        Goods goods = (
        //        Goods)View.CurrentObject;
        //        Debug.WriteLine(goods);
        //    }
        //}
    }
}
