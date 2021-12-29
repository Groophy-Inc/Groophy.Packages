using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Groophy.Packages
{
    #region Helpers
    public class Configure
    {
        public int Port { get; set; }
    }

    public class paramst
    {
        public string var { get; set; }
        public string val { get; set; }
    }

    public class ConfigureClient
    {
        public int Port { get; set; }
        public paramst[] param { get; set; }
    }

    public class User
    {
        public HttpListenerContext UserContext { get; set; }
        public System.Collections.Specialized.NameValueCollection Query { get; set; }

        public void Response(string html)
        {
            byte[] _responseArray = System.Text.Encoding.UTF8.GetBytes(html);
            UserContext.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length);
        }

        public void Kill()
        {
            UserContext.Response.KeepAlive = false;
            UserContext.Response.Close();
        }
    }
    #endregion
    public class Web
    {
        #region listen
        public delegate void onDataHandler(Web sender, User e);
        public event onDataHandler onData;

        static HttpListener _httpListener = new HttpListener();

        public void run(Configure c)
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add("http://localhost:" + c.Port.ToString() + "/");
            _httpListener.Start();
            Thread _responseThread = new Thread(ResponseThread);
            _responseThread.Start(); 
        }

        private void ResponseThread()
        {
            while (true)
            {
                HttpListenerContext context = _httpListener.GetContext();
                User u = new User()
                {
                    UserContext = context,
                    Query = context.Request.QueryString
                };
                onData(this, u);
            }
        }
        #endregion

        #region client
        public delegate void onClientHandler(Web sender, HttpResponseMessage Context, string response);
        public event onClientHandler onClient;

        static readonly  HttpClient client = new HttpClient();

        public async Task Client(ConfigureClient c)
        {
            try
            {
                string link = "http://localhost:" + c.Port.ToString() + "/?";
                foreach (paramst a in c.param)
                {
                    link += a.var + "=" + a.val;
                }
                HttpResponseMessage response = await client.GetAsync(link);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                onClient(this, response, responseBody);
            }
            catch (HttpRequestException e)
            {
                onClient(this, null, null);
            }
        }
        #endregion

    }
}
