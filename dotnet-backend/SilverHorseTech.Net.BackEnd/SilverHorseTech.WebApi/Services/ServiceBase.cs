using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SilverHorseTech.WebApi.Services
{
    public class ServiceBase<T> where T : new()
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _urlBase = ConfigurationManager.AppSettings["jsonplaceholderurl"];

        public async Task<T> GetAsync(string path)
        {
            string responseBody = await _httpClient.GetStringAsync(_urlBase + path);
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public async Task<List<T>> GetListAsync(string path)
        {
            string responseBody = await _httpClient.GetStringAsync(_urlBase + path);
            return JsonConvert.DeserializeObject<List<T>>(responseBody);
        }

        public async Task<T> PostJsonAsync(string path, T value)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<T>(_urlBase + path, value);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public async Task<T> PutJsonAsync(string path, T value)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(_urlBase + path, value);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(_urlBase + path);
            response.EnsureSuccessStatusCode();
            return response;
        }

    }
}