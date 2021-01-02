using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AntDesign;
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
                return new ResponseMessage<bool>
                {
                    Entity = message.IsSuccessStatusCode,
                    StateCode = message.IsSuccessStatusCode ? Setting.StateCode.OK : (Setting.StateCode)message.StatusCode,
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
                return new ResponseMessage<bool>
                {
                    Entity = message.IsSuccessStatusCode,
                    StateCode = message.IsSuccessStatusCode ? Setting.StateCode.OK : (Setting.StateCode)message.StatusCode,
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
                return new ResponseMessage<bool>
                {
                    Entity = message.IsSuccessStatusCode,
                    StateCode = message.IsSuccessStatusCode ? Setting.StateCode.OK : (Setting.StateCode)message.StatusCode,
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

        /// <summary>
        /// 通常用於http的GetFromJson<T> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpFunc"></param>
        /// <returns></returns>
        protected async Task<ResponseMessage<T>> SendRequest<T>(string url, Func<string, JsonSerializerOptions, CancellationToken, Task<T>> httpFunc)
        {
            try
            {
                var result = await httpFunc(url, null, default);
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

        /// <summary>
        /// 通常用於http的GetFromJson<T> 且須帶body
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpFunc"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected async Task<ResponseMessage<T>> SendRequest<T, T1>(string url, Func<string, T1, JsonSerializerOptions, CancellationToken, Task<T>> httpFunc, T1 input)
        {
            try
            {
                var result = await httpFunc(url, input, null, default);
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

        /// <summary>
        /// 取得需反序列化的response body物件
        /// </summary>
        /// <typeparam name="T">需反序列type</typeparam>
        /// <typeparam name="T1">傳入物件type</typeparam>
        /// <param name="url"></param>
        /// <param name="httpFunc">http func</param>
        /// <param name="input">傳入物件</param>
        /// <returns></returns>
        protected async Task<ResponseMessage<T>> SendNeedDeserializedRequest<T, T1>(string url, Func<string, T1, JsonSerializerOptions, CancellationToken, Task<HttpResponseMessage>> httpFunc, T1 input)
        {
            try
            {
                var result = await httpFunc(url, input, null, default);
                return result.IsSuccessStatusCode ? new ResponseMessage<T> { Entity = await result.Content.ReadFromJsonAsync<T>() }
                    : new ResponseMessage<T> { Entity = default, StateCode = result.IsSuccessStatusCode ? Setting.StateCode.OK : (Setting.StateCode)result.StatusCode, ErrorMessage = result.ReasonPhrase };
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