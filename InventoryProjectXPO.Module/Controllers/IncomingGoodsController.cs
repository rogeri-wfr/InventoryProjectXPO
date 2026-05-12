

using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using InventoryProjectXPO.Module.BusinessObjects;
using InventoryProjectXPO.Module.BusinessObjects.Master;
using System.Diagnostics;

namespace InventoryProjectXPO.Module.Controllers
{
    public class IncomingGoodsController : ViewController<DetailView>
    {
        public IncomingGoodsController()
        {
            TargetObjectType = typeof(IncomingGoods);
            TargetViewType = ViewType.DetailView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            View.ObjectSpace.ObjectSaving += ObjectSpace_ObjectSaving;
            View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            View.ObjectSpace.ObjectSaving -= ObjectSpace_ObjectSaving;
            View.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
        }

        private int prevQuantity;
        private Goods prevGood;

        private void ObjectSpace_ObjectSaving(object sender, ObjectManipulatingEventArgs e)
        {
            // TODO: waktu save ini, update value dari inventory
            // TODO: Perlu bisa dapatkan value yang di-save itu apa, kalau bisa dapatkan value yang di-save itu, bisa langsung update inventory-nya, kalau tidak bisa, mungkin bisa pakai event lain yang lebih tepat
            var eClassName = e.Object.GetType().Name;
            if (eClassName == "IncomingGoods")
            {
                var eIncomingGoods = e.Object as IncomingGoods;
                //var relatedInventory = View.ObjectSpace.FindObject<Inventory>(new BinaryOperator("GoodFk", eIncomingGoods.GoodsFk[0].Oid));
                var relatedInventory = View.ObjectSpace.FindObject<Inventory>(new BinaryOperator("GoodFk", eIncomingGoods.GoodsFk));
                if (relatedInventory != null)
                {
                    // TODO: perlu bedakan antara new, edit, dan del
                    // New dan Edit harusnya bisa di sini, del buat controller baru id DetailView
                    // nanti perlu bisa ambil previous value, either mungkin bisa ambil dari e
                    // atua nanti ambil pakai FindObject
                    // katua mungkin lebih efisien di modesl-nya langsung, karena sepertinya ada function untuk itu

                    //int curStock = relatedInventory.CurrentStock;
                    //int inQuant = eIncomingGoods.Quantity;
                    //int newTot = curStock + inQuant;
                    //relatedInventory.CurrentStock = newTot;

                    // TODO: ini seperitnya yang bikin +3x karena redundant dengan model, perlu hapus salah satu
                    //if(eIncomingGoods.Session.IsNewObject(eIncomingGoods))
                    if (View.ObjectSpace.IsNewObject(eIncomingGoods))
                    {
                        relatedInventory.CurrentStock += eIncomingGoods.Quantity;
                    }
                    else
                    {
                        //// TODO: ini tidak bisa dapat previous value
                        //var prevValue = eIncomingGoods.Session.GetObjectByKey<IncomingGoods>(eIncomingGoods.Oid);
                        //var prevValue2 = View.ObjectSpace.FindObject<IncomingGoods>(new BinaryOperator("Oid", eIncomingGoods.Oid));
                        ////var prevValue3 = View.ObjectSpace.FindObject<IncomingGoods>(eIncomingGoods);
                        //// V4: tidak bisa pakai cara ini, GetOldValue tidak ada di Session. Mungkin fitur baru
                        ////var sess = ((XPObjectSpace)ObjectSpace).Session;
                        ////var memb = sess.GetClassInfo(eIncomingGoods).FindMember(nameof(IncomingGoods.Quantity));
                        ////var prevValue4 = sess.GetOldValue(eIncomingGoods, memb);
                        //XPMemberInfo miQuant = eIncomingGoods.ClassInfo.GetMember("Quantity");
                        //IXPModificationsStore ms = PersistentBase.GetModificationsStore(eIncomingGoods);
                        //var prevValue5 = ms.GetPropertyOldValue(miQuant);
                        //if (prevValue != null)
                        //{
                        //    //int newCurrentStock = relatedInventory.CurrentStock + (eIncomingGoods.Quantity - prevValue.Quantity);
                        //    //if(newCurrentStock < 0)
                        //    //{
                        //    //    throw new UserFriendlyException("Current stock cannot be negative");
                        //    //}
                        //    //relatedInventory.CurrentStock = newCurrentStock;
                        //    relatedInventory.CurrentStock += (eIncomingGoods.Quantity - prevValue.Quantity);
                        //}
                        // TODO: ini masih menganggap goods yang sama, nanti perlu kasih condition untuk goods berbeda
                        relatedInventory.CurrentStock += eIncomingGoods.Quantity - prevQuantity;
                    }
                    //relatedInventory.CurrentStock += eIncomingGoods.Quantity;
                    //relatedInventory.Session.CommitTransaction();
                    // NOTE: ini tak dipakai sepertinya aman
                    //relatedInventory.Save();
                }
                else
                {
                    // TODO: mungkin bisa bikin add data terkait dengan Goods terkait
                }
            }

        }

        // Kalau pakai ini, setiap field changes kena trigger, baiknya jangan takut affecting performance
        // NOTE: ini ternyata jalan juga pas jalankan Saving,
        // dimana saat itu ternyata baru e.NewValue & e.OldValue baru tidak null lagi
        // perlu pikrikan bagimana bisa tahu kalau ini lagi onSaving
        // mungkin bia kaish conditional if New & old-nya != null
        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            Debug.WriteLine("Check if this triggers everytime");
            //switch (e.PropertyName)
            //{
            //    case "Note":
            //        Debug.WriteLine("Note changed : " + e.NewValue);
            //        Debug.WriteLine("Note old value : " + e.OldValue);
            //        // TODO: kalau ini sudah oke, refactor codingan ini
            //        if (e.NewValue != null || e.OldValue != null)
            //        {
            //            Debug.WriteLine("Check apakah ini jalan cuma saat save");
            //            Debug.WriteLine($"Property -{e.PropertyName}- changed from '{e.OldValue}' to '{e.NewValue}'");
            //        }
            //        break;
            //}

            // sepertinya NewValue & OldValue bisa dipakai gini untuk tentukan apakah ini saving atua tidak
            if (e.NewValue != null || e.OldValue != null)
            {
                switch(e.PropertyName)
                {
                    case nameof(IncomingGoods.Quantity):
                        prevQuantity = (int)e.OldValue;
                        break;
                    case nameof(IncomingGoods.GoodsFk):
                        prevGood = (Goods)e.OldValue;
                        break;
                }
            }



        }
    }
}
