using Deepin.WebBff.API.ApiClients;

namespace Deepin.WebBff.API.Models.Identity;

public class UserProfile
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string AvatarFileId { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserProfile(UserProfileDto dto)
    {
        Id = dto.Id;
        UserName = dto.UserName;
        DisplayName = dto.DisplayName;
        Email = dto.Email;
        AvatarFileId = dto.AvatarFileId;
        CreatedAt = dto.CreatedAt;
    }
}
