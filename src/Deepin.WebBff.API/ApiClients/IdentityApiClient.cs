using Deepin.WebBff.API.Configurations;
using Microsoft.Extensions.Options;

namespace Deepin.WebBff.API.ApiClients;

public class IdentityApiClient(ILogger<IdentityApiClient> logger, IOptions<UrlsConfig> options, HttpClient httpClient)
: HttpClientBase(logger, httpClient), IIdentityApiClient
{
    private readonly UrlsConfig _urlsConfig = options.Value;
    protected override string BaseUrl => _urlsConfig.Identity;

    public async Task<UserProfileDto> GetUserAsync(string userId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/users/{userId}");
        var response = await SendAsync<UserProfileDto>(request);
        return response;
    }

    public async Task<IEnumerable<UserProfileDto>> GetUsersAsync(string[] userIds)
    {
        var queryStrings = string.Join("&", userIds.Select(x => $"ids={x}"));
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/users/batch?{queryStrings}");
        var response = await SendAsync<IEnumerable<UserProfileDto>>(request);
        return response;
    }
}
public class UserProfileDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string AvatarFileId { get; set; }
    public DateTime CreatedAt { get; set; }
}