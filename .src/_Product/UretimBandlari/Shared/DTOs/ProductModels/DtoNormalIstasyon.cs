using FullSharedResults.BaseModels;

namespace DTOs.ForOperational
{
    public class KullaniciVeIstasyonBilgisi
    {
      
    }




    public class DtoNormalIstasyon: _DtoBaseModel
    {
        public DtoNormalIstasyon()
        {
            DtoNormalIstasyonDetays = new List<DtoNormalIstasyonDetay>();
        }
        public int Id { get; set; }
        public string UrunSeri_Urun { get; set; } = string.Empty;
        public string UrunSeri_Seri { get; set; } = string.Empty;
        public string Sablon_OperatorUyarisi { get; set; } = string.Empty;
        public int Test_Id { get; set; } = 0;
        public int Sablon_KalmasiIcinMinMinorSayisi { get; set; }= 0;
        public string Test_GroupKey { get; set; } = string.Empty;
        public int SablonDetay_Id { get; set; } = 0;
        public int Sablon_Id { get; set; } = 0;
        public string SystemUser_AdiSoyadi { get; set; } = string.Empty;
        public string UretimYeri_Adi { get; set; } = string.Empty;
        public string Bant_Adi { get; set; } = string.Empty;
        public string Istasyon_Adi { get; set; } = string.Empty;


        public List<DtoNormalIstasyonDetay> DtoNormalIstasyonDetays { get; set; }


    }

  


    public class DtoNormalIstasyonDetay
    {
        public string SablonDetay_GroupKey { get; set; } = string.Empty;
        public int SablonDetay_SoruTipi { get; set; } = 0;
        public string SablonDetay_Soru { get; set; } = string.Empty;
        public string SablonDetay_Secenek { get; set; } = string.Empty;
        public bool SablonDetay_HataDerecesi { get; set; } = false;


        /// <summary>
        /// [1=>Yok]     [2=>Sadece Extra]     [3=>Extra ve Açıklama Zorunlu]    [4=>Extra ve Açıklama Zorunsuz]  
        /// </summary>
        public int SablonDetay_ExtraVeAciklamasi { get; set; } = 0;


        public string TestCevap_CevapString { get; set; } = string.Empty;
        public int TestCevap_CevapInt { get; set; } = 0;
        public bool TestCevap_CevapBool { get; set; } = false;


        /// <summary>
        /// [0=>False]    [1=>True]
        /// </summary>
        public bool TestCevap_CevapExtra { get; set; } = false;
        public string TestCevap_CevapExtraAciklama { get; set; } = string.Empty;
    }





   



}
