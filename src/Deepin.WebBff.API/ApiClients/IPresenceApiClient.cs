namespace Deepin.WebBff.API.ApiClients;

public interface IPresenceApiClient
{
    Task<UserPresenceDto> GetPresenceAsync(string userId);
    Task<IEnumerable<UserPresenceDto>> GetPresencesAsync(string[] userIds);
}
