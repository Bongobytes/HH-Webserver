using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace hhwebserver
{
    class Program
    {
        static void Main()
        {
            string ipAddress = "127.0.0.1";
            int port = 3012;

            var listener = new HttpListener();
            listener.Prefixes.Add($"http://{ipAddress}:{port}/");
            listener.Start();

            Console.WriteLine($"Web server started. Listening on {ipAddress}:{port}");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                string requestedUrl = request.Url.LocalPath.TrimStart('/');
                if (string.IsNullOrEmpty(requestedUrl) || requestedUrl.EndsWith("/"))
                {
                    requestedUrl += "index.html";
                }

                string resourcePath = GetResourcePath(requestedUrl);
                if (resourcePath != null)
                {
                    using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
                    {
                        if (resourceStream != null)
                        {
                            byte[] fileBytes;
                            using (var memoryStream = new MemoryStream())
                            {
                                resourceStream.CopyTo(memoryStream);
                                fileBytes = memoryStream.ToArray();
                            }

                            response.ContentType = GetContentType(requestedUrl);
                            response.ContentEncoding = Encoding.UTF8;
                            response.ContentLength64 = fileBytes.Length;

                            using (var outputStream = response.OutputStream)
                            {
                                outputStream.Write(fileBytes, 0, fileBytes.Length);
                            }
                        }
                        else
                        {
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                        }
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                response.Close();
            }
        }

        static string GetContentType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".html":
                    return "text/html";
                case ".css":
                    return "text/css";
                case ".js":
                    return "application/javascript";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".ico":
                    return "image/x-icon";
                default:
                    return "application/octet-stream";
            }
        }

        static string GetResourcePath(string url)
        {
            string resourceName = "hhwebserver." + url.Replace('/', '.');

            string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (var name in resourceNames)
            {
                if (name.EndsWith(resourceName))
                {
                    return name;
                }
            }

            return null;
        }
    }
}
