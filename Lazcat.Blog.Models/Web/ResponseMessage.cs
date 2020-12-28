namespace Lazcat.Blog.Models.Web
{
    public class ResponseMessage<T>
    {
        public T Entity { get; set; }
        public string ErrorMessage { get; set; }
        public Setting.StateCode StateCode { get; set; } = Setting.StateCode.OK;
    }
}