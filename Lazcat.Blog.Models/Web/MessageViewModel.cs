using System;
using System.Collections.Generic;

namespace Lazcat.Blog.Models.Web
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDeleted { get; set; }
        public List<MessageViewModel> ReplyMessages { get; set; }
    }
}