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

        string _id;
        string _name;
        Boolean _active;

        [Size(3)]
        public string Id
        {
            get => _id;
            set => SetPropertyValue(nameof(Id), ref _id, value);
        }

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