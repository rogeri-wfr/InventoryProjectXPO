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
using DevExpress.Xpo;

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

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            View.ObjectSpace.ObjectSaving -= ObjectSpace_ObjectSaving;
        }

        private void ObjectSpace_ObjectSaving(object sender, ObjectManipulatingEventArgs e)
        {
            var eClassName = e.Object.GetType().Name;

            // NOTE: sepertinya memang perlu conditional seperti ini,
            // possibly karena newInventory.Save(), jadi balik ke sini lagi, berujung value e.Object-nya berubah 
            // Kalua banyak bisa jadikan switch case saja biar rapi
            if(eClassName == "Goods")
            {
                var eGoods = e.Object;
                var isNew = View.ObjectSpace.IsNewObject(eGoods);
                //Goods eGoods2 = (Goods)e.Object;
                //if(TargetObjectType.GUID == Guid.Empty) { }
                // TODO: mungkin bisa coba pakai ini saja instead of pakai inventoryExists seperti di bawah
                // Harusnya bisa lebih efisien gitu 
                //if (View.ObjectSpace.IsNewObject(e.Object)) { }

                //var inventoryExists = View.ObjectSpace.FindObject<Inventory>(new BinaryOperator("GoodFk", e.Object));
                //if (inventoryExists == null)
                if (isNew)
                {

                    Debug.WriteLine("Inventory no exists");

                    // TODO: ini perlu cari alternative, kalua pakai cara yang sama seperti Haermes kena error karena bentuknya beda
                    // kalau coba trace debug-nya memang benar, bentuknya beda 
                    //var obj = (Inventory)e.Object;
                    //obj.CurrentStock = 0;

                    // TOOD: coba implement ini saja instead of pakai pengganti CType();
                    Inventory newInventory = View.ObjectSpace.CreateObject<Inventory>();
                    //var eInventory = e.Object;
                    newInventory.GoodFk = (Goods)eGoods;
                    newInventory.CurrentStock = 0;
                    newInventory.Save();
                }
                else
                {
                    Debug.WriteLine("Inventory does exists : ");
                }
            } else
            {
                // TODO: mungkin perlu tambahan error log kalau misalkan bukan goods atau inventory, tapi mestinya tak perlu
                // karena bisa jadi else-nya banyak
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
