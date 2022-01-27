using System;
using BookStore.Function;
using BookStore.Function.Configurations;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

[assembly: FunctionsStartup(typeof(Startup))]
namespace BookStore.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            builder.Services.Configure<DatabaseConfig>(opt =>
            {
                opt.ConnectionString = connectionString;
            });

            builder.Services.AddHttpClient();
            builder.Services.AddTransient(s => new NpgsqlConnection(connectionString));
        }
    }
}
