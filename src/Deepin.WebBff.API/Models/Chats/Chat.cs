using Deepin.WebBff.API.ApiClients;
using Deepin.WebBff.API.Models.Messages;

namespace Deepin.WebBff.API.Models.Chats;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public ChatType Type { get; set; }
    public string UserName { get; set; }
    public string Description { get; set; }
    public string AvatarFileId { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Message LastMessage { get; set; }
    public Message LastReadMessage { get; set; }
    public Chat()
    {

    }
    public Chat(ChatDto dto, Message lastMessage = null, Message lastReadMessage = null)
    {
        Id = dto.Id;
        CreatedAt = dto.CreatedAt;
        UpdatedAt = dto.UpdatedAt;
        Type = dto.Type;
        LastMessage = lastMessage;
        LastReadMessage = lastReadMessage;
        if (Type != ChatType.Direct)
        {
            IsPublic = dto.GroupInfo.IsPublic;
            Name = dto.GroupInfo.Name;
            UserName = dto.GroupInfo.UserName;
            AvatarFileId = dto.GroupInfo.AvatarFileId;
            Description = dto.GroupInfo.Description;
        }
    }
}