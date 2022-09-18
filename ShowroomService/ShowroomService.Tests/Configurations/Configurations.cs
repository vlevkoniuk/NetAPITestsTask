using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ShowroomService.Test.Configurations
{
    public class Configuration
    {
        public string ApiUrl { get; set; }
    }

    public static class Configurations
    {
        private static Configuration _config;

        public static Configuration Config
        {
            get
            {
                if (_config == null)
                    _config = ReadConfiguration();
                return _config;
            }
        }

        private static Configuration ReadConfiguration()
        {
            string path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).Parent.Parent.FullName;
            string text = File.ReadAllText(path + "\\Configurations\\Configurations.json");
            Configuration config = JsonConvert.DeserializeObject<Configuration>(text);
            return config;
        }
    }
}
