using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lazcat.Blog.Models.Dtos.Messages;
using Lazcat.Blog.Models.Web;
using Lazcat.Blog.Web.Provider.Messages;

namespace Lazcat.Blog.Web.Services.Messages
{
    public class MessageService
    {
        private readonly IMessageProvider _messageProvider;

        public MessageService(IMessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        //todo make a tree to do this
        public async Task<IEnumerable<MessageDto>> GetMessages(int articleId)
        {
            return default;
            var response = await _messageProvider.GetMessages(articleId);
            var messages = response.Entity;
            if (messages == null) return new List<MessageDto>();
            var noneReplyMessages = messages.Where(x => !x.ReplyId.HasValue);
        }

        public async Task<StandardOutput<MessageDto>> UpdateMessage(CreateUpdateMessageInput input)
        {
            var responseMessage = await _messageProvider.UpdateMessage(input);
            return responseMessage.StateCode != Setting.StateCode.OK
                ? new StandardOutput<MessageDto>
                {
                    Message = $"Update Message failed."
                }
                : new StandardOutput<MessageDto>{Entity = responseMessage.Entity, Message = "Update Success"};
        }

        public async Task<StandardOutput<MessageDto>> CreateMessage(CreateUpdateMessageInput input)
        {
            var responseMessage = await _messageProvider.CreateMessage(input);
            return responseMessage.StateCode != Setting.StateCode.OK
                ? new StandardOutput<MessageDto>
                {
                    Message = $"Create Message failed."
                }
                : new StandardOutput<MessageDto> { Entity = responseMessage.Entity, Message = "Create Success" };
        }

        public async Task<StandardOutput<MessageDto>> DeleteMessage(Guid id)
        {
            var responseMessage = await _messageProvider.DeleteMessage(id);
            return responseMessage.StateCode != Setting.StateCode.OK
                ? new StandardOutput<MessageDto>
                {
                    Message = $"Delete Message failed."
                }
                : new StandardOutput<MessageDto> { Entity = responseMessage.Entity, Message = "Delete Success" };
        }
    }
}