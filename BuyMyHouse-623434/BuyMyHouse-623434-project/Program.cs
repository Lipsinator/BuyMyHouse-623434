using BuyMyHouse_623434_project.QueueStorage;
using DAL.EFContext;
using DAL.Helpers;
using DAL.Interface;
using DAL.Repository;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLayer.Interface;
using ServiceLayer.Service;
using System.Threading.Tasks;

namespace BuyMyHouse_623434_project
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    // services
                    services.AddTransient<IBuyerInfoService, BuyerInfoService>();
                    services.AddTransient<IBlobService, BlobService>();

                    // repos
                    services.AddTransient<IBuyerInfoRepository, BuyerInfoRepository>();

                    //DBContexts
                    services.AddDbContext<BuyerInfoContext>();
                })
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}