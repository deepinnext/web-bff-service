using Deepin.Application.Pagination;
using Deepin.WebBff.API.Models.Messages;

namespace Deepin.WebBff.API.Services;

public interface IMessageService
{
    Task<Message> GetMessageAsync(string messageId);
    Task<Message> GetLastMessageAsync(Guid chatId);
    Task<IEnumerable<Message>> GetMessagesAsync(string[] messageIds);
    Task<IPagination<Message>> GetMessagesAsync(Guid chatId, MessageQuery query);

}
