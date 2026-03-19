using Avalonia.Threading;
using HardwareMonitor.Models;
using RpiControlCenter.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HardwareMonitor.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly HardwareDataListener _listener;

        private HardwareData _hardwareData = new HardwareData();
        public HardwareData HardwareData
        {
            get => _hardwareData;
            private set
            {
                _hardwareData = value;
                OnPropertyChanged(nameof(HardwareData));
            }
        }

        public MainWindowViewModel()
        {
            _listener = new HardwareDataListener();
            _listener.OnDataReceived += OnDataReceived;

            // Start MQTT listener
            _ = StartListenerAsync();
        }

        private void OnDataReceived(HardwareData data)
        {
            Dispatcher.UIThread.Post(() =>
            {
                // Update the existing HardwareData object property by property
                HardwareData.CpuUsage = data.CpuUsage;
                HardwareData.CpuFreq = data.CpuFreq;
                HardwareData.RamUsage = data.RamUsage;
                HardwareData.RamTotal = data.RamTotal;
                HardwareData.GpuTemp = data.GpuTemp;
                HardwareData.GpuUsage = data.GpuUsage;
                HardwareData.GpuTotal = data.GpuTotal;
                HardwareData.GpuName = data.GpuName;
            });
        }

        private async Task StartListenerAsync()
        {
            try
            {
                await _listener.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MQTT listener error: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
