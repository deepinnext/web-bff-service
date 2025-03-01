using Deepin.Infrastructure.Pagination;
using Deepin.WebBff.API.Configurations;
using Microsoft.Extensions.Options;

namespace Deepin.WebBff.API.ApiClients;

public class MessageApiClient(ILogger<MessageApiClient> logger, IOptions<UrlsConfig> options, HttpClient httpClient)
: HttpClientBase(logger, httpClient), IMessageApiClient
{
    private readonly UrlsConfig _urlsConfig = options.Value;
    protected override string BaseUrl => _urlsConfig.Message;
    public async Task<MessageDto> GetMessageByIdAsync(string id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/messages/{id}");
        var response = await SendAsync<MessageDto>(request);
        return response;
    }
    public async Task<IEnumerable<MessageDto>> GetMessagesByIdsAsync(string[] ids)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/messages/batch?ids={string.Join(",", ids)}");
        var response = await SendAsync<IEnumerable<MessageDto>>(request);
        return response;
    }
    public async Task<IPagination<MessageDto>> GetMessagesAsync(MessageQueryDto query)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/messages?{query.ToQueryString()}");
        var response = await SendAsync<IPagination<MessageDto>>(request);
        return response;
    }

    public async Task<MessageDto> GetLastMessageAsync(Guid chatId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/messages/last?chatId={chatId}");
        var response = await SendAsync<MessageDto>(request);
        return response;
    }
}

public class MessageDto
{
    public string Id { get; set; }
    public string From { get; set; }
    public bool IsEdited { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public long Sequence { get; set; }
    public Guid ChatId { get; set; }
    public string Content { get; set; }
    public string ReplyTo { get; set; }
}
public enum MessageQueryDirection
{
    Backward,
    Forward
}
public class MessageQueryDto : PageQuery
{
    public Guid ChatId { get; set; }
    public string Keywords { get; set; }
    public string From { get; set; }
    public long AnchorSequence { get; set; }
    public MessageQueryDirection? Direction { get; set; }
    internal string ToQueryString()
    {
        var query = new List<string>
        {
            $"limit={Limit}",
            $"offset={Offset}",
            $"chatId={ChatId}"
        };
        if (!string.IsNullOrWhiteSpace(Keywords))
        {
            query.Add($"keywords={Keywords}");
        }
        if (!string.IsNullOrWhiteSpace(From))
        {
            query.Add($"from={From}");
        }
        if (AnchorSequence > 0)
        {
            query.Add($"anchorSequence={AnchorSequence}");
        }
        if (Direction.HasValue)
        {
            query.Add($"direction={Direction.Value.ToString().ToLower()}");
        }

        return string.Join("&", query);
    }
}