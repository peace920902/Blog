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
        Task UpdateMessage(CreateUpdateMessageInput input);
        Task DeleteMessage(Guid messageId);
    }
}