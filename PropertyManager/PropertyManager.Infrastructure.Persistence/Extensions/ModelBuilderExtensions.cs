using Microsoft.EntityFrameworkCore;

namespace PropertyManager.Infrastructure.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void RemovePluralTableNames(
            this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }
    }
}
