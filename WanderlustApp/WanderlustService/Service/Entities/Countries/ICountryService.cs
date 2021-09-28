using System.Threading.Tasks;
using WanderlustPersistence.Entity;
using WanderlustService.Service.Common;

namespace WanderlustService.Service.Entities.Countries
{
    /// <summary>
    /// A contract for a service manipulating with <see cref="Country"/>
    /// </summary>
    public interface ICountryService : IEntityService<Country>
    {
    }
}
