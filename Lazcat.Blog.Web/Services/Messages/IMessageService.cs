using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Messages;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Services.Messages
{
    public interface IMessageService
    {
        Task<Dictionary<MessageDto, List<MessageDto>>> GetMessages(int articleId);
        Task<StandardOutput<MessageDto>> UpdateMessage(CreateUpdateMessageInput input);
        Task<StandardOutput<MessageDto>> CreateMessage(CreateUpdateMessageInput input);
        Task<StandardOutput<MessageDto>> DeleteMessage(Guid id);
        Dictionary<MessageDto, List<MessageDto>> GetMessages(IEnumerable<MessageDto> messages);
    }
}