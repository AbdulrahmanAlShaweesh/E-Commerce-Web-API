
namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region Add services to the container.

            builder.Services.AddControllers();       // this controlls api to be able to deal or working with them
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // These two service for swagger open api, it implementated becouse we add them during the project configrations
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 

            #endregion

            var app = builder.Build();


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
