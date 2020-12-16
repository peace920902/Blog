using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Lazcat.Blog.Models.Domain.Articles;

namespace Lazcat.Blog.Models.Domain.Messages
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public DateTime CreateTime { get; set; }
        public ICollection<ReplyMessage> ReplyMessages { get; set; }

        public Message()
        {
            Id = Guid.NewGuid();
        }
    }
}