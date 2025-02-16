using Newtonsoft.Json;

namespace Deepin.WebBff.API.ApiClients;


public abstract class HttpClientBase(
    ILogger logger,
    HttpClient httpClient)
{
    private readonly ILogger _logger = logger;
    private readonly HttpClient _httpClient = httpClient;
    protected abstract string BaseUrl { get; }

    protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        request.RequestUri = new Uri($"{BaseUrl.TrimEnd('/')}/{request.RequestUri.ToString().TrimStart('/')}");
        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Request to {request.RequestUri} failed with status code {response.StatusCode}");
            response.EnsureSuccessStatusCode();
        }
        return response;
    }
    protected async Task<T> SendAsync<T>(HttpRequestMessage request)
    {
        var response = await SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        if (typeof(T) == typeof(string))
        {
            return (T)(object)content;
        }

        return JsonConvert.DeserializeObject<T>(content);
    }
}
