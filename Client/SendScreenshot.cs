using System;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace UGN_Client
{

    public class SendScreenshot
    {

        private static string _file_path;
        private static string _paramName;
        private static string _contentType;
        private static NameValueCollection _nvc;
        private static string boundary;
        private static byte[] boundarybytes;

        public static void Run(string url, string file_path, string paramName, string contentType, NameValueCollection nvc)
        {
            _file_path = file_path;
            _paramName = paramName;
            _contentType = contentType;
            _nvc = nvc;

            boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            // here we "observe" the Task exceptions so that it doesn't crash the app
            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                args.SetObserved();
            };

            Task.Factory.StartNew(() =>
            {

                try
                {

                    RequestTools.XPfix();

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.ContentType = "multipart/form-data; boundary=" + boundary;
                    request.Method = "POST";
                    request.Proxy = null;
                    request.KeepAlive = true;
                    request.SendChunked = true;

                    // start
                    Stream postStream = request.GetRequestStream();

                    string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

                    foreach (string key in _nvc.Keys)
                    {
                        postStream.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, key, _nvc[key]);
                        byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                        postStream.Write(formitembytes, 0, formitembytes.Length);
                    }

                    postStream.Write(boundarybytes, 0, boundarybytes.Length);
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: {2}\r\n\r\n";
                    string header = string.Format(headerTemplate, _paramName, _file_path, _contentType);
                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                    postStream.Write(headerbytes, 0, headerbytes.Length);
                    int BufferSize = 1024;

                    // source file Stream
                    Stream sourceFileStream = new FileStream(_file_path, FileMode.Open, FileAccess.Read);

                    byte[] buffer = new byte[BufferSize];

                    int readCount = sourceFileStream.Read(buffer, 0, BufferSize);

                    while (readCount > 0)
                    {
                        postStream.Write(buffer, 0, readCount);
                        readCount = sourceFileStream.Read(buffer, 0, BufferSize);
                        Thread.Sleep(220);
                    }

                    sourceFileStream.Close();

                    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

                    // Write to the request stream.
                    postStream.Write(trailer, 0, trailer.Length);
                    postStream.Close();

                    // End the operation
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream streamResponse = response.GetResponseStream();

                    // Close the stream object
                    streamResponse.Close();

                    // Release the HttpWebResponse
                    response.Close();

                }
                catch (WebException WebEx)
                {
                    // suppress (loss of connection)
                }

            });

        }

    }
}















