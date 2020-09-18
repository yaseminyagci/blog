using Core.Interfaces;
using System;

namespace Core.BaseModels
{
    public abstract class DateModel : IDateModel
    {

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

    }
}
