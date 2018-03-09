using EFDI.CommonLibrary.Models.Contracts;
using EFDI.CommonLibrary.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDI.CommonLibrary.Models.Base
{
    public class PersistenceModelBase : IObjectState
    {
        protected PersistenceModelBase()
        {
            CreatedDate = DateTime.UtcNow;
        }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(100)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        //public bool IsActive { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
