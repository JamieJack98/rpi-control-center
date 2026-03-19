using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RpiControlCenter.Models
{
    public class HardwareData : INotifyPropertyChanged
    {
        private double _cpuUsage;
        private double _cpuFreq;
        private double _ramUsage;
        private double _ramTotal;
        private int _gpuTemp;
        private double _gpuUsage;
        private double _gpuTotal;
        private string _gpuName;

        [JsonPropertyName("cpu_usage")]
        public double CpuUsage
        {
            get => _cpuUsage;
            set { _cpuUsage = value; OnPropertyChanged(nameof(CpuUsage)); }
        }

        [JsonPropertyName("cpu_freq")]
        public double CpuFreq
        {
            get => _cpuFreq;
            set { _cpuFreq = value; OnPropertyChanged(nameof(CpuFreq)); }
        }

        [JsonPropertyName("ram_usage")]
        public double RamUsage
        {
            get => _ramUsage;
            set { _ramUsage = value; OnPropertyChanged(nameof(RamUsage)); }
        }

        [JsonPropertyName("ram_total")]
        public double RamTotal
        {
            get => _ramTotal;
            set { _ramTotal = value; OnPropertyChanged(nameof(RamTotal)); }
        }

        [JsonPropertyName("gpu_temp")]
        public int GpuTemp
        {
            get => _gpuTemp;
            set { _gpuTemp = value; OnPropertyChanged(nameof(GpuTemp)); }
        }

        [JsonPropertyName("gpu_usage")]
        public double GpuUsage
        {
            get => _gpuUsage;
            set { _gpuUsage = value; OnPropertyChanged(nameof(GpuUsage)); }
        }

        [JsonPropertyName("gpu_total")]
        public double GpuTotal
        {
            get => _gpuTotal;
            set { _gpuTotal = value; OnPropertyChanged(nameof(GpuTotal)); }
        }

        [JsonPropertyName("gpu_name")]
        public string GpuName
        {
            get => _gpuName;
            set { _gpuName = value; OnPropertyChanged(nameof(GpuName)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
