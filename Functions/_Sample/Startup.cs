using System;
using BookStore.Sample.Function;
using BookStore.Sample.Function.Configurations;
using BookStore.Sample.Function.Interface;
using BookStore.Sample.Function.Repository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

[assembly: FunctionsStartup(typeof(Startup))]
namespace BookStore.Sample.Function
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

            //TEST

            builder.Services.AddHttpClient();
            builder.Services.AddTransient(s => new NpgsqlConnection(connectionString));
            builder.Services.AddTransient<IBookRepository, BookRepository>();
        }
    }
}
