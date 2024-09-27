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
            var response = await client.GetAsync("apipedidos");
            return await JsonSerializer.DeserializeAsync<List<Pedido>>(await response.Content.ReadAsStreamAsync(), options);
        }

        public async Task<Pedido?> GetById(int id)
        {
            var response = await client.GetAsync($"apipedidos/{id}");
            return await JsonSerializer.DeserializeAsync<Pedido>(await response.Content.ReadAsStreamAsync(), options);
        }

        public async Task Add(Pedido pedido)
        {
            var content = new StringContent(JsonSerializer.Serialize(pedido), System.Text.Encoding.UTF8, "application/json");
            await client.PostAsync("apipedidos", content);
        }

        public async Task Update(Pedido pedido)
        {
            var content = new StringContent(JsonSerializer.Serialize(pedido), System.Text.Encoding.UTF8, "application/json");
            await client.PutAsync($"apipedidos/{pedido.Id}", content);
        }

        public async Task Delete(int id)
        {
            await client.DeleteAsync($"apipedidos/{id}");
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

