using Deepin.Application.Pagination;

namespace Deepin.WebBff.API.ApiClients;

public interface IMessageApiClient
{
    Task<MessageDto> GetMessageByIdAsync(string id);
    Task<MessageDto> GetLastMessageAsync(Guid chatId);
    Task<IEnumerable<MessageDto>> GetMessagesByIdsAsync(string[] ids);
    Task<IPagination<MessageDto>> GetMessagesAsync(MessageQueryDto query);
}