using FullSharedResults.BaseModels;

namespace Entities.DbModels.ProductModels
{
    public class Bant : _EntitiyBaseModel
    {
        public string Adi { get; set; }
        public virtual UretimYeri UretimYeri { get; set; }

        public virtual List<Istasyon> Istasyons { get; set; }
    }
}
