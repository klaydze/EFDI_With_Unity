using EFDI.CommonLibrary.Models.Contracts;
using EFDI.CommonLibrary.Models.Enums;

namespace EFDI.CommonLibrary.Models.Base
{
    public class DomainModelBase : IObjectState
    {
        public ObjectState ObjectState { get; set; }
    }
}
