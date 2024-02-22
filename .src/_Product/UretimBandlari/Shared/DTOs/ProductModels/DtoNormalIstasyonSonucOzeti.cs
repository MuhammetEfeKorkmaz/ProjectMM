namespace DTOs.ProductModels
{
    public class DtoNormalIstasyonSonucOzeti
    {
        public DtoNormalIstasyonSonucOzeti()
        {
            MinorHataliDurumlar = new List<string>();
            MajorHataliDurumlar = new List<string>();
        }
        public bool Sonuc { get; set; } = false;
        public string Soru { get; set; }=string.Empty;
        public List<string> MinorHataliDurumlar { get; set; }
        public List<string> MajorHataliDurumlar { get; set; }
        public bool DigeriSecilmisHataliDurumlarVarmi { get; set; } = false;
    }
}
