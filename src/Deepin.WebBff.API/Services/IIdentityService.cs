using Deepin.WebBff.API.Models.Identity;

namespace Deepin.WebBff.API.Services;

public interface IIdentityService
{
    Task<UserProfile> GetUserAsync(string userId);
    Task<IEnumerable<UserProfile>> GetUsersAsync(string[] userIds);
}
