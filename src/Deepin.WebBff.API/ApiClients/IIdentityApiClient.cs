namespace Deepin.WebBff.API.ApiClients;

public interface IIdentityApiClient
{
    Task<UserProfileDto> GetUserAsync(string userId);
    Task<IEnumerable<UserProfileDto>> GetUsersAsync(string[] userIds);
}
