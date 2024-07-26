using Blogging.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogging.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrasturcture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BloggingDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BloggingDb")));
    }
}
