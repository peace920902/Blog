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
    public class MessageService : IMessageService
    {
        private readonly IMessageProvider _messageProvider;

        public MessageService(IMessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        //todo make a tree to do this
        public async Task<Dictionary<MessageDto, List<MessageDto>>> GetMessages(int articleId)
        {
            var response = await _messageProvider.GetMessages(articleId);
            if (response.Entity == null) return new();
            var messages = response.Entity.ToList();
            var msgDict = messages.Where(x => !x.ReplyId.HasValue).ToDictionary(x => x, _ => new List<MessageDto>());
            foreach (var replyMessage in messages.Where(x => x.ReplyId.HasValue))
            {
                foreach (var message in msgDict.Where(message => replyMessage.ReplyId == message.Key.Id))
                {
                    message.Value.Add(replyMessage);
                }
            }
            return msgDict;
        }

        public Dictionary<MessageDto, List<MessageDto>> GetMessages(IEnumerable<MessageDto> messages)
        {
            if (messages== null) return new();
            var messageDtos = messages as MessageDto[] ?? messages.ToArray();
            var msgDict = messageDtos.Where(x => !x.ReplyId.HasValue).ToDictionary(x => x, _ => new List<MessageDto>());
            foreach (var replyMessage in messageDtos.Where(x => x.ReplyId.HasValue))
            {
                foreach (var message in msgDict.Where(message => replyMessage.ReplyId == message.Key.Id))
                {
                    message.Value.Add(replyMessage);
                }
            }
            return msgDict;
        }

        public async Task<StandardOutput<MessageDto>> UpdateMessage(CreateUpdateMessageInput input)
        {
            var responseMessage = await _messageProvider.UpdateMessage(input);
            return responseMessage.StateCode != Define.StateCode.OK
                ? new StandardOutput<MessageDto>
                {
                    Message = "Update Message failed."
                }
                : new StandardOutput<MessageDto>{Entity = responseMessage.Entity, Message = "Update Success"};
        }

        public async Task<StandardOutput<MessageDto>> CreateMessage(CreateUpdateMessageInput input)
        {
            var responseMessage = await _messageProvider.CreateMessage(input);
            return responseMessage.StateCode != Define.StateCode.OK
                ? new StandardOutput<MessageDto>
                {
                    Message = "Create Message failed."
                }
                : new StandardOutput<MessageDto> { Entity = responseMessage.Entity, Message = "Create Success" };
        }

        public async Task<StandardOutput<MessageDto>> DeleteMessage(Guid id)
        {
            var responseMessage = await _messageProvider.DeleteMessage(id);
            return responseMessage.StateCode != Define.StateCode.OK
                ? new StandardOutput<MessageDto>
                {
                    Message = "Delete Message failed."
                }
                : new StandardOutput<MessageDto> { Entity = responseMessage.Entity, Message = "Delete Success" };
        }
    }
}