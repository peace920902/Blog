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

        public async Task UpdateMessage(CreateUpdateMessageInput input)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMessage(Guid messageId)
        {
            throw new NotImplementedException();
        }
    }
}