
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data.DbContexts;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region Add services to the container.

            builder.Services.AddControllers();       // this controlls api to be able to deal or working with them
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // These two service for swagger open api, it implementated becouse we add them during the project configrations
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defualtConnection"));
            });

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            #endregion

            var app = builder.Build();

            // For data seeding
            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync(); // tell that the method is working async and return task (void) 

            #region Configure the HTTP request pipeline [MiddlesWare] .
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
