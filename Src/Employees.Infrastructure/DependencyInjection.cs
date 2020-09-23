using Employees.Domain.Accounts;
using Employees.Infrastructure.Database;
using Employees.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EmployeesDB");
            services.AddScoped<SqlConnectionFactory>(provider => new SqlConnectionFactory(connectionString));
            services.AddTransient<IAccountsRepository, AccountsRepository>();
            
            return services;
        }
    }
}