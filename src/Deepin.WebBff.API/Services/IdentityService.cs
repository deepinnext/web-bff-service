using Deepin.WebBff.API.ApiClients;
using Deepin.WebBff.API.Models.Identity;

namespace Deepin.WebBff.API.Services;

public class IdentityService(
    IIdentityApiClient identityApiClient,
    IPresenceApiClient presenceApiClient) : IIdentityService
{
    private readonly IIdentityApiClient _identityApiClient = identityApiClient;
    private readonly IPresenceApiClient _presenceApiClient = presenceApiClient;
    public async Task<UserProfile> GetUserAsync(string userId)
    {
        var user = await _identityApiClient.GetUserAsync(userId);
        if (user == null)
        {
            return null;
        }
        var presence = await _presenceApiClient.GetPresenceAsync(userId);
        return new UserProfile(user, presence);
    }

    public async Task<IEnumerable<UserProfile>> GetUsersAsync(string[] userIds)
    {
        var users = await _identityApiClient.GetUsersAsync(userIds);
        var presences = await _presenceApiClient.GetPresencesAsync(userIds);
        return users.Select(user => new UserProfile(user, presences.FirstOrDefault(p => p.UserId == user.Id)));
    }
}
