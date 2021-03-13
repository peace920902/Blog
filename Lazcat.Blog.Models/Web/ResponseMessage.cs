namespace Lazcat.Blog.Models.Web
{
    public class ResponseMessage<T>
    {
        public T Entity { get; set; }
        public string ErrorMessage { get; set; }
        public Define.StateCode StateCode { get; set; } = Define.StateCode.OK;
    }
}