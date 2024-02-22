using FullSharedResults.BaseModels;

namespace DTOs.UserModels.Commands
{
    public class OgrenciAddCommandDto: _DtoBaseModel
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }


        public string SinifAdi { get; set; }
        public string AdresAdi { get; set; }
        public ICollection<string> Kitaps { get; set; }
    }
}
