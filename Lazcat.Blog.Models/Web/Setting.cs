using System.Net;

namespace Lazcat.Blog.Models.Web
{
    public class Setting
    {
        public const string DefaultHttpClient = "Default";

        public enum StateCode
        {
            NoJsonContent = 9001,
            OtherException = 9999,
            OperationFailed = 9101,
            Ok = 1000,
        }
        
        public class StateMessage
        {
            public const string OtherException = "There's somethings error. U can try to reload page to resolve the problem. If it's still existed. Please contact us.";
            public const string NoJsonContent = "Return anything";
        }
    }
}