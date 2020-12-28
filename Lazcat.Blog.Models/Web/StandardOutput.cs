namespace Lazcat.Blog.Models.Web
{
    public class StandardOutput<T>
    {
        public T Entity { get; set; }
        public string Message { get; set; }
    }
}