using Deepin.Infrastructure.Pagination;
using Deepin.WebBff.API.ApiClients;

namespace Deepin.WebBff.API.Models.Messages;

public class MessageQuery : PageQuery
{
    public string Keywords { get; set; }
    public string From { get; set; }
    public MessageQueryDto ToDto(Guid chatId)
    {
        return new MessageQueryDto
        {
            ChatId = chatId,
            Keywords = Keywords,
            From = From,
            Limit = Limit,
            Offset = Offset
        };
    }
}