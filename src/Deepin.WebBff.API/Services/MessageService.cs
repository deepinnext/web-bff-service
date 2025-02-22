using Deepin.Infrastructure.Pagination;
using Deepin.WebBff.API.ApiClients;
using Deepin.WebBff.API.Models.Messages;

namespace Deepin.WebBff.API.Services;

public class MessageService(
    IMessageApiClient messageApiClient,
    IIdentityService identityService) : IMessageService
{
    private readonly IMessageApiClient _messageApiClient = messageApiClient;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Message> GetLastMessageAsync(Guid chatId)
    {
        if (chatId == Guid.Empty)
        {
            return null;
        }
        var messageDto = await _messageApiClient.GetLastMessageAsync(chatId);
        if (messageDto == null)
        {
            return null;
        }
        var fromUser = await _identityService.GetUserAsync(messageDto.From);
        return new Message(messageDto, fromUser);
    }

    public async Task<Message> GetMessageAsync(string messageId)
    {
        if (string.IsNullOrEmpty(messageId))
        {
            return null;
        }
        var messageDto = await _messageApiClient.GetMessageByIdAsync(messageId);
        if (messageDto == null)
        {
            return null;
        }
        var fromUser = await _identityService.GetUserAsync(messageDto.From);
        return new Message(messageDto, fromUser);
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(string[] messageIds)
    {
        if (messageIds == null || messageIds.Length == 0)
        {
            return [];
        }
        var messages = await _messageApiClient.GetMessagesByIdsAsync(messageIds);
        if (messages == null || messages.Count() == 0)
        {
            return [];
        }
        var fromUsers = await _identityService.GetUsersAsync(messages.Select(m => m.From).Distinct().ToArray());
        return messages.Select(m =>
        {
            var fromUser = fromUsers.FirstOrDefault(u => u.Id == m.From);
            return new Message(m, fromUser);
        });
    }

    public async Task<IPagination<Message>> GetMessagesAsync(Guid chatId, MessageQuery query)
    {
        var messages = await _messageApiClient.GetMessagesAsync(query.ToDto(chatId));
        if (messages == null || messages.Items.Count() == 0)
        {
            return new Pagination<Message>([], 0, 0, 0);
        }
        var fromUsers = await _identityService.GetUsersAsync(messages.Items.Select(m => m.From).Distinct().ToArray());
        return new Pagination<Message>(
            messages.Items.Select(m =>
            {
                var fromUser = fromUsers.FirstOrDefault(u => u.Id == m.From);
                return new Message(m, fromUser);
            }),
            messages.Offset,
            messages.Limit,
            messages.TotalCount
        );
    }
}
