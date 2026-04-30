using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Diagnostics;

namespace InventoryProjectXPO.Module.BusinessObjects.Master
{
    [DefaultClassOptions]
    // kalau di Haermse 3.2, pakai HaermesBaseObject, dimana di situ ada settingan userCreated dkk
    public class Goods : BaseObject
    {
        private int onSavingCount = 0;
        private int onSavedCount = 0;

        public Goods(Session session) : base(session)
        {

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            // TODO: disarankan pakai ViewController untuk update other table-nya

            //if(Session.IsNewObject(this))
            //{
            //    Debug.WriteLine("New Goods, perlu update table Inventory untuk barang ini dengan stock 0");
            //    Inventory inventory = new Inventory(Session)
            //    {
            //        GoodFk = this,
            //        CurrentStock = 0
            //    };
            //    inventory.Save();
            //}



            //onSavingCount++;
            ////Active = true;

            //// TODO: when it's new, need to add it into Inventory as well with stock 0, so that when it's added into inventory, it will be updated, not created
            //if (Session.IsNewObject(this))
            //{

            //    //Active = true;
            //    // mestinya sih tidka perlu pengecekan apakah data-nya ada di inventory,
            //    // karena ini IsNewObject, jadi harusnya memang belum ada
            //    //if(Session.FindObject<Inventory>(new BinaryOperator("GoodFk", this)) {

            //    //}

            //    //Inventory inventory = new Inventory(Session)
            //    //{

            //    //    CurrentStock = 0
            //    //};
            //    //inventory.GoodsFk.Add(this);
            //    // TODO: for easy workaround, just check if whether GoodFk exists in table to save
            //    // tapi ini bahaya juga karena seprtinya tidak bisa untuk inventory in & out
            //    Debug.WriteLine("is new object, ini isi value dari this : ");
            //    Debug.WriteLine(this);
            //    Debug.WriteLine("is Saving : " + this.IsSaving);
            //    Debug.WriteLine("is Loading : " + this.IsLoading);
            //    Debug.WriteLine("onSavingCount : " + onSavingCount);

            //    // woraround-nya tidak ngefek
            //    //if(Session.FindObject<Inventory>(new BinaryOperator("GoodFk", this)) == null) 
            //    //{
            //    //    Debug.WriteLine("Data belum ada di inventory, jadi buat baru");
            //    //    Inventory inventory = new Inventory(Session)
            //    //    {
            //    //        GoodFk = this,
            //    //        CurrentStock = 0
            //    //    };
            //    //    //inventory.Save();
            //    //}
            //    //else
            //    //{
            //    //    Debug.WriteLine("Data sudah ada di inventory, jadi tidak perlu buat baru");
            //    //}

            //    //Inventory inventory = new Inventory(Session)
            //    //{
            //    //    //GoodFk = this,
            //    //    CurrentStock = 0
            //    //};
            //    //inventory.Save();

            //}
            //else
            //{
            //    Debug.WriteLine("Bukan new object, tapi ini isi value dari this : ");
            //    Debug.WriteLine(this);
            //}   

        }

        //protected override void OnSaved()
        //{
        //    base.OnSaved();
        //    onSavedCount++;
        //    Debug.WriteLine("OnSaved : " + this);
        //    Debug.WriteLine("OnSavedCount : " + onSavedCount);

        //    if(Session.FindObject<Inventory>(new BinaryOperator("GoodFk", this)) == null) 
        //    {
        //        Debug.WriteLine("Data belum ada di inventory, jadi buat baru");
        //        Inventory inventory = new Inventory(Session)
        //        {
        //            GoodFk = this,
        //            CurrentStock = 0
        //        };
        //        //inventory.Save();
        //    }
        //    else
        //    {
        //        Debug.WriteLine("Data sudah ada di inventory, jadi tidak perlu buat baru");
        //    }
        //}

        //protected override void OnDeleting()
        //{
        //    Inventory findInventory = Session.FindObject<Inventory>(new BinaryOperator("GoodFk", this));
        //    findInventory?.Delete();
        //}

        //string _id;
        string _name;
        // TODO: not sure ini best practice untuk default value di sini atau di AfterConstruction/OnSaving
        bool _active =true;

        // NOTE: harusnya tak perlu ini karena oid by default dihandle XPO
        //[Size(3)]
        //public string Id
        //{
        //    get => _id;
        //    set => SetPropertyValue(nameof(Id), ref _id, value);
        //}

        public string Name
        {
            get => _name;
            set => SetPropertyValue(nameof(Name), ref _name, value);
        }

        public bool Active
        {
            get => _active;
            set => SetPropertyValue(nameof(Active), ref _active, value);
        }
    }
}