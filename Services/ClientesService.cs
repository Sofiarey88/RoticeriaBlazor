using RoticeriaBlazor.Modelos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace RoticeriaBlazor.Services
{
    public class ClientesService : IClienteService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;

        public ClientesService(HttpClient client)
        {
            this.client = client;
            this.options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        // Obtener todos los clientes
        public async Task<List<Cliente>?> Get()
        {
            var response = await client.GetAsync("https://roticeriaback.azurewebsites.net/api/clientes");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content?.ToString());
            }
            return JsonSerializer.Deserialize<List<Cliente>>(content, options);
        }

        // Obtener un cliente por ID
        public async Task<Cliente?> Get(int idCliente)
        {
            var response = await client.GetAsync($"https://roticeriaback.azurewebsites.net/api/clientes/{idCliente}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content?.ToString());
            }
            return JsonSerializer.Deserialize<Cliente>(content, options);
        }

        // Agregar un nuevo cliente
        public async Task<Cliente?> Add(Cliente cliente)
        {
            var response = await client.PostAsJsonAsync("https://roticeriaback.azurewebsites.net/api/clientes", cliente);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content?.ToString());
            }
            return JsonSerializer.Deserialize<Cliente>(content, options);
        }

        // Actualizar un cliente existente
        public async Task Put(Cliente? cliente)
        {
            var response = await client.PutAsJsonAsync($"https://roticeriaback.azurewebsites.net/api/clientes/{cliente?.Id}", cliente);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response?.ToString());
            }
        }

        // Eliminar un cliente por ID
        public async Task Delete(int idCliente)
        {
            var response = await client.DeleteAsync($"https://roticeriaback.azurewebsites.net/api/clientes/{idCliente}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.ToString());
            }
        }
    }

    public interface IClienteService
    {
        Task<List<Cliente>?> Get();
        Task<Cliente?> Get(int idCliente);
        Task<Cliente?> Add(Cliente cliente);
        Task Put(Cliente cliente);
        Task Delete(int idCliente);
    }
}
