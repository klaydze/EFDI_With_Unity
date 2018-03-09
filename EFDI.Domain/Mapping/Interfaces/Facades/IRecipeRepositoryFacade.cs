using EFDI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDI.Domain.Mapping.Interfaces.Facades
{
    public interface IRecipeRepositoryFacade
    {
        RecipeDomain FindByRecipeId(int Id);
    }
}
