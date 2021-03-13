namespace Lazcat.Blog.Models.Web
{
    public class Define
    {
        public enum StateCode
        {
            OK = 200,
            Created = 201,
            Accepted = 202,
            NoContent = 204,
            Redirect = 302,
            BadRequest = 400,
            Unauthorized = 401,
            PaymentRequired = 402,
            Forbidden = 403,
            NotFound = 404,
            MethodNotAllowed = 405,
            NotAcceptable = 406,
            RequestTimeout = 408,
            UnsupportedMediaType = 415,
            InternalServerError = 500,
            NotImplemented = 501,
            BadGateway = 502,
            ServiceUnavailable = 503,
            GatewayTimeout = 504,
            HttpVersionNotSupported = 505,
            NoJsonContent = 9001,
            OtherException = 9999,
            OperationFailed = 9101
        }

        public const string DefaultHttpClient = "Default";

        public class UrlName
        {
            public const string Article = "Article";
            public const string Category = "Category";
            public const string Manager = "Manager";
            public const string Edit = "Edit";
        }

        public class AuthorDefine
        {
            public const string Author = "Author";
            public const string AvatarPath = "AvatarPath";
            public const string Description = "Description";
        }

        public class ProviderRoutes
        {
            public class ArticleRoute
            {
                public const string Article = "Article";
                public const string GetAll = "All";
                public const string GetOnlyPublished = "AllPublished";
                public const string UpdateContent = "Content";
                public const string Publish = "Publish";

                public class QueryString
                {
                    public const string IsGetContent = "IsGetContent";
                }
            }
        }


        public class StateMessage
        {
            public const string OtherException =
                "There's somethings error. U can try to reload page to resolve the problem. If it's still existed. Please contact us.";

            public const string NoJsonContent = "Return anything";
        }
    }
}