using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Text;
using JsonLocalizer;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace ClientSideApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddLocalization(options=>
            {
                options.ResourcesPath = "Resources";
            });

            //var culture = new CultureInfo("en");
            //CultureInfo.DefaultThreadCurrentCulture = culture;
            //CultureInfo.DefaultThreadCurrentUICulture = culture;

            builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

            await builder.Build().RunAsync();
        }
    }
}
