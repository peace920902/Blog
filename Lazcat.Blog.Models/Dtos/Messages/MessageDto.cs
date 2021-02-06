using System;

namespace Lazcat.Blog.Models.Dtos.Messages
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid? ReplyId { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}