using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Foundation.Core
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> Json<T>(this HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<ErrorResponse> Error(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return new ErrorResponse();
            return await response.Json<ErrorResponse>();
        }

        public static T Json<T>(this object json)
        {
            return JsonConvert.DeserializeObject<T>(json.ToString());
        }
    }

    public class ErrorResponse
    {
        public string error { get; set; }
        public string exception { get; set; }
    }

    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostJsonAsync(this HttpClient client, string url, object data,
            string token = null)
        {
            if (!token.IsNullOrWhiteSpace())
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }
            return client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
        }

        public static Task<HttpResponseMessage> GetJsonAsync(this HttpClient client, string url, string token = null)
        {
            if (!token.IsNullOrWhiteSpace())
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }
            return client.GetAsync(url);
        }
    }
}