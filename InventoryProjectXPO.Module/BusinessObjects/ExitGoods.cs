using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;

namespace InventoryProjectXPO.Module.BusinessObjects.Master
{
    [DefaultClassOptions]
    // kalau di Haermse 3.2, pakai HaermesBaseObject, dimana di situ ada settingan userCreated dkk
    public class ExitGoods : BaseObject
    {
        public ExitGoods(Session session) : base(session)
        {

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        //Goods _goods;
        // TODO: coba baca ini utnuk apa, ini otomatis direkomendasikan
        //[Association("Goods-Inventories")]
        int _quantity;
        DateTime _date;

        //public Goods GoodFK
        //{
        //    get => _goods;
        //    set => SetPropertyValue(nameof(GoodFK), ref _goods, value);
        //}
        public XPCollection<Goods> GoodsFk
        {
            get => GetCollection<Goods>(nameof(GoodsFk));
        }

        public int Quantity
        {
            get => _quantity;
            set => SetPropertyValue(nameof(Quantity), ref _quantity, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetPropertyValue(nameof(Date), ref _date, value);
        }
    }
}