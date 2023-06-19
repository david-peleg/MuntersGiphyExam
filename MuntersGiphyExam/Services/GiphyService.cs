using MuntersGiphyExam.Services.Interfaces;

namespace MuntersGiphyExam.Services
{
    public class GiphyService : IGiphyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly Dictionary<string, string> _cache;
        private string _cachedTrendingGifs;

        public GiphyService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _cache = new Dictionary<string, string>();
            _cachedTrendingGifs = string.Empty;
        }

        public async Task<string> GetTrendingGifs()
        {
            if (!string.IsNullOrEmpty(_cachedTrendingGifs))
            {
                return _cachedTrendingGifs;
            }

            try
            {
                string apiUrl = $"https://api.giphy.com/v1/gifs/trending?api_key={_apiKey}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    _cachedTrendingGifs = json;
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
