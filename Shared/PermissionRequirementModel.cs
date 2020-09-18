namespace Shared
{
    public sealed class PermissionRequirementModel
    {
        public string CategoryId { get; set; }
        public string PermissionId { get; set; }
        public bool IsEqual(string permissionId, string categoryId = null)
        {
            var isCategoryEqual = true;
            var isPermissionEqual = permissionId == PermissionId;


            if (!string.IsNullOrEmpty(CategoryId))
            {
                if (!string.IsNullOrEmpty(categoryId))
                    isCategoryEqual = this.CategoryId == categoryId;
                else
                    isCategoryEqual = false;
            }
            bool isSucces= isCategoryEqual && isPermissionEqual;

            return isSucces;

        }




        public PermissionRequirementModel(string permissionId, string categoryId = null)
        {
            PermissionId = permissionId;
            CategoryId = categoryId;
        }
    }
}
