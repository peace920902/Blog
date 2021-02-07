using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Messages;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Provider.Messages
{
    public interface IMessageProvider
    {
        Task<ResponseMessage<IEnumerable<MessageDto>>> GetMessages(int articleId);
        Task<ResponseMessage<MessageDto>> CreateMessage(CreateUpdateMessageInput input);
        Task<ResponseMessage<MessageDto>> UpdateMessage(CreateUpdateMessageInput input);
        Task<ResponseMessage<MessageDto>> DeleteMessage(Guid id);
    }
}