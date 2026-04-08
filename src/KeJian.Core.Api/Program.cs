using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Security.Cryptography.X509Certificates;

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
                    webBuilder.ConfigureKestrel((context, serverOptions) =>
                    {
                        var kestrelConfig = context.Configuration.GetSection("Kestrel");

                        // HTTP 端口
                        serverOptions.Listen(IPAddress.Any, 5001, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        });

                        // HTTPS 端口 - 支持 SNI（多域名证书）
                        serverOptions.Listen(IPAddress.Any, 5002, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;

                            listenOptions.UseHttps(httpsOptions =>
                            {
                                // SNI 配置：根据域名选择对应证书
                                httpsOptions.ServerCertificateSelector = (connectionContext, domainName) =>
                                {
                                    if (string.IsNullOrEmpty(domainName))
                                    {
                                        return LoadCertificate(kestrelConfig, "Default");
                                    }

                                    // 根据域名选择证书
                                    if (domainName.Equals("api.ycgjie.cn", System.StringComparison.OrdinalIgnoreCase))
                                    {
                                        return LoadCertificate(kestrelConfig, "Api");
                                    }
                                    else if (domainName.Equals("image.ycgjie.cn", System.StringComparison.OrdinalIgnoreCase))
                                    {
                                        return LoadCertificate(kestrelConfig, "Image");
                                    }

                                    return LoadCertificate(kestrelConfig, "Api");
                                };
                            });
                        });
                    });

                    webBuilder.UseStartup<Startup>();
                });
        }

        /// <summary>
        /// 加载指定的证书配置
        /// </summary>
        private static X509Certificate2 LoadCertificate(IConfigurationSection kestrelConfig, string certificateName)
        {
            var certPath = kestrelConfig[$"Certificates:{certificateName}:Path"];
            var certPassword = kestrelConfig[$"Certificates:{certificateName}:Password"];

            if (!string.IsNullOrEmpty(certPath) && System.IO.File.Exists(certPath))
            {
                return new X509Certificate2(certPath, certPassword);
            }

            return null;
        }
    }
}