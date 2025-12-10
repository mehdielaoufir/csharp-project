using System.Net.Http.Json;

namespace Maui.Thera.Services
{
    public class WebRequestHandler
    {
        private readonly HttpClient _client;

        public WebRequestHandler(string baseUrl)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            return await _client.GetFromJsonAsync<T>(endpoint);
        }

        public async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            var response = await _client.PostAsJsonAsync(endpoint, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> PutAsync(string endpoint, object data)
        {
            var response = await _client.PutAsJsonAsync(endpoint, data);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _client.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
    }
}
