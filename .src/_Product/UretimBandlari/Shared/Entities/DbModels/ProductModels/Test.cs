using FullSharedResults.BaseModels;
namespace Entities.DbModels.ProductModels
{
    public class Test:_EntitiyBaseModel
    {
        public string GroupKey { get; set; }
        public virtual UrunSeri UrunSeri { get; set; }
        public virtual Bant Bant { get; set; }
        public virtual Istasyon Istasyon { get; set; }


        public virtual List<TestCevap> TestCevaps { get; set; }

        /// <summary>
        /// [0=>False]    [1=>True]
        /// </summary>
        public int TestinBasarisiIcinKritik { get; set; }

    }


  

}
