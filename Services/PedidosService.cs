using RoticeriaBlazor.Modelos;
using System.Text.Json;

namespace RoticeriaBlazor.Services
{
    public class PedidosService : IPedidoService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;

        public PedidosService(HttpClient client)
        {
            this.client = client;
            options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Pedido>?> Get()
        {
            var response = await client.GetAsync("https://roticeriaback.azurewebsites.net/api/pedidos"); // URL completa
            response.EnsureSuccessStatusCode(); // Asegúrate de manejar los errores de respuesta
            return await JsonSerializer.DeserializeAsync<List<Pedido>>(await response.Content.ReadAsStreamAsync(), options);
        }

        public async Task<Pedido?> GetById(int id)
        {
            var response = await client.GetAsync($"https://roticeriaback.azurewebsites.net/api/pedidos/{id}"); // URL completa
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<Pedido>(await response.Content.ReadAsStreamAsync(), options);
        }

        public async Task Add(Pedido pedido)
        {
            var content = new StringContent(JsonSerializer.Serialize(pedido), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://roticeriaback.azurewebsites.net/api/pedidos", content); // URL completa
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(Pedido pedido)
        {
            var content = new StringContent(JsonSerializer.Serialize(pedido), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://roticeriaback.azurewebsites.net/api/pedidos/{pedido.Id}", content); // URL completa
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var response = await client.DeleteAsync($"https://roticeriaback.azurewebsites.net/api/pedidos/{id}"); // URL completa
            response.EnsureSuccessStatusCode();
        }
    }

    public interface IPedidoService
    {
        Task<List<Pedido>?> Get();
        Task<Pedido?> GetById(int id);
        Task Add(Pedido pedido);
        Task Update(Pedido pedido);
        Task Delete(int id);
    }
}
