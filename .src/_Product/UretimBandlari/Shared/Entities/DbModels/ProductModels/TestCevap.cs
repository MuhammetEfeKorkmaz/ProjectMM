using FullSharedResults.BaseModels;
namespace Entities.DbModels.ProductModels
{
    public class TestCevap : _EntitiyBaseModel
    {
        public virtual Test Test { get; set; }
        public virtual SablonDetay SablonDetay { get; set; }

        public string CevapString { get; set; }
        public int CevapInt { get; set; }
        public bool CevapBool { get; set; }


        /// <summary>
        /// [0=>False]    [1=>True]
        /// </summary>
        public bool CevapExtra { get; set; }
        public string CevapExtraAciklama { get; set; }


        /// <summary>
        /// [0=>False]    [1=>True]
        /// </summary>
        public int TestinBasarisiIcinKritik { get; set; }

    }

}
