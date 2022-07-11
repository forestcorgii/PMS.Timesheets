using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.TimeSystem.Adapter
{
    public class TimeDownloaderParameter
    {
        public Dictionary<string, string> Urls;

        public string Info;
        public string APIToken;
        public PostDataClass PostData;


        public class PostDataClass
        {
            public string info;
            public string api_token;
            public string date_from;
            public string date_to;
            public string page;
            public string payroll_code;
        }
    }
}
