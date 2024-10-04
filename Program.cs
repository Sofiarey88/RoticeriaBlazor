using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RoticeriaBlazor;
using RoticeriaBlazor.Interfaces;
using RoticeriaBlazor.Modelos;
using RoticeriaBlazor.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
        var urlApi = builder.Configuration.GetValue<string>("urlApi");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(urlApi) });
        builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        builder.Services.AddScoped<IClienteService, ClientesService>();
        builder.Services.AddScoped<IProductoService, ProductoService>();
        builder.Services.AddScoped<GenericService<Proveedor>>();
        builder.Services.AddScoped<IPedidoService, PedidosService>();






       


        builder.Services.AddSweetAlert2();
        await builder.Build().RunAsync();
    }
}