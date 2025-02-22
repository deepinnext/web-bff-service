using Deepin.WebBff.API.Configurations;
using Microsoft.Extensions.Options;

namespace Deepin.WebBff.API.ApiClients;

public class PresenceApiClient(ILogger<PresenceApiClient> logger, IOptions<UrlsConfig> options, HttpClient httpClient)
: HttpClientBase(logger, httpClient), IPresenceApiClient
{
    private readonly UrlsConfig _urlsConfig = options.Value;
    protected override string BaseUrl => _urlsConfig.Presence;
    public async Task<UserPresenceDto> GetPresenceAsync(string userId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/presences/{userId}");
        var response = await SendAsync<UserPresenceDto>(request);
        return response;
    }
    public async Task<IEnumerable<UserPresenceDto>> GetPresencesAsync(string[] userIds)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/presences/batch?ids={string.Join(",", userIds)}");
        var response = await SendAsync<IEnumerable<UserPresenceDto>>(request);
        return response;
    }
}
public class UserPresenceDto
{
    public string UserId { get; set; }
    public PresenceStatus Status { get; set; }
    public DateTime LastOnlineAt { get; set; }
    public string CustomStatus { get; set; }
    public DateTime? CustomStatusExpiresAt { get; set; }
}
public enum PresenceStatus
{
    Online,
    Offline,
    Away,
    Busy,
    DoNotDisturb,
    Custom
}
