using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Utils.Tool
{
    public static class HttpRequestTool
    {
        public static string HttpGet(string url, Dictionary<string, string>? headers = null, Encoding? encoding = null, int timeOut = 60000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = timeOut;
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream responseStream = response.GetResponseStream();
            using StreamReader myStreamReader = new(responseStream, encoding ?? Encoding.GetEncoding("utf-8"));
            return myStreamReader.ReadToEnd();
        }

        public static string HttpPost(string url, Dictionary<string, string>? formData = null, Encoding? encoding = null, int timeOut = 60000, Dictionary<string, string>? headers = null)
        {
            MemoryStream ms = new();
            formData?.FillFormDataStream(ms);
            return HttpPost(url, ms, "application/x-www-form-urlencoded", encoding, headers, timeOut);
        }

        public static string HttpPost(string url, string postData, Dictionary<string, string>? headers = null, Encoding? encoding = null, string method = "Post")
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
            return HttpPost(url, stream, "application/json", encoding, headers, 60000, method);
        }

        public static string HttpPost(string url, object dataObj, Encoding? encoding = null, Dictionary<string, string>? headers = null)
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
            return HttpPost(url, stream, "application/json", encoding, headers);
        }

        public static string HttpPost(string url, Stream? postStream = null, string contentType = "application/x-www-form-urlencoded", Encoding? encoding = null, Dictionary<string, string>? headers = null, int timeOut = 60000, string method = "Post")
        {
            if (method.Equals("PATCH", StringComparison.CurrentCultureIgnoreCase))
            {
                ServicePointManager.Expect100Continue = false;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Timeout = timeOut;
            request.ContentLength = postStream?.Length ?? 0;
            request.Accept = "*/*";
            request.KeepAlive = false;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.87 Safari/537.36 QQBrowser/9.2.5748.400";
            request.ContentType = contentType;
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            if (postStream != null)
            {
                postStream.Position = 0L;
                Stream requestStream = request.GetRequestStream();
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }
                postStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream responseStream = response.GetResponseStream();
            using StreamReader myStreamReader = new(responseStream, encoding ?? Encoding.GetEncoding("utf-8"));
            return myStreamReader.ReadToEnd();
        }

        public static void FillFormDataStream(this Dictionary<string, string> formData, Stream stream)
        {
            string dataString = formData.GetQueryString();
            byte[] formDataBytes = ((formData == null) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(dataString));
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0L, SeekOrigin.Begin);
        }

        public static string GetQueryString(this Dictionary<string, string> formData)
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

        public static string UrlEncode(string str)
        {
            StringBuilder sb = new();
            byte[] byStr = Encoding.UTF8.GetBytes(str);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append("%" + Convert.ToString(byStr[i], 16));
            }
            return sb.ToString();
        }

        public static string UploadFile(string url, string filePath, string fileName, Dictionary<string, string>? headers = null)
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
            return PostForm(url, formDatas, headers);
        }

        public static string PostForm(string url, List<FormItemModel> formItems, Dictionary<string, string>? headers = null, CookieContainer? cookieContainer = null, string? refererUrl = null, Encoding? encoding = null, int timeOut = 20000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = timeOut;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            if (!string.IsNullOrEmpty(refererUrl))
            {
                request.Referer = refererUrl;
            }
            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            string boundary = "----" + DateTime.Now.Ticks.ToString("x");
            request.ContentType = $"multipart/form-data; boundary={boundary}";
            MemoryStream postStream = new();
            if (formItems != null && formItems.Count > 0)
            {
                string fileFormdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                string dataFormdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                byte[] header2 = Encoding.UTF8.GetBytes("--" + boundary);
                postStream.Write(header2, 0, header2.Length);
                int index = 0;
                foreach (FormItemModel item in formItems)
                {
                    index++;
                    string formdata = (item.IsFile ? ((index != 1) ? string.Format(fileFormdataTemplate, item.Key, item.FileName) : $"\r\nContent-Disposition: form-data; name=\"{item.Key}\"; filename=\"{item.FileName}\"\r\nContent-Type: application/octet-stream\r\n\r\n") : ((index != 1) ? string.Format(dataFormdataTemplate, item.Key, item.Value) : $"\r\nContent-Disposition: form-data; name=\"{item.Key}\"\r\n\r\n{item.FileName}"));
                    byte[] formdataBytes = Encoding.UTF8.GetBytes(formdata);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);
                    if (item.IsFile && File.Exists(item.FilePath))
                    {
                        FileStream fs = new(item.FilePath, FileMode.Open, FileAccess.Read);
                        byte[] byData = new byte[fs.Length];
                        fs.Read(byData, 0, byData.Length);
                        fs.Close();
                        postStream.Write(byData, 0, byData.Length);
                    }
                }
                byte[] footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--");
                postStream.Write(footer, 0, footer.Length);
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            request.ContentLength = postStream.Length;
            if (postStream != null)
            {
                postStream.Position = 0L;
                Stream requestStream = request.GetRequestStream();
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }
                postStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }
            using Stream responseStream = response.GetResponseStream();
            using StreamReader myStreamReader = new(responseStream, encoding ?? Encoding.GetEncoding("utf-8"));
            return myStreamReader.ReadToEnd();
        }
    }

    public class FormItemModel
    {
        public string? Key { get; set; }

        public string? Value { get; set; }

        public bool IsFile { get; set; }

        public string? FileName { get; set; }

        public string? FilePath { get; set; }
    }
}
