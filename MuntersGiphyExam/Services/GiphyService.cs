using MuntersGiphyExam.Services.Interfaces;
using System.Collections.Concurrent;

namespace MuntersGiphyExam.Services
{
    public class GiphyService : IGiphyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ConcurrentDictionary<string, string> _cache;

        public GiphyService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _cache = new ConcurrentDictionary<string, string>();
        }

        public async Task<string> GetTrendingGifs()
        {
            if (_cache.ContainsKey("trending"))
            {
                return _cache["trending"];
            }

            try
            {
                string apiUrl = $"https://api.giphy.com/v1/gifs/trending?api_key={_apiKey}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    _cache["trending"] = json;
                    return json;
                }
                else
                {
                    throw new Exception($"Failed to retrieve trending GIFs. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving trending GIFs.", ex);
            }
        }

        public async Task<string> SearchGifs(string query)
        {
            if (_cache.ContainsKey(query))
            {
                return _cache[query];
            }

            try
            {
                string apiUrl = $"https://api.giphy.com/v1/gifs/search?api_key={_apiKey}&q={query}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    _cache[query] = json;
                    return json;
                }
                else
                {
                    throw new Exception($"Failed to search GIFs. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while searching GIFs.", ex);
            }
        }
    }
}
