using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Tool
{
    public static class HttpRequestTool
    {
        public static async Task<string> HttpGet(string url, Dictionary<string, string> headers = null, Encoding encoding = null, int timeOut = 60000)
        {
            using var client = CreateHttpClient(timeOut, headers);

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            byte[] responseBytes = await response.Content.ReadAsByteArrayAsync();
            return (encoding ?? Encoding.UTF8).GetString(responseBytes);
        }

        public static async Task DownloadFileWithProgress(string url, string savePath,
            Action<long, long, double> onProgress = null, Action<HttpResponseHeaders> validateHeaders = null, Dictionary<string, string> headers = null, int timeOut = 60000)
        {
            using var client = CreateHttpClient(timeOut, headers);

            using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            // 验证头部信息
            validateHeaders?.Invoke(response.Headers);

            var totalBytes = response.Content.Headers.ContentLength ?? -1L;

            using var contentStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);

            var buffer = new byte[8192]; // 8KB 缓冲区
            long totalDownloaded = 0L;
            int bytesRead;

            while ((bytesRead = await contentStream.ReadAsync(buffer)) > 0)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                totalDownloaded += bytesRead;

                // 报告进度
                onProgress?.Invoke(totalDownloaded, totalBytes, totalBytes > 0 ? (double)totalDownloaded / totalBytes : 0);
            }
        }

        public static async Task<string> HttpPost(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 60000, Dictionary<string, string> headers = null)
        {
            MemoryStream ms = new();
            formData?.FillFormDataStream(ms);
            return await HttpPost(url, ms, "application/x-www-form-urlencoded", encoding, headers, timeOut);
        }

        public static async Task<string> HttpPost(string url, string postData, Dictionary<string, string> headers = null, Encoding encoding = null, string method = "Post")
        {
            encoding ??= Encoding.UTF8;
            if (string.IsNullOrWhiteSpace(postData))
            {
                throw new ArgumentNullException(nameof(postData));
            }
            MemoryStream stream = new();
            byte[] formDataBytes = ((postData == null) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(postData));
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0L, SeekOrigin.Begin);
            return await HttpPost(url, stream, "application/json", encoding, headers, 60000, method);
        }

        public static async Task<string> HttpPost(string url, object dataObj, Encoding encoding = null, Dictionary<string, string> headers = null)
        {
            encoding ??= Encoding.UTF8;
            if (dataObj == null)
            {
                throw new ArgumentNullException(nameof(dataObj));
            }
            string postData = JsonConvert.SerializeObject(dataObj, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            });
            MemoryStream stream = new();
            byte[] formDataBytes = ((postData == null) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(postData));
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0L, SeekOrigin.Begin);
            return await HttpPost(url, stream, "application/json", encoding, headers);
        }

        public static async Task<string> HttpPost(string url, Stream postStream = null, string contentType = "application/x-www-form-urlencoded",
            Encoding encoding = null, Dictionary<string, string> headers = null, int timeOut = 60000, string method = "POST")
        {
            using var client = CreateHttpClient(timeOut, headers);

            HttpMethod httpMethod = method.ToUpper() switch
            {
                "PATCH" => HttpMethod.Patch,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => HttpMethod.Post
            };

            HttpRequestMessage request = new(httpMethod, url);

            if (postStream != null)
            {
                postStream.Position = 0;
                request.Content = new StreamContent(postStream);
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            }

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            byte[] responseBytes = await response.Content.ReadAsByteArrayAsync();
            return (encoding ?? Encoding.UTF8).GetString(responseBytes);
        }

        public static async Task<string> UploadFile(string url, string filePath, string fileName, Dictionary<string, string> headers = null)
        {
            List<FormItemModel> formDatas = new()
            {
                new FormItemModel
                {
                    Key = "",
                    Value = "",
                    IsFile = true,
                    FileName = fileName,
                    FilePath = filePath
                }
            };
            return await PostForm(url, formDatas, headers);
        }

        public static async Task<string> PostForm(string url, List<FormItemModel> formItems, Dictionary<string, string> headers = null,
            CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 20000)
        {
            using var client = CreateHttpClient(timeOut, headers, cookieContainer, refererUrl);

            var formContent = new MultipartFormDataContent($"----{DateTime.Now.Ticks:x}");

            if (formItems != null && formItems.Count > 0)
            {
                foreach (var item in formItems)
                {
                    if (item.IsFile && File.Exists(item.FilePath))
                    {
                        byte[] fileBytes = await File.ReadAllBytesAsync(item.FilePath);
                        var fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                        formContent.Add(fileContent, item.Key, item.FileName);
                    }
                    else
                    {
                        formContent.Add(new StringContent(item.Value ?? "", encoding ?? Encoding.UTF8), item.Key);
                    }
                }
            }

            HttpResponseMessage response = await client.PostAsync(url, formContent);
            response.EnsureSuccessStatusCode();

            byte[] responseBytes = await response.Content.ReadAsByteArrayAsync();
            return (encoding ?? Encoding.UTF8).GetString(responseBytes);
        }

        private static HttpClient CreateHttpClient(int timeout, Dictionary<string, string> headers = null, CookieContainer cookieContainer = null, string referer = null)
        {
            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = cookieContainer ?? new CookieContainer()
            };

            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromMilliseconds(timeout)
            };

            // 设置默认请求头
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.87 Safari/537.36 QQBrowser/9.2.5748.400");
            client.DefaultRequestHeaders.Accept.ParseAdd("*/*");

            if (!string.IsNullOrEmpty(referer))
            {
                client.DefaultRequestHeaders.Referrer = new Uri(referer);
            }

            // 添加自定义请求头
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value))
                    {
                        // 对于不能添加到默认请求头的头部，我们会在具体请求中处理
                    }
                }
            }

            return client;
        }

        private static void FillFormDataStream(this Dictionary<string, string> formData, Stream stream)
        {
            string dataString = formData.GetQueryString();
            byte[] formDataBytes = ((formData == null) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(dataString));
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0L, SeekOrigin.Begin);
        }

        private static string GetQueryString(this Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return "";
            }
            StringBuilder sb = new();
            int i = 0;
            foreach (KeyValuePair<string, string> kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, UrlEncode(kv.Value));
                if (i < formData.Count)
                {
                    sb.Append('&');
                }
            }
            return sb.ToString();
        }

        private static string UrlEncode(string str)
        {
            StringBuilder sb = new();
            byte[] byStr = Encoding.UTF8.GetBytes(str);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append("%" + Convert.ToString(byStr[i], 16));
            }
            return sb.ToString();
        }
    }

    public class FormItemModel
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public bool IsFile { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }
    }
}
