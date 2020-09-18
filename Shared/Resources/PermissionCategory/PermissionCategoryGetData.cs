using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Resources.PermissionCategory
{
    public class PermissionCategoryGetData
    {

        public string Label { get; set; }
        public string VisibleLabel { get; set; }
        public string Description { get; set; }
        public string DateCreated { get; set; }
    }
}
