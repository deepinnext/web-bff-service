using Deepin.Infrastructure.Pagination;
using Deepin.WebBff.API.Models.Chats;
using Deepin.WebBff.API.Models.Messages;
using Deepin.WebBff.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deepin.WebBff.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatsController(IChatService chatService, IMessageService messageService) : ControllerBase
    {
        private readonly IChatService _chatService = chatService;
        private readonly IMessageService _messageService = messageService;

        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> Get(Guid id)
        {
            var chat = await _chatService.GetChatAsync(id);
            return Ok(chat);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
        {
            var chats = await _chatService.GetChatsAsync();
            return Ok(chats);
        }
        [HttpGet("{chatId}/messages/{messageId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessage(Guid chatId, string messageId)
        {
            var chat = await _chatService.GetChatAsync(chatId);
            if (chat == null)
            {
                return NotFound();
            }
            var message = await _messageService.GetMessageAsync(messageId);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }
        [HttpGet("{chatId}/messages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(Guid chatId, [FromQuery] MessageQuery query)
        {
            var chat = await _chatService.GetChatAsync(chatId);
            if (chat == null)
            {
                return NotFound();
            }
            var messages = await _messageService.GetMessagesAsync(chatId, query);
            return Ok(messages);
        }
        [HttpGet("{chatId}/members")]
        public async Task<ActionResult<IPagination<ChatMember>>> GetMembers(Guid chatId, int offset = 0, int limit = 20)
        {
            var chat = await _chatService.GetChatAsync(chatId);
            if (chat == null)
            {
                return NotFound();
            }
            var members = await _chatService.GetChatMemberAsync(chatId, offset, limit);
            return Ok(members);
        }
    }
}
