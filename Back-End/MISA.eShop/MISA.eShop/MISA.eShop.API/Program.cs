using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MISA.eShop.API
{
    /// <summary>
    /// Lớp khởi tạo chương trình
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Hàm khởi tạo ctrinh
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Hàm build host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
