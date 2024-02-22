using FullSharedResults.BaseModels;
namespace Entities.DbModels.ProductModels
{
    public class Sablon:_EntitiyBaseModel
    {
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public int KalmasiIcinMinMinorSayisi { get; set; }
        public string OperatorUyarisi { get; set; }
        public string KararIstasyonuUyari { get; set; }
        public virtual List<SablonDetay> SablonDetays { get; set; }

    }


 

}
