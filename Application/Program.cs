using DataAccess;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Configuration;
using Application;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

        builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")!));
        
        builder.Services.AddAuthentication();
        builder.Services.ConfigureIdentity();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddAutoMapper(typeof(MapperInitializer));


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
