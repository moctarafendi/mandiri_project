using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace mandiri_project.Settings
{
    public class AppLaunchSettings
    {
        public const string ConfigName = "AppLaunch";

        public string ApplicationUrl { get; set; }

    }    
}
