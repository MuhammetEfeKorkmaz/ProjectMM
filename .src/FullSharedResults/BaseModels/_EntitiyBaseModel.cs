namespace FullSharedResults.BaseModels
{
    public class _EntitiyBaseModel
    {
        public int Id { get; set; }

        /// <summary>
        /// [0=>User Deleted]    [1=>Active]     [2=>Admin Deleted] 
        /// </summary>
        public int Status { get; set; } = DbStatusEnum.Active.GetDbStatus();
    }



    public enum DbStatusEnum : int
    {
        UserDeleted = 0,
        Active = 1,
        AdminDeleted = 2,
    }
    public static class DbStatusEnumInt
    {
        public static int GetDbStatus(this DbStatusEnum dbStatus)
        {
            return (int)dbStatus;
        }
        public static DbStatusEnum SetDbStatus(this int dbStatus)
        {
            return (DbStatusEnum)dbStatus;
        }
    }

}
