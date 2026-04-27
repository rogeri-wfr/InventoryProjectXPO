using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;

namespace InventoryProjectXPO.Module.BusinessObjects.Master
{
    [DefaultClassOptions]
    // kalau di Haermse 3.2, pakai HaermesBaseObject, dimana di situ ada settingan userCreated dkk
    public class Inventory : BaseObject
    {
        public Inventory(Session session) : base(session)
        {

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        //Goods _goods;
        // TODO: coba baca ini utnuk apa, ini otomatis direkomendasikan
        // reference 1 : https://docs.devexpress.com/XPO/DevExpress.Xpo.AssociationAttribute
        // reference 2 : https://docs.devexpress.com/XPO/2041/create-a-data-model/relationships-between-objects
        [Association("Goods-Inventories")]
        int _currentStock;

        //public Goods GoodFK
        //{
        //    get => _goods;
        //    set => SetPropertyValue(nameof(GoodFK), ref _goods, value);
        //}
        
        // kalua relation one to many, sepertinya pakai xp collection ini
        public XPCollection<Goods> GoodsFk
        {
            //get { return GetCollection<Goods>(nameof(GoodsFk)); }
            get => GetCollection<Goods>(nameof(GoodsFk));
        }

        public int CurrentStock
        {
            get => _currentStock;
            set => SetPropertyValue(nameof(CurrentStock), ref _currentStock, value);
        }
    }
}