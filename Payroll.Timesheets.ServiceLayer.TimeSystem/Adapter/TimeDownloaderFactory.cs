using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.TimeSystem.Adapter
{
    public class TimeDownloaderFactory
    {
        public TimeDownloaderAdapter CreateAdapter(IConfigurationRoot config)
        {
            var section = config.GetRequiredSection("HRMSAPI");

            Dictionary<string, string> Urls = new();
            Urls.Add("MANILA", section.GetValue<string>("Url"));
            Urls.Add("LEYTE", section.GetValue<string>("Url_Leyte"));
            TimeDownloaderParameter parameter = new()
            {
                Info = section.GetValue<string>("Info"),
                APIToken = section.GetValue<string>("APIToken"),
                Urls = Urls
            };


            return new TimeDownloaderAdapter(parameter);
        }
    }
}
