using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lazcat.Blog.Models.Domain.Messages
{
    public class ReplyMessage : Entity<Guid>
    {
        public Guid MessageId { get; set; }
        public Message Message { get; set; }
        public Guid ReplyId { get; set; }
        public Message RepliedMessage { get; set; }
    }
}