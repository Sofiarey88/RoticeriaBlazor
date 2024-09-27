using RoticeriaBlazor.Class;
using RoticeriaBlazor.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace RoticeriaBlazor.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;
        private readonly string _endpoint;

        public GenericService(HttpClient client)
        {
            this.client = client;
            this.options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            this._endpoint = ApiEndpoints.GetEndpoint(typeof(T).Name);
        }

        public async Task<List<T>?> GetAllAsync()
        {
            var response = await client.GetAsync(_endpoint);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al obtener todos los elementos: {response.ReasonPhrase} - {content}");
            }

            return JsonSerializer.Deserialize<List<T>>(content, options);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var fullEndpoint = $"{_endpoint}/{id}";
            Console.WriteLine($"Consultando endpoint: {fullEndpoint}"); // Depuración
            var response = await client.GetAsync(fullEndpoint);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al obtener el elemento con ID {id}: {response.ReasonPhrase} - {content}");
            }

            return JsonSerializer.Deserialize<T>(content, options);
        }

        public async Task<T?> AddAsync(T? entity)
        {
            var response = await client.PostAsJsonAsync(_endpoint, entity);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al agregar el elemento: {response.ReasonPhrase} - {content}");
            }

            return JsonSerializer.Deserialize<T>(content, options);
        }

        public async Task UpdateAsync(T? entity)
        {
            var idValue = entity?.GetType().GetProperty("Id")?.GetValue(entity);

            if (idValue == null)
            {
                throw new ApplicationException("No se pudo encontrar la propiedad 'Id' en la entidad.");
            }

            var response = await client.PutAsJsonAsync($"{_endpoint}/{idValue}", entity);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al actualizar el elemento con ID {idValue}: {response.ReasonPhrase} - {content}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var response = await client.DeleteAsync($"{_endpoint}/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al eliminar el elemento con ID {id}: {response.ReasonPhrase} - {content}");
            }
        }
    }
}
