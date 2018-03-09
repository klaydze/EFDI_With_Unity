using EFDI.CommonLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDI.Domain.Models
{
    public class RecipeDomain : DomainModelBase
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int? CategoryId { get; set; }
    }
}
