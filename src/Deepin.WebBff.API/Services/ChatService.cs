using Deepin.Domain;
using Deepin.Infrastructure.Helpers;
using Deepin.WebBff.API.ApiClients;
using Deepin.WebBff.API.Models.Chats;
using Deepin.WebBff.API.Models.Messages;

namespace Deepin.WebBff.API.Services;

public class ChatService(
    IChatApiClient chatApiClient,
    IMessageService messageService,
    IIdentityService identityService,
    IUserContext userContext) : IChatService
{
    private readonly IChatApiClient _chatApiClient = chatApiClient;
    private readonly IMessageService _messageService = messageService;
    private readonly IIdentityService _identityService = identityService;
    private readonly IUserContext _userContext = userContext;
    public async Task<Chat> GetChatAsync(Guid chatId)
    {
        var chat = await _chatApiClient.GetChatAsync(chatId);
        if (chat == null)
        {
            return null;
        }
        var readStatus = await _chatApiClient.GetChatReadStatusAsync(chatId);
        var lastReadMessage = readStatus is null ? null : await _messageService.GetMessageAsync(readStatus.LastReadMessageId);
        return await PopulateChatDataAsync(chat, lastReadMessage);
    }

    public async Task<IEnumerable<Chat>> GetChatsAsync(CancellationToken stoppingToken = default)
    {
        var chats = await _chatApiClient.GetChatsAsync();
        var chatReadStatuses = await _chatApiClient.GetChatReadStatusesAsync();
        var messageIds = chatReadStatuses?.Select(x => x.LastReadMessageId).Where(x => !string.IsNullOrEmpty(x));
        var messages = messageIds?.Any() == true ? await _messageService.GetMessagesAsync(messageIds.ToArray()) : [];
        using var parallelTaskExecutor = new ParallelTaskExecutor<Chat>();
        var tasks = new List<Task<Chat>>();
        foreach (var chat in chats)
        {
            var readStatus = chatReadStatuses.FirstOrDefault(x => x.ChatId == chat.Id);
            var lastReadMessage = readStatus is null ? null : messages.FirstOrDefault(x => x.Id == readStatus.LastReadMessageId);
            tasks.Add(parallelTaskExecutor.EnqueueAsync(async (cancellationToken) =>
            {
                var result = await PopulateChatDataAsync(chat, lastReadMessage, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                return result;
            }, stoppingToken));
        }
        return await Task.WhenAll(tasks);
    }
    private async Task<Chat> PopulateChatDataAsync(ChatDto dto, Message lastReadMessage = null, CancellationToken stoppingToken = default)
    {
        var lastMessage = await _messageService.GetLastMessageAsync(dto.Id);
        var chat = new Chat(dto, lastMessage, lastReadMessage);
        if (chat.Type == ChatType.Direct)
        {
            var pagedMembers = await _chatApiClient.GetChatMembersAsync(dto.Id, 0, 2);
            var otherUser = pagedMembers.Items.FirstOrDefault(x => x.UserId != _userContext.UserId);
            var userProfile = await _identityService.GetUserAsync(otherUser.UserId);
            chat.UserName = userProfile.UserName;
            chat.AvatarFileId = userProfile.AvatarFileId;
            chat.Name = userProfile.DisplayName;
        }
        return chat;
    }
}
