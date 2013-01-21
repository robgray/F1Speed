using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed
{
    public class WebSpeedModule : Nancy.NancyModule
    {
        public WebSpeedModule()
        {           
            dynamic model = new 
            {   
                Title = "F1 Speed Web Viewer"
            };
            Get["/"] = x => View["dash.sshtml", model];
            
        }
    }
}