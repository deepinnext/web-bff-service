using Deepin.Application.Pagination;

namespace Deepin.WebBff.API.ApiClients;

public interface IChatApiClient
{
    Task<ChatDto> GetChatAsync(Guid chatId);
    Task<IEnumerable<ChatDto>> GetChatsAsync();
    Task<IPagination<ChatMemberDto>> GetChatMembersAsync(Guid chatId, int offset, int limit);
    Task<IEnumerable<ChatReadStatusDto>> GetChatReadStatusesAsync();
    Task<ChatReadStatusDto> GetChatReadStatusAsync(Guid chatId);
}
