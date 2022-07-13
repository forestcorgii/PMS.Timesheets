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
        public static TimeDownloaderAdapter CreateAdapter(IConfigurationRoot config)
        {
            var section = config.GetRequiredSection("TimeDownloaderAPI");

            Dictionary<string, string> Urls = new();
            Urls.Add("MANILA", section.GetValue<string>("Url"));
            Urls.Add("LEYTE", section.GetValue<string>("Url_Leyte"));
            TimeDownloaderParameter parameter = new()
            {
                PostData = new()
                {
                    info = section.GetValue<string>("Info"),
                    api_token = section.GetValue<string>("APIToken"),
                },
                Urls = Urls,
            };

            return new TimeDownloaderAdapter(parameter);
        }
    }
}
