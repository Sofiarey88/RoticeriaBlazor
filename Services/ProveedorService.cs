using System.Net.Http.Json;
using RoticeriaBlazor.Modelos;

namespace RoticeriaBlazor.Services
{
    public interface IProveedorService
    {
        Task<List<Proveedor>> Get();
        Task<Proveedor?> GetById(int id);
        Task<Proveedor> Create(Proveedor proveedor);
        Task Update(Proveedor proveedor);
        Task Delete(int id);
    }

    public class ProveedorService : IProveedorService
    {
        private readonly HttpClient _httpClient;

        public ProveedorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Proveedor>> Get()
        {
            return await _httpClient.GetFromJsonAsync<List<Proveedor>>("api/proveedores");
        }

        public async Task<Proveedor?> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Proveedor>($"api/proveedores/{id}");
        }

        public async Task<Proveedor> Create(Proveedor proveedor)
        {
            var response = await _httpClient.PostAsJsonAsync("api/proveedores", proveedor);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Proveedor>();
        }

        public async Task Update(Proveedor proveedor)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/proveedores/{proveedor.Id}", proveedor);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/proveedores/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

