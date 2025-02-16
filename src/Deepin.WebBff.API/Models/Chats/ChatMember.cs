using System;
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
}