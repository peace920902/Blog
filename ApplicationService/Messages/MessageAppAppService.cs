using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Infrastructure;
using Lazcat.Blog.Models.Domain.Messages;
using Lazcat.Blog.Models.Dtos.Messages;
using Lazcat.Blog.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Lazcat.Blog.ApplicationService.Messages
{
    public class MessageAppAppService : IMessageAppService
    {
        private readonly IRepository<Guid, Message> _messageRepository;

        private readonly IMapper _mapper;

        public MessageAppAppService(IRepository<Guid, Message> messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<MessageDto>> GetMessages(int articleId)
        {
            var messages = await _messageRepository.GetAll().Where(x => x.ArticleId == articleId).ToListAsync();
            return _mapper.Map<List<Message>, IEnumerable<MessageDto>>(messages);
        }

        public async Task<MessageDto> CreateMessage(CreateUpdateMessageInput input)
        {
            if (input.ReplyId != null)
            {
                var replyMessage = await _messageRepository.FindAsync(input.ReplyId.Value);
                if (replyMessage == null)
                    throw ExceptionBuilder.Build(HttpStatusCode.BadRequest,
                        new HttpException($"Reply Message Id:{input.ReplyId} cannot bind any message"));
            }
            var message = new Message();
            _mapper.Map(input, message);
            var createMessage = await _messageRepository.CreateAsync(message);
            return _mapper.Map<Message, MessageDto>(createMessage);
        }

        public async Task<MessageDto> UpdateMessage(CreateUpdateMessageInput input)
        {
            if(input.Id==null) throw ExceptionBuilder.Build(HttpStatusCode.BadRequest,
                new HttpException($"Message Id:{input.Id} cannot be null"));
            var message = await _messageRepository.FindAsync(input.Id.Value);
            if (message == null) throw ExceptionBuilder.Build(HttpStatusCode.BadRequest,
                new HttpException($"Message Id:{input.Id} cannot bind any message"));
            _mapper.Map(input, message);
            return _mapper.Map<Message, MessageDto>(await _messageRepository.UpdateAsync(message.Id, message));
        }

        public async Task<MessageDto> DeleteMessage(Guid messageId)
        {
            var message = await _messageRepository.FindAsync(messageId);
            if(message==null) throw ExceptionBuilder.Build(HttpStatusCode.BadRequest,
                new HttpException($"Message Id:{messageId} cannot bind any message"));

            message.IsDeleted = true;
            return _mapper.Map<Message,MessageDto>(await _messageRepository.UpdateAsync(message.Id, message));
        }
    }
}