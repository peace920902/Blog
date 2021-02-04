using System;
using System.ComponentModel.DataAnnotations;

namespace Lazcat.Blog.Models.Dtos.Messages
{
    public class CreateUpdateMessageInput
    {
        public Guid Id { get; set; }
        public Guid? ReplyId { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        [Required]
        public int ArticleId { get; set; }
    }
}