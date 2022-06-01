
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        builder.Build().Run();
        // Add services to the container.





    }
}