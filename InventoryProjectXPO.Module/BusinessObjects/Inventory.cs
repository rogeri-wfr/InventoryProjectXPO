using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using InventoryProjectXPO.Module.BusinessObjects.Master;
using System;

// TODO: need t adjust so user can only edit inventory
namespace InventoryProjectXPO.Module.BusinessObjects
{
    [DefaultClassOptions]
    [CreatableItem(false)]
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

        Goods _goods;
        // TODO: coba baca ini utnuk apa, ini otomatis direkomendasikan
        // reference 1 : https://docs.devexpress.com/XPO/DevExpress.Xpo.AssociationAttribute
        // reference 2 : https://docs.devexpress.com/XPO/2041/create-a-data-model/relationships-between-objects
        //[Association("Goods-Inventories")]
        int _currentStock;

        //public Goods GoodFK
        //{
        //    get => _goods;    
        //    set => SetPropertyValue(nameof(GoodFK), ref _goods, value);
        //}

        // kalua relation one to many, sepertinya pakai xp collection ini
        //[Association("Goods-Inventories")]

        // pakai XPCollection ini error,
        // sepertinya karena 1 goods harusnya 1 inventory, berarti mestinya 1 to 1, bukan 1 to many
        //public XPCollection<Goods> GoodsFk
        //{
        //    //get { return GetCollection<Goods>(nameof(GoodsFk)); }
        //    get => GetCollection<Goods>(nameof(GoodsFk));
        //}

        // TODO: sepertinya ini perlu foreign key yang jelas untuk association
        //[Association("Goods-Inventories")]
        // TODO: ini mestinya bisa pakai aggregated, tapi bisa skip dulu karena ini mesitnya juga tak boleh edit
        //[Aggregated]
        public Goods GoodFk
        {
            get => _goods;
            set => SetPropertyValue(nameof(GoodFk), ref _goods, value);
        }

        public int CurrentStock
        {
            get => _currentStock;
            set => SetPropertyValue(nameof(CurrentStock), ref _currentStock, value);
        }
    }
}