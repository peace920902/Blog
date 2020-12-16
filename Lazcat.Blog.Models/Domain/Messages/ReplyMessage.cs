using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lazcat.Blog.Models.Domain.Messages
{
    public class ReplyMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Message Message { get; set; }
        public Guid ReplyId { get; set; }
        public Message RepliedMessage { get; set; }
    }
}