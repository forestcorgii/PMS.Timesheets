using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pms.Timesheets.ServiceLayer.TimeSystem.Adapter
{
    public class TimeDownloaderAdapter
    {
        private HttpClient Client = new();
        private TimeDownloaderParameter Parameter;

        public TimeDownloaderAdapter(TimeDownloaderParameter parameter)
        {
            Client = new HttpClient { Timeout = TimeSpan.FromMinutes(2d) };
            Parameter = parameter;
        }

        public async Task<T> GetSummary<T>(DateTime date_from, DateTime date_to, string payroll_code, string site)
        {
            Parameter.PostData.payroll_code = payroll_code;
            Parameter.PostData.page = "-1";
            Parameter.PostData.date_from = date_from.ToString("yyyy-MM-dd");
            Parameter.PostData.date_to = date_to.ToString("yyyy-MM-dd");

            var dicc = new Dictionary<string, string>();
            dicc.Add("postData", JsonConvert.SerializeObject(Parameter.PostData));

            var content = new FormUrlEncodedContent(dicc);

            var responseMessage = await Client.PostAsync(string.Format("{0}", Parameter.Urls[site]), content);
            string responseMessageContent = await responseMessage.Content.ReadAsStringAsync();
            var responseDeserialized = JsonConvert.DeserializeObject<T>(responseMessageContent);

            return responseDeserialized;
        }

        public async Task<T> GetPageContent<T>(DateTime date_from, DateTime date_to, int page, string payroll_code, string site)
        {
            Parameter.PostData.payroll_code = payroll_code;
            Parameter.PostData.page = page.ToString();
            Parameter.PostData.date_from = date_from.ToString("yyyy-MM-dd");
            Parameter.PostData.date_to = date_to.ToString("yyyy-MM-dd");

            var dicc = new Dictionary<string, string>();
            dicc.Add("postData", JsonConvert.SerializeObject(Parameter.PostData));


            var res = await Client.PostAsync(string.Format("{0}", Parameter.Urls[site]), new FormUrlEncodedContent(dicc));
            var resdeserialized = JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());

            return resdeserialized;
        }

    }

}