using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GuiJun.Core.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://api.ycgjie.cn:5001");
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}