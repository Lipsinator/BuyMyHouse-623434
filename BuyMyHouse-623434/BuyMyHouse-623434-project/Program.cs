using DAL.EFContext;
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
                    services.AddTransient<IHouseService, HouseService>();
                    services.AddTransient<IMortgageApplcationService, MortgageApplicationService>();

                    // repos
                    services.AddTransient<IHouseRepository, HouseRepository>();
                    services.AddTransient<IMortgageApplicationRepository, MortgageApplicationRepository>();


                    //DBContexts
                    services.AddDbContext<HouseContext>();
                    services.AddDbContext<MortgageApplicationContext>();


                    // cosmosdb setup
                    //services.AddSingleton<ICosmosDbService<Message>>(CosmosDbSetup<Message>
                    //   .InitializeMessageCosmosClientInstanceAsync("MessageContainer")
                    //  .GetAwaiter().GetResult());
                })
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}