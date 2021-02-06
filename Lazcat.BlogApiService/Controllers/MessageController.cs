using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.ApplicationService.Messages;
using Lazcat.Blog.Models.Dtos.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Lazcat.BlogApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageAppService _messageAppService;

        public MessageController(IMessageAppService messageAppService)
        {
            _messageAppService = messageAppService;
        }

        [HttpGet, Route("{articleId}")]
        public async Task<IEnumerable<MessageDto>> GetMessages(int articleId)
        {
            return await _messageAppService.GetMessages(articleId);
        }
        
        [HttpPost]
        public async Task<MessageDto> CreateMessage(CreateUpdateMessageInput input)
        {
            return await _messageAppService.CreateMessage(input);
        }
        
        [HttpPut]
        public async Task<MessageDto> UpdateMessage(CreateUpdateMessageInput input)
        {
            return await _messageAppService.UpdateMessage(input);
        }
        
        [HttpDelete,Route("{Id}")]
        public async Task<MessageDto> DeleteMessage(Guid id)
        {
            return await _messageAppService.DeleteMessage(id);  
        }
    }
}