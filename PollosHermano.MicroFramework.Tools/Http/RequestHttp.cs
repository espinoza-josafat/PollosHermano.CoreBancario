using PollosHermano.MicroFramework.Tools.MicroJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Tools.Http
{
    public static class RequestHttp
    {
        public static T Get<T>(string url, Dictionary<string, string> headers = null)
        {
            var task = GetAsync<T>(url, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static T GetWithError<T>(string url, Dictionary<string, string> headers = null)
        {
            var task = GetWithErrorAsync<T>(url, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            try
            {
                result = await GetWithErrorAsync<T>(url, headers);
            }
            catch (WebException)
            {

            }
            catch (Exception)
            {

            }

            return result;
        }

        public static async Task<T> GetWithErrorAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = 300000;
            httpWebRequest.ReadWriteTimeout = 600000;

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers[header.Key] = header.Value;
                }
            }

            using (var httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
            {
                using (var stream = httpWebResponse.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        var @string = await streamReader.ReadToEndAsync();

                        if (typeof(T) == typeof(string))
                        {
                            result = (T)(object)@string;
                        }
                        else
                        {
                            result = JsonSerializer.DeserializeObject<T>(@string);
                        }

                        streamReader.Close();
                    }

                    await stream.FlushAsync();
                    stream.Close();
                }

                httpWebResponse.Close();
            }

            return result;
        }

        public static async Task<T> GetWithErrorFromBodyAsync<T>(string url, Dictionary<string, string> headers = null, object objectSendFromBody = null)
        {
            var result = default(T);

            using (var client = new HttpClient())
            {
                var model = string.Empty;

                if (objectSendFromBody != null)
                {
                    model = typeof(string) == objectSendFromBody.GetType() ? (string)objectSendFromBody : JsonSerializer.SerializeObject(objectSendFromBody);
                }

                if (headers != null && headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                    Content = new StringContent(model, Encoding.UTF8, "application/json"),
                };

                var response = await client.SendAsync(request).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var @string = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (typeof(T) == typeof(string))
                {
                    result = (T)(object)@string;
                }
                else
                {
                    result = JsonSerializer.DeserializeObject<T>(@string);
                }
            }

            return result;
        }

        public static T PostJson<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var task = PostJsonAsync<T>(url, objectToSend, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static T PostJsonWithError<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var task = PostJsonWithErrorAsync<T>(url, objectToSend, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static async Task<T> PostJsonAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            return await RequestJsonAsync<T>("POST", url, objectToSend, headers);
        }

        public static async Task<T> PostJsonWithErrorAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            return await RequestJsonWithErrorAsync<T>("POST", url, objectToSend, headers);
        }

        public static T PutJson<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var task = PutJsonAsync<T>(url, objectToSend, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static T PutJsonWithError<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var task = PutJsonWithErrorAsync<T>(url, objectToSend, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static async Task<T> PutJsonAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            return await RequestJsonAsync<T>("PUT", url, objectToSend, headers);
        }

        public static async Task<T> PutJsonWithErrorAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            return await RequestJsonWithErrorAsync<T>("PUT", url, objectToSend, headers);
        }

        public static T PatchJson<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var task = PatchJsonAsync<T>(url, objectToSend, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static T PatchJsonWithError<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var task = PatchJsonWithErrorAsync<T>(url, objectToSend, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static async Task<T> PatchJsonAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            return await RequestJsonAsync<T>("PATCH", url, objectToSend, headers);
        }

        public static async Task<T> PatchJsonWithErrorAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            return await RequestJsonWithErrorAsync<T>("PATCH", url, objectToSend, headers);
        }

        public static T DeleteJson<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var task = DeleteJsonAsync<T>(url, objectToSend, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static T DeleteJsonWithError<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var task = DeleteJsonWithErrorAsync<T>(url, objectToSend, headers);

            if (!task.IsCompleted)
            {
                task.Wait();
            }

            return task.Result;
        }

        public static async Task<T> DeleteJsonAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            return await RequestJsonAsync<T>("DELETE", url, objectToSend, headers);
        }

        public static async Task<T> DeleteJsonWithErrorAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            return await RequestJsonWithErrorAsync<T>("DELETE", url, objectToSend, headers);
        }

        static async Task<T> RequestJsonAsync<T>(string method, string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            try
            {
                result = await RequestJsonWithErrorAsync<T>(method, url, objectToSend, headers);
            }
            catch (WebException)
            {
            }
            catch (Exception)
            {
            }

            return result;
        }

        static async Task<T> RequestJsonWithErrorAsync<T>(string method, string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = method;
            if (objectToSend != null)
            {
                httpWebRequest.ContentType = "application/json; charset=utf-8";
            }
            httpWebRequest.Timeout = 300000;
            httpWebRequest.ReadWriteTimeout = 600000;

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers[header.Key] = header.Value;
                }
            }

            if (objectToSend != null)
            {
                var json = typeof(string) == objectToSend.GetType() ? (string)objectToSend : JsonSerializer.SerializeObject(objectToSend);
                var dataToSend = Encoding.UTF8.GetBytes(json);

                httpWebRequest.ContentLength = dataToSend.Length;

                using var stream = await httpWebRequest.GetRequestStreamAsync();
                await stream.WriteAsync(dataToSend.AsMemory(0, dataToSend.Length));
                await stream.FlushAsync();
                stream.Close();
            }

            if (url.Trim().ToLower().StartsWith("https"))
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            }

            using (var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var @string = await streamReader.ReadToEndAsync();

                    if (typeof(T) == typeof(string))
                    {
                        result = (T)(object)@string;
                    }
                    else
                    {
                        result = JsonSerializer.DeserializeObject<T>(@string);
                    }

                    streamReader.Close();
                }

                httpResponse.Close();
            }

            return result;
        }

        static bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
