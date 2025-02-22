using Deepin.Infrastructure.Pagination;
using Deepin.WebBff.API.Configurations;
using Microsoft.Extensions.Options;

namespace Deepin.WebBff.API.ApiClients;

public class ChatApiClient(ILogger<ChatApiClient> logger, IOptions<UrlsConfig> options, HttpClient httpClient)
: HttpClientBase(logger, httpClient), IChatApiClient
{
    private readonly UrlsConfig _urlsConfig = options.Value;
    protected override string BaseUrl => _urlsConfig.Chat;
    public async Task<ChatDto> GetChatAsync(Guid chatId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/chats/{chatId}");
        var response = await SendAsync<ChatDto>(request);
        return response;
    }
    public async Task<IEnumerable<ChatDto>> GetChatsAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/chats");
        var response = await SendAsync<IEnumerable<ChatDto>>(request);
        return response;
    }
    public async Task<IPagination<ChatMemberDto>> GetChatMembersAsync(Guid chatId, int offset, int limit)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/chats/{chatId}/members?offset={offset}&limit={limit}");
        var response = await SendAsync<Pagination<ChatMemberDto>>(request);
        return response;
    }
    public async Task<IEnumerable<ChatReadStatusDto>> GetChatReadStatusesAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/chats/read-statuses");
        var response = await SendAsync<IEnumerable<ChatReadStatusDto>>(request);
        return response;
    }

    public async Task<ChatReadStatusDto> GetChatReadStatusAsync(Guid chatId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/chats/{chatId}/read-status");
        var response = await SendAsync<ChatReadStatusDto>(request);
        return response;
    }
}
public enum ChatType
{
    Direct,
    Group,
    Channel
}
public enum ChatMemberRole
{
    Owner,
    Admin,
    Member,
    Guest,
    Banned,
    Muted
}
public class ChatDto
{
    public Guid Id { get; set; }
    public ChatType Type { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public ChatInfo GroupInfo { get; set; }
    public string LastMessageId { get; set; }
}
public class ChatInfo
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Description { get; set; }
    public string AvatarFileId { get; set; }
    public bool IsPublic { get; set; }
}
public class ChatMemberDto
{
    public string UserId { get; set; }
    public string DisplayName { get; set; }
    public DateTime JoinedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ChatMemberRole Role { get; set; }
}

public class ChatReadStatusDto
{
    public Guid ChatId { get; set; }
    public string LastReadMessageId { get; set; }
    public DateTime LastReadAt { get; set; }
}