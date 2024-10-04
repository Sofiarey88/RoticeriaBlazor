using RoticeriaBlazor.Modelos;
using System.Text.Json;

namespace RoticeriaBlazor.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;

        public ProductoService(HttpClient client)
        {
            this.client = client;
            options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Producto>?> Get()
        {
            var response = await client.GetAsync("https://roticeriaback.azurewebsites.net/api/productos"); // Actualiza aquí
            response.EnsureSuccessStatusCode(); // Asegúrate de que la respuesta sea exitosa
            return await JsonSerializer.DeserializeAsync<List<Producto>>(await response.Content.ReadAsStreamAsync(), options);
        }

        public async Task<Producto?> GetById(int id)
        {
            var response = await client.GetAsync($"https://roticeriaback.azurewebsites.net/api/productos/{id}"); // Actualiza aquí
            response.EnsureSuccessStatusCode(); // Asegúrate de que la respuesta sea exitosa
            return await JsonSerializer.DeserializeAsync<Producto>(await response.Content.ReadAsStreamAsync(), options);
        }

        public async Task Add(Producto producto)
        {
            var content = new StringContent(JsonSerializer.Serialize(producto), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://roticeriaback.azurewebsites.net/api/productos", content); // Actualiza aquí
            response.EnsureSuccessStatusCode(); // Asegúrate de que la respuesta sea exitosa
        }

        public async Task Update(Producto producto)
        {
            var content = new StringContent(JsonSerializer.Serialize(producto), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://roticeriaback.azurewebsites.net/api/productos/{producto.Id}", content); // Actualiza aquí
            response.EnsureSuccessStatusCode(); // Asegúrate de que la respuesta sea exitosa
        }

        public async Task Delete(int id)
        {
            var response = await client.DeleteAsync($"https://roticeriaback.azurewebsites.net/api/productos/{id}"); // Actualiza aquí
            response.EnsureSuccessStatusCode(); // Asegúrate de que la respuesta sea exitosa
        }
    }

    public interface IProductoService
    {
        Task<List<Producto>?> Get();
        Task<Producto?> GetById(int id);
        Task Add(Producto producto);
        Task Update(Producto producto);
        Task Delete(int id);
    }
}
