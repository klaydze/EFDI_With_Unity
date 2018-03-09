using EFDI.CommonLibrary.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace EFDI.CommonLibrary.Models.Contracts
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
