using FullSharedResults.BaseModels;
namespace Entities.DbModels.ProductModels
{
    public class SablonDetay : _EntitiyBaseModel
    {
        public string GroupKey { get; set; }

        /// <summary>
        /// [1=>Çoktan Seçmeli-Çok Seçim]      [2=>Çoktan Seçmeli-Tek Seçim]     [3=>Evet/Hayir]    [4=>Var/Yok]     [5=>Değer Eşleştirme İnt]      [6=>Değer Eşleştirme String]
        /// </summary>
        public int SoruTipi { get; set; }
        public string Soru { get; set; }


        /// <summary>
        /// [1=>Boş]
        /// </summary>
        public string Secenek { get; set; } = "1";


        /// <summary>
        /// [0=>Minör Hata]      [1=>Majör Hata]
        /// </summary>
        public bool HataDerecesi { get; set; } = false;





        /// <summary>
        /// [1=>Yok]     [2=>Sadece Extra]     [3=>Extra ve Açıklama Zorunlu]    [4=>Extra ve Açıklama Zorunsuz]  
        /// </summary>
        public int ExtraVeAciklamasi { get; set; }


        public virtual Sablon Sablon { get; set; }

    }
}
