using Entities.DbModels.UserModels;
using FullSharedResults.BaseModels;
namespace Entities.DbModels.ProductModels
{
    public class TestUrunSeriBazinda:_EntitiyBaseModel
    {
        public virtual Test Test { get; set; }
       // public virtual UrunSeri UrunSeri { get; set; }

        public virtual SystemUser SystemUser { get; set; }




        public string TestGroupKey { get; set; }
        public string UrunSeri { get; set; }

        public DateTime TestTarihi { get; set; }



        /// <summary>
        /// [0=>False Kaldı]    [1=>True Geçti]   
        /// </summary>
        public bool Sonuc { get; set; }
    }

  







}
