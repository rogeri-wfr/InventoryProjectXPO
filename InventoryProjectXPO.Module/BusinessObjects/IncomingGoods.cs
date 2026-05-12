using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using InventoryProjectXPO.Module.BusinessObjects.Master;
using System;
using System.Diagnostics;

namespace InventoryProjectXPO.Module.BusinessObjects
{
    [DefaultClassOptions]
    // kalau di Haermse 3.2, pakai HaermesBaseObject, dimana di situ ada settingan userCreated dkk
    public class IncomingGoods : BaseObject
    {
        public IncomingGoods(Session session) : base(session)
        {

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        
        protected override void OnSaving()
        {
            base.OnSaving();
            // NOTE: ini sebenernya bisa, tapi Robin minta lewat ViewController dulu
            // kalua ViewController tak bisa mungkin baru lewat onSaving ini
            //if (Session.IsNewObject(this))
            //{
            //    // TODO: need to add up quanitity to inventory
            //    // tapi sepertinya ini belum perfect, perlu test basic Goods & Inventory dulu baru coba lanjut sini
            //    Inventory inventory = Session.FindObject<Inventory>(new BinaryOperator("GoodFk", GoodsFk));
            //    if (inventory != null)
            //    {
            //        inventory.CurrentStock += Quantity;
            //        //inventory.SetMemberValue("CurrentStock", inventory.CurrentStock + Quantity);
            //        inventory.Save();
            //    }
            //    else
            //    {
            //        // TODO: harusnya kalau GoodsFk-nya belum ada di inentory, perlu ditambahkan lewat sini,
            //        // tapi bingung juga cara-nya bagaimana
            //        inventory = new Inventory(Session)
            //        {
            //            //GoodsFk = GoodsFk,
            //            //GoodsFk = this.GoodsFk
            //            //GoodsFk.Add(this.GoodsFk)
            //            //CurrentStock = Quantity
            //            CurrentStock = 0
            //        };
            //        //inventory.GoodsFk.AddRange(GoodsFk);

            //        inventory.Save();

            //        // mungkin dibeginikan jadi update 2x, tapi sepertinya aneh
            //        //Inventory inventoryAfter = Session.FindObject<Inventory>(new BinaryOperator("GoodsFk", GoodsFk));
            //        //inventoryAfter.CurrentStock += Quantity;
            //        //inventory.Save();
            //    }
            //}
            if (!Session.IsNewObject(this))
            {
                // TODO: ini untuk case kalau Goods tidak ada diubah,
                // kalau diubah berarti perlu adjust 2 inventory yang berbeda
                Inventory inventory = Session.FindObject<Inventory>(new BinaryOperator("GoodFk", GoodsFk));
                if (inventory != null)
                {
                    var prevQuantity = (int)Session.GetObjectByKey<IncomingGoods>(this.Oid).Quantity;
                    var prevQuantity2 = this.ClassInfo.FindMember(nameof(Quantity)).GetOldValue(this);
                    var newQuantity = this.Quantity;
                    Debug.WriteLine($"prevQuantity: {prevQuantity}, prevQuantity2: {prevQuantity2}, newQuantity: {newQuantity}, prevQuantity3: {_prevQuantity}");
                    inventory.CurrentStock += newQuantity - prevQuantity;
                    //inventory.Session.CommitTransaction();
                    inventory.Save();
                }
            }
        }

        int _prevQuantity;
        Goods _prevGoods;
        // OnChanged ini sepertinya tetap jalan setiap perubahan field layak-nya ObjectChanged di ViewController
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading)
            {
                switch (propertyName)
                {
                    case nameof(Quantity):
                        _prevQuantity = (int)oldValue;
                        break;
                    case nameof(GoodsFk):
                        _prevGoods = (Goods)oldValue;
                        break;
                    default:
                        break;
                }
            }
            Debug.WriteLine($"_prevQuantity : {_prevQuantity}, _prevGoods: {_prevGoods}");
            //if(propertyName == nameof(Quantity) && !IsLoading)
            //{
            //    _prevQuantity = (int)oldValue;
            //}
        }


        Goods _goods;
        // TODO: coba baca ini utnuk apa, ini otomatis direkomendasikan
        //[Association("Goods-Inventories")]
        int _quantity;
        DateTime _date;
        string _note;

        //public Goods GoodFK
        //{
        //    get => _goods;
        //    set => SetPropertyValue(nameof(GoodFK), ref _goods, value);
        //}
        // kalau ini 
        //[Aggregated]
        // NOTE: sepertinya tidka bisa pakai aggregated, karena jadi tidka bisa diedit,
        // dan jadi seperit dropdown
        [Association("Goods-IncomingGoods")]
        //public XPCollection<Goods> GoodsFk
        //{
        //    get => GetCollection<Goods>(nameof(GoodsFk));
        //}
        //[Aggregated]
        public Goods GoodsFk
        {
            get => _goods;
            set => SetPropertyValue(nameof(GoodsFk), ref _goods, value);
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

        public string Note
        {
            get => _note;
            set => SetPropertyValue(nameof(Note), ref _note, value);
        }
    }
}