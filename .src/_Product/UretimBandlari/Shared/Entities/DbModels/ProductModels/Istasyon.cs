using FullSharedResults.BaseModels;

namespace Entities.DbModels.ProductModels
{
    public class Istasyon : _EntitiyBaseModel
    {
        public string Adi { get; set; }
        public string IstasyonSeriNo { get; set; }
        public virtual Bant Bant { get; set; }
    }
}
