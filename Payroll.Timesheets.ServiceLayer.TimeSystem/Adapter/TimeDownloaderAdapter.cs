using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Payroll.Timesheets.ServiceLayer.TimeSystem.Adapter
{
    public class TimeDownloaderAdapter
    {
        private HttpClient Client = new();

        public string Info;
        public string APIToken;
        public string Url;

        public class PostData
        {
            public string info;
            public string api_token;
            public string date_from;
            public string date_to;
            public string page;
            public string payroll_code;
        }

        public TimeDownloaderAdapter() { }
        public TimeDownloaderAdapter(IConfigurationSection _config)
        {

            Client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30d)
            };

            Info = _config.GetValue<string>("Info");
            APIToken = _config.GetValue<string>("APIToken");
            Url = _config.GetValue<string>("Url");
        }

        public async Task<T?> GetSummary<T>(DateTime date_from, DateTime date_to, string payroll_code)
        {
            try
            {
                PostData postData = new PostData
                {
                    info = Info,
                    api_token = APIToken,

                    payroll_code = payroll_code,
                    page = (-1).ToString(),
                    date_from = date_from.ToString("yyyy-MM-dd"),
                    date_to = date_to.ToString("yyyy-MM-dd")
                };

                var dicc = new Dictionary<string, string>();
                dicc.Add("postData", JsonConvert.SerializeObject(postData));

                var content = new FormUrlEncodedContent(dicc);

                var responseMessage = await Client.PostAsync(string.Format("{0}", Url), content);
                string responseMessageContent = await responseMessage.Content.ReadAsStringAsync();
                var responseDeserialized = JsonConvert.DeserializeObject<T>(responseMessageContent);

                return responseDeserialized;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }

        public async Task<T?> GetPageContent<T>(DateTime date_from, DateTime date_to, int page, string payroll_code)
        {
            try
            {
                var postData = new PostData
                {
                    info = Info,
                    api_token = APIToken,

                    payroll_code = payroll_code,
                    page = page.ToString(),
                    date_from = date_from.ToString("yyyy-MM-dd"),
                    date_to = date_to.ToString("yyyy-MM-dd")
                };
                var dicc = new Dictionary<string, string>();
                dicc.Add("postData", JsonConvert.SerializeObject(postData));


                var res = await Client.PostAsync(string.Format("{0}", Url), new FormUrlEncodedContent(dicc));
                var resdeserialized = JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());

                return resdeserialized;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }

    }

}