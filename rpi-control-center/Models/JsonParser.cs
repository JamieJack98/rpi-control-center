using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HardwareMonitor.Models
{
    internal class JsonParser
    {
        public JsonParser() { }
        
        public readonly ushort ram_usage;
        public readonly ushort ram_total;
        public readonly ushort gpu_usage;
        public readonly ushort gpu_total;
        public readonly ushort cpu_freq;
        public readonly ushort cpu_usage;

        private string _filePath = "C:\\Users\\jamie\\source\\repos\\hw_data.json";

        public ushort getRamUsage()
        {
            using (StreamReader r = new StreamReader(this._filePath))
            { 
                string json = r.ReadToEnd();
            }
            return 0;
        }

    }
}
