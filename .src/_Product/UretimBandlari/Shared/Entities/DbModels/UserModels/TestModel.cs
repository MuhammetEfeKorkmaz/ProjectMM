using FullSharedResults.BaseModels;

namespace Entities.DbModels.UserModels
{

    public class Ogrenci: _EntitiyBaseModel
    {
        public Ogrenci()
        {
            Sinif = new();
            Adres = new();
            Kitaps = new HashSet<OgrenciKitap>();
        }
       
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public Sinif Sinif { get; set; }
        public int SinifId { get; set; }
        public Adres Adres { get; set; }  
        public IEnumerable<OgrenciKitap> Kitaps { get; set; }
    }
    public class Sinif : _EntitiyBaseModel
    {
        public Sinif()
        {
            Ogrencis = new HashSet<Ogrenci>();
        }
 
        public string SinifAdi { get; set; }
        public IEnumerable<Ogrenci> Ogrencis { get; set; }
    }



    public class Adres : _EntitiyBaseModel
    { 
        public string AdresAdi { get; set; }
        public Ogrenci Ogrenci { get; set; } 
    }

    public class Kitap : _EntitiyBaseModel
    {
        public Kitap()
        {
            Ogrencis = new HashSet<OgrenciKitap>();
            Yazars = new HashSet<YazarKitap>();
        } 
        public string KitapAdi { get; set; }
        public IEnumerable<OgrenciKitap> Ogrencis { get; set; }
        public IEnumerable<YazarKitap> Yazars { get; set; }
    }

    public class Yazar : _EntitiyBaseModel
    {
        public Yazar()
        {
            Kitaps = new HashSet<YazarKitap>();
        }
        public string YazarAdi { get; set; }
        public IEnumerable<YazarKitap> Kitaps { get; set; }
    }


    public class YazarKitap : _EntitiyBaseModel
    {
        public int YazarId { get; set; }
        public int KitapId { get; set; }
        public Yazar Yazar { get; set; }
        public Kitap Kitap { get; set; }

    }

    public class OgrenciKitap : _EntitiyBaseModel
    {
        public int OgrenciId { get; set; }
        public int KitapId { get; set; }
        public Ogrenci Ogrenci { get; set; }
        public Kitap Kitap { get; set; }

    }

}
