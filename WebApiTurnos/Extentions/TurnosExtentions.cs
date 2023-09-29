using WebApiTurnos.Core.Services.BranchSer;
using WebApiTurnos.Core.Services.ShiftSer;
using WebApiTurnos.Core.Services.UserSer;
using WebApiTurnos.Data.Repositories.BranchRep;
using WebApiTurnos.Data.Repositories.IShiftRep;
using WebApiTurnos.Data.Repositories.UserRep;

namespace WebApiTurnos.Extentions
{
    public static class TurnosExtentions
    {
        public static IServiceCollection AddTurnosExtentions(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IShiftService, ShiftService>();
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IBranchRepository, BranchRepository>();

            return services;
        }
    }
}
