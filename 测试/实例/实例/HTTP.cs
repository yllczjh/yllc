using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace 实例
{
    public partial class HTTP : Form
    {
        public HTTP()
        {
            InitializeComponent();
        }
        HttpClient client = new HttpClient();
        public string HttpPost(string url, dynamic body)
        {
            HttpContent httpContent = new StringContent(body);
            //var handler = new HttpClientHandler() { UseCookies = false };

            if (client.DefaultRequestHeaders.Contains("msgid"))
            {
                client.DefaultRequestHeaders.Remove("msgid");
            }
            if (client.DefaultRequestHeaders.Contains("customid"))
            {
                client.DefaultRequestHeaders.Remove("customid");
            }
            if (client.DefaultRequestHeaders.Contains("clienttype"))
            {
                client.DefaultRequestHeaders.Remove("clienttype");
            }
            if (client.DefaultRequestHeaders.Contains("token"))
            {
                client.DefaultRequestHeaders.Remove("token");
            }
            if (client.DefaultRequestHeaders.Contains("reqtime"))
            {
                client.DefaultRequestHeaders.Remove("reqtime");
            }
            if (client.DefaultRequestHeaders.Contains("code"))
            {
                client.DefaultRequestHeaders.Remove("code");
            }
            if (client.DefaultRequestHeaders.Contains("sign"))
            {
                client.DefaultRequestHeaders.Remove("sign");
            }

            client.DefaultRequestHeaders.Add("msgid", Guid.NewGuid().ToString("N"));
            client.DefaultRequestHeaders.Add("customid", "web");
            client.DefaultRequestHeaders.Add("clienttype", "web");
            client.DefaultRequestHeaders.Add("token", "1");
            client.DefaultRequestHeaders.Add("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            client.DefaultRequestHeaders.Add("code", "token");
            client.DefaultRequestHeaders.Add("sign", "12");

            HttpResponseMessage response = client.PostAsync(url, httpContent).Result;

            string statusCode = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpPost("http://localhost:61639/api/v1/main/webapi", "");
        }
    }
}
