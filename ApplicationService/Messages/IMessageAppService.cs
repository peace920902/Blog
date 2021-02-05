using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Messages;

namespace Lazcat.Blog.ApplicationService.Messages
{
    public interface IMessageAppService
    {
        Task<IEnumerable<MessageDto>> GetMessages(int articleId);
        Task<MessageDto> CreateMessage(CreateUpdateMessageInput input);
        Task<MessageDto> UpdateMessage(CreateUpdateMessageInput input);
        Task<MessageDto> DeleteMessage(Guid messageId);
    }
}