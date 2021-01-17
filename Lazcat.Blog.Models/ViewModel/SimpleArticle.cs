

namespace Lazcat.Blog.Models.ViewModel
{
    public class SimpleArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public bool IsPublished { get; set; }
        public string PublishTime { get; set; }

    }
}