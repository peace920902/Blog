using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AntDesign;
using Lazcat.Blog.Models.Infrastructure;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Provider
{
    public abstract class ProviderBase
    {
        /// <summary>
        /// 無回傳值的http method
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpFunc"></param>
        /// <returns></returns>
        protected async Task<ResponseMessage<bool>> SendEmptyResponseBodyRequest(string url, Func<string, Task<HttpResponseMessage>> httpFunc)
        {
            try
            {
                var message = await httpFunc(url);
                var isSuccessStatusCode = message.IsSuccessStatusCode;
                var responseMessage = new ResponseMessage<bool>
                {
                    Entity = isSuccessStatusCode,
                    StateCode = isSuccessStatusCode ? Define.StateCode.OK : (Define.StateCode)message.StatusCode,
                };
                if (isSuccessStatusCode) return responseMessage;
                var exception = await message.Content.ReadFromJsonAsync<HttpException>();
                responseMessage.ErrorMessage = exception?.Content;
                return responseMessage;
            }
            catch (Exception ex)
            {
                return new ResponseMessage<bool>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Define.StateCode.OtherException,
                    Entity = false
                };
            }
        }
        /// <summary>
        /// 無回傳值的http method
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpFunc"></param>
        /// <returns></returns>
        protected async Task<ResponseMessage<bool>> SendEmptyResponseBodyRequest(string url, Func<string, JsonSerializerOptions, CancellationToken, Task<HttpResponseMessage>> httpFunc)
        {
            try
            {
                var message = await httpFunc(url, null, default);
                var isSuccessStatusCode = message.IsSuccessStatusCode;
                var responseMessage = new ResponseMessage<bool>
                {
                    Entity = isSuccessStatusCode,
                    StateCode = isSuccessStatusCode ? Define.StateCode.OK : (Define.StateCode)message.StatusCode,
                };
                if (isSuccessStatusCode) return responseMessage;
                var exception = await message.Content.ReadFromJsonAsync<HttpException>();
                responseMessage.ErrorMessage = exception?.Content;
                return responseMessage;
            }
            catch (Exception ex)
            {
                return new ResponseMessage<bool>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Define.StateCode.OtherException,
                    Entity = false
                };
            }
        }

        /// <summary>
        /// 無回傳值的http method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpFunc"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected async Task<ResponseMessage<bool>> SendEmptyResponseBodyRequest<T>(string url, Func<string, T, JsonSerializerOptions, CancellationToken, Task<HttpResponseMessage>> httpFunc, T input)
        {
            try
            {
                var message = await httpFunc(url, input, null, default);
                var isSuccessStatusCode = message.IsSuccessStatusCode;
                var responseMessage = new ResponseMessage<bool>
                {
                    Entity = isSuccessStatusCode,
                    StateCode = isSuccessStatusCode ? Define.StateCode.OK : (Define.StateCode)message.StatusCode,
                };
                if (isSuccessStatusCode) return responseMessage;
                var exception = await message.Content.ReadFromJsonAsync<HttpException>();
                responseMessage.ErrorMessage = exception?.Content;
                return responseMessage;
            }
            catch (Exception ex)
            {
                return new ResponseMessage<bool>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Define.StateCode.OtherException,
                    Entity = false
                };
            }
        }

        /// <summary>
        /// 通常用於http的GetFromJson<T> 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpFunc"></param>
        /// <returns></returns>
        protected async Task<ResponseMessage<TResponse>> SendRequest<TResponse>(string url, Func<string, JsonSerializerOptions, CancellationToken, Task<TResponse>> httpFunc)
        {
            try
            {
                var result = await httpFunc(url, null, default);
                return new ResponseMessage<TResponse> { Entity = result };
            }
            catch (JsonException jsonException)
            {
                return new ResponseMessage<TResponse>
                {
                    ErrorMessage = jsonException.Message,
                    StateCode = Define.StateCode.NoJsonContent,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<TResponse>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Define.StateCode.OtherException,
                    Entity = default
                };
            }
        }

        /// <summary>
        /// 通常用於http的GetFromJson<T> 且須帶body
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpFunc"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected async Task<ResponseMessage<TResponse>> SendRequest<TResponse, TInput>(string url, Func<string, TInput, JsonSerializerOptions, CancellationToken, Task<TResponse>> httpFunc, TInput input)
        {
            try
            {
                var result = await httpFunc(url, input, null, default);
                return new ResponseMessage<TResponse> { Entity = result };
            }
            catch (JsonException jsonException)
            {
                return new ResponseMessage<TResponse>
                {
                    ErrorMessage = jsonException.Message,
                    StateCode = Define.StateCode.NoJsonContent,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<TResponse>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Define.StateCode.OtherException,
                    Entity = default
                };
            }
        }

        /// <summary>
        /// 取得需反序列化的response body物件
        /// </summary>
        /// <typeparam name="TResponse">需反序列type</typeparam>
        /// <typeparam name="TInput">傳入物件type</typeparam>
        /// <param name="url"></param>
        /// <param name="httpFunc">http func</param>
        /// <param name="input">傳入物件</param>
        /// <returns></returns>
        protected async Task<ResponseMessage<TResponse>> SendNeedDeserializedRequest<TResponse, TInput>(string url, Func<string, TInput, JsonSerializerOptions, CancellationToken, Task<HttpResponseMessage>> httpFunc, TInput input)
        {
            try
            {
                var result = await httpFunc(url, input, null, default);
                var isSuccessStatusCode = result.IsSuccessStatusCode;
                if (isSuccessStatusCode)
                    return new ResponseMessage<TResponse> { Entity = await result.Content.ReadFromJsonAsync<TResponse>(), StateCode = Define.StateCode.OK };
                var errorMessage = await result.Content.ReadFromJsonAsync<HttpException>();
                return new ResponseMessage<TResponse>
                {
                    Entity = default,
                    StateCode = (Define.StateCode)result.StatusCode,
                    ErrorMessage = errorMessage?.Content
                };
            }
            catch (JsonException jsonException)
            {
                return new ResponseMessage<TResponse>
                {
                    ErrorMessage = jsonException.Message,
                    StateCode = Define.StateCode.NoJsonContent,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<TResponse>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Define.StateCode.OtherException,
                    Entity = default
                };
            }
        }

        /// <summary>
        /// 取得需反序列化的response body物件
        /// </summary>
        /// <typeparam name="TResponse">需反序列type</typeparam>
        /// <typeparam name="TInput">傳入物件type</typeparam>
        /// <param name="url"></param>
        /// <param name="httpFunc">http func</param>
        /// <param name="input">傳入物件</param>
        /// <returns></returns>
        protected async Task<ResponseMessage<TResponse>> SendNeedDeserializedRequest<TResponse>(string url, Func<string, CancellationToken, Task<HttpResponseMessage>> httpFunc)
        {
            try
            {
                var result = await httpFunc(url,default);
                var isSuccessStatusCode = result.IsSuccessStatusCode;
                if (isSuccessStatusCode)
                    return new ResponseMessage<TResponse> { Entity = await result.Content.ReadFromJsonAsync<TResponse>(), StateCode = Define.StateCode.OK };
                var errorMessage = await result.Content.ReadFromJsonAsync<HttpException>();
                return new ResponseMessage<TResponse>
                {
                    Entity = default,
                    StateCode = (Define.StateCode)result.StatusCode,
                    ErrorMessage = errorMessage?.Content
                };
            }
            catch (JsonException jsonException)
            {
                return new ResponseMessage<TResponse>
                {
                    ErrorMessage = jsonException.Message,
                    StateCode = Define.StateCode.NoJsonContent,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<TResponse>
                {
                    ErrorMessage = ex.Message,
                    StateCode = Define.StateCode.OtherException,
                    Entity = default
                };
            }
        }
    }
}