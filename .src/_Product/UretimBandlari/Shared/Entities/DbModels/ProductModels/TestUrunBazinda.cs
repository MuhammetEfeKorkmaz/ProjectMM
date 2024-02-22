using Entities.DbModels.UserModels;
using FullSharedResults.BaseModels;
namespace Entities.DbModels.ProductModels
{
    public class TestUrunBazinda : _EntitiyBaseModel
    {
        public virtual Test Test { get; set; }
        public virtual UrunSeri UrunSeri { get; set; }

        public virtual SystemUser SystemUser { get; set; }

        public DateTime TestOlusturmaTarihi { get; set; }


        public int BasariliSayisi { get; set; }
        public int BasarisizSayisi { get; set; }
        public int ToplamSayi { get; set; }

    }
}
