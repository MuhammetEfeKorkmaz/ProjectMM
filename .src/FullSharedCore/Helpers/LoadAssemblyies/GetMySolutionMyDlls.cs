using System.Reflection;

namespace FullSharedCore.Helpers.LoadAssemblyies
{
    public static class GetMySolutionMyDlls
    {
         

       // public static Assembly[] List { get => _list ?? (_list = Load()); }
        public static Assembly[] List { get => Load(); }


        private readonly static List<string> Names=new List<string>() {"Business.dll", "Core.dll", "Dal.dll", "EndPointApi.dll", "Entities.dll", "DTOs.dll", "FullSharedCore.dll", "FullSharedResults.dll" };
        private static Assembly[] _list { get; set; }
        private static Assembly[] Load()
        {
            List<Assembly> list = new List<Assembly>(); 
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var item2 in Names)
                {
                    if (item2 == item.ManifestModule.Name)
                    {
                        list.Add(item);
                        break;
                    }
                } 
            } 
            return list.ToArray();
        }
    }
}
