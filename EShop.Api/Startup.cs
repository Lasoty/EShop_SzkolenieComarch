using Autofac;
using EShop.Data.Context;
using EShop.Data.IoC;
using EShop.Data.Repositories;
using EShop.Services.IoC;
using EShop.Services.Sale;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("EShopDb");
        });

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

    }

    public void Configure(IApplicationBuilder app, ApplicationDbContext dbContext)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();

        app.UseEndpoints(endpoint => 
        {
            endpoint.MapControllers();
        });

        Seeder.Seed(dbContext);
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new DataModule());
        builder.RegisterModule(new ServicesModule());
    }
}