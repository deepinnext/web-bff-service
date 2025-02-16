using Deepin.WebBff.API.ApiClients;
using Deepin.WebBff.API.Models.Identity;

namespace Deepin.WebBff.API.Models.Messages;

public class Message
{
    public string Id { get; set; }
    public Guid ChatId { get; set; }
    public long CreatedAt { get; set; }
    public long ModifiedAt { get; set; }
    public string Content { get; set; }
    public string ReplyTo { get; set; }
    public long Sequence { get; set; }
    public UserProfile From { get; set; }
    public Message(MessageDto message, UserProfile from)
    {
        Id = message.Id;
        ChatId = message.ChatId;
        CreatedAt = message.CreatedAt;
        ModifiedAt = message.ModifiedAt;
        Content = message.Content;
        ReplyTo = message.ReplyTo;
        Sequence = message.Sequence;
        From = from;
    }
}
