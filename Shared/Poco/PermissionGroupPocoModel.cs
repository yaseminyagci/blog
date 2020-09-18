using System.Collections.Generic;

namespace Shared.Poco
{
    public class PermissionGroupPocoModel
    {
        public string Title { get; set; }
        public string VisibleTitle { get; set; }
        public List<PermissionPocoModel> PossiblePermissionsList { get; set; }
    }
}
