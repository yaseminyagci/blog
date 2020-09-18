using System;

namespace Core.Interfaces
{
    public interface IDateModel
    {

        DateTime DateCreated { get; set; }
        DateTime? DateModified { get; set; }
        DateTime? DateDeleted { get; set; }

    }
}
