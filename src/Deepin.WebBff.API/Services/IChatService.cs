using Deepin.Infrastructure.Pagination;
using Deepin.WebBff.API.Models.Chats;

namespace Deepin.WebBff.API.Services;

public interface IChatService
{
    Task<Chat> GetChatAsync(Guid chatId);
    Task<IEnumerable<Chat>> GetChatsAsync(CancellationToken stoppingToken = default);
    Task<IPagination<ChatMember>> GetChatMemberAsync(Guid chatId, int offset = 0, int limit = 20);
}
