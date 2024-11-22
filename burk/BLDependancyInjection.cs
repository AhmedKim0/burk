

using burk.BL.Imp;
using burk.BL.interfaces;
using burk.Mapper;
using Burk.DAL.Repositories.imp;
using Burk.DAL.Repositories.Interface;

namespace burk
{
    public static class BLDependancyInjection
    {
       public static IServiceCollection AddBL(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddServices();

            return services;
        }
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepo,ClientRepository>();

        }
        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService,AccountService>();
            services.AddAutoMapper(typeof(Mappingprofile));

        }


    }
}
