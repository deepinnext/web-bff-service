using Deepin.WebBff.API.ApiClients;
using Deepin.WebBff.API.Models.Identity;

namespace Deepin.WebBff.API.Models.Chats;

public class ChatMember
{
    public string DisplayName { get; set; }
    public DateTime JoinedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ChatMemberRole Role { get; set; }
    public string UserId { get; set; }
    public UserProfile Profile { get; set; }
    public ChatMember() { }
    public ChatMember(ChatMemberDto dto, UserProfile profile)
    {
        DisplayName = dto.DisplayName;
        JoinedAt = dto.JoinedAt;
        UpdatedAt = dto.UpdatedAt;
        Role = dto.Role;
        UserId = dto.UserId;
        Profile = profile;
    }
}