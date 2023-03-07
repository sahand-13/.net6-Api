using Application.Interfaces.Repository.GenericRepository;
using Cqrs.Domain;
using Infrastructure.Context;
using Infrastructure.RepositoryServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region ===[ Add DataBase Context ]=============================================================
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            #endregion 

            #region ===[ Generic Repository ]=============================================================
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            #region ======[ Services ]=======================================================================

            #endregion
        }
    }
}
