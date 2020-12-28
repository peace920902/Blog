using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AntDesign;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Provider
{
    public abstract class ProviderBase
    {
        protected async Task<ResponseMessage<bool>> SendEmptyResponseBodyRequest(string url, Func<string, Task<HttpResponseMessage>> httpFunc)
        {
            try
            {
                var message = await httpFunc(url);
                return new ResponseMessage<bool>
                {
                    Entity = message.IsSuccessStatusCode,
                    StateCode = (Setting.StateCode)message.StatusCode,
                    ErrorMessage = message.ReasonPhrase
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<bool>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Setting.StateCode.OtherException,
                    Entity = false
                };
            }
        }
        protected async Task<ResponseMessage<bool>> SendEmptyResponseBodyRequest(string url, Func<string, JsonSerializerOptions, CancellationToken, Task<HttpResponseMessage>> httpFunc)
        {
            try
            {
                var message = await httpFunc(url,null,default);
                return new ResponseMessage<bool>
                {
                    Entity = message.IsSuccessStatusCode,
                    StateCode = (Setting.StateCode)message.StatusCode,
                    ErrorMessage = message.ReasonPhrase
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<bool>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Setting.StateCode.OtherException,
                    Entity = false
                };
            }
        }



        protected async Task<ResponseMessage<bool>> SendEmptyResponseBodyRequest<T>(string url, Func<string, T, JsonSerializerOptions, CancellationToken, Task<HttpResponseMessage>> httpFunc, T input)
        {
            try
            {
                var message = await httpFunc(url,input, null, default);
                return new ResponseMessage<bool>
                {
                    Entity = message.IsSuccessStatusCode,
                    StateCode = (Setting.StateCode)message.StatusCode,
                    ErrorMessage = message.ReasonPhrase
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<bool>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Setting.StateCode.OtherException,
                    Entity = false
                };
            }
        }

        protected async Task<ResponseMessage<T>> SendRequest<T>(string url, Func<string, JsonSerializerOptions, CancellationToken, Task<T>> httpFunc)
        {
            try
            {
                var result = await httpFunc(url,null,default);
                return new ResponseMessage<T> { Entity = result };
            }
            catch (JsonException jsonException)
            {
                return new ResponseMessage<T>
                {
                    ErrorMessage = jsonException.Message,
                    StateCode = Setting.StateCode.NoJsonContent,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<T>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Setting.StateCode.OtherException,
                    Entity = default
                };
            }
        }

        protected async Task<ResponseMessage<T>> SendRequest<T, T1>(string url, Func<string, T1, JsonSerializerOptions, CancellationToken, Task<T>> httpFunc, T1 input)
        {
            try
            {
                var result = await httpFunc(url, input,null,default);
                return new ResponseMessage<T> { Entity = result };
            }
            catch (JsonException jsonException)
            {
                return new ResponseMessage<T>
                {
                    ErrorMessage = jsonException.Message,
                    StateCode = Setting.StateCode.NoJsonContent,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<T>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Setting.StateCode.OtherException,
                    Entity = default
                };
            }
        }
    }
}