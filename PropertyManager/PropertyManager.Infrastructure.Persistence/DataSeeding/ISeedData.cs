using System.Threading.Tasks;

namespace PropertyManager.Infrastructure.Persistence.DataSeeding
{
    public interface ISeedData
    {
        Task SeedAsync();
    }
}
