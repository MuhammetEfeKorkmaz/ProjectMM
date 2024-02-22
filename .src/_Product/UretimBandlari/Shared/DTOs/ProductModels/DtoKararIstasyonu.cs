using FullSharedResults.BaseModels;

namespace DTOs.ForOperational
{

    public class DtoKararIstasyonu : _DtoBaseModel
    {
        public DtoKararIstasyonu()
        {
            DtoKararIstasyonuDetays = new List<DtoKararIstasyonuDetay>();
        }
        public string TestUrunSeriBazinda_UrunSeri { get; set; }
        public string UrunSeri_Seri { get; set; }
        public string Sablon_KararIstasyonuUyari { get; set; }
      
        public string Test_GroupKey { get; set; }
        public string Test_Urun { get; set; }

        public string Istasyon_Adi { get; set; }
        public string Bant_Adi { get; set; } 
        public string SystemUser_AdiSoyadi { get; set; }

        public List<DtoKararIstasyonuDetay> DtoKararIstasyonuDetays { get; set; }
        public int Id { get; set; }
    }

    public class DtoKararIstasyonuDetay
    {
        public string SystemUser_AdiSoyadi { get; set; }
        public int TestUrunSeriBazinda_TestId { get; set; }
        public bool TestUrunSeriBazinda_Sonuc { get; set; }
        public DateTime TestUrunSeriBazinda_TestTarihi { get; set; }
        
    }



}
