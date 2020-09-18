using Core.Enums;
using Core.Interfaces;

namespace Core.BaseModels
{
    public abstract class HelperModel : DateModel, IHelperModel
    {

        public byte Status { get; set; }

        protected HelperModel()
        {
            Status = (byte)RecordStatus.Active;
        }
    }
}
