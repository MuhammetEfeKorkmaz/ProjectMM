using FullSharedResults.BaseModels;
namespace Entities.DbModels.ProductModels
{
    public class UretimYeri : _EntitiyBaseModel
    {
        public string Adi { get; set; }
        public virtual List<Bant> Bants { get; set; }
    }



 

  
}
