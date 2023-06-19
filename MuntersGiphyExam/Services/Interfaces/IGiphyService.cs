using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MuntersGiphyExam.Services.Interfaces
{
    public interface IGiphyService
    {
        Task<string> SearchGifs(string query);

        Task<string> GetTrendingGifs();
    }
}
