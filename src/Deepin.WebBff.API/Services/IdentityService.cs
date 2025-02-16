using Deepin.WebBff.API.ApiClients;
using Deepin.WebBff.API.Models.Identity;

namespace Deepin.WebBff.API.Services;

public class IdentityService(IIdentityApiClient identityApiClient) : IIdentityService
{
    private readonly IIdentityApiClient _identityApiClient = identityApiClient;
    public async Task<UserProfile> GetUserAsync(string userId)
    {
        var user = await _identityApiClient.GetUserAsync(userId);
        return new UserProfile(user);
    }

    public async Task<IEnumerable<UserProfile>> GetUsersAsync(string[] userIds)
    {
        var users = await _identityApiClient.GetUsersAsync(userIds);
        return users.Select(user => new UserProfile(user));
    }
}
