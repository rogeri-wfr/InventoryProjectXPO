using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;

namespace InventoryProjectXPO.Module.BusinessObjects.Master
{
    [DefaultClassOptions]
    // kalau di Haermse 3.2, pakai HaermesBaseObject, dimana di situ ada settingan userCreated dkk
    public class Goods : BaseObject
    {
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
            //Active = true;

            // TODO: when it's new, need to add it into Inventory as well with stock 0, so that when it's added into inventory, it will be updated, not created
            if (Session.IsNewObject(this))
            {
                //Active = true;
                Inventory inventory = new Inventory(Session)
                {
                    CurrentStock = 0
                };
                inventory.GoodsFk.Add(this);
            }
        }

        //string _id;
        string _name;
        // TODO: not sure ini best practice untuk default value di sini atau di AfterConstruction/OnSaving
        Boolean _active =true;

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

        public Boolean Active
        {
            get => _active;
            set => SetPropertyValue(nameof(Active), ref _active, value);
        }
    }
}