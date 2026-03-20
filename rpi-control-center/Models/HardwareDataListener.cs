using MQTTnet;
using MQTTnet.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HardwareMonitor.Models
{
    public class HardwareDataListener
    {
        private MqttFactory _mqttFactory;
        private IMqttClient _mqttClient;
        private MqttClientOptions _options;

        public event Action<HardwareData>? OnDataReceived;
        public HardwareDataListener()
        {
            _mqttFactory = new MqttFactory();
            _mqttClient = _mqttFactory.CreateMqttClient();

            _options = new MqttClientOptionsBuilder()
                .WithClientId("pi-client")
                .WithTcpServer("localhost", 1883)
                .Build();
        }

        public async Task StartAsync(string broker = "localhost", UInt16 port = 1883)
        {
            _mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                var payloadSegment = e.ApplicationMessage.PayloadSegment;
                string payload;

                if (payloadSegment.Array != null)
                {
                    payload = Encoding.UTF8.GetString(
                        payloadSegment.Array,
                        payloadSegment.Offset,
                        payloadSegment.Count);

                    Console.WriteLine($"Received message on topic {e.ApplicationMessage.Topic}: {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment.Array)}");
                }

                else
                {
                    payload = string.Empty;
                }

                try
                {
                    var data = JsonSerializer.Deserialize<HardwareData>(
                        payload);

                    if (data != null)
                    {
                        OnDataReceived?.Invoke(data);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"JSON error: {ex.Message}");
                }

                return Task.CompletedTask;
            };

            _mqttClient.ConnectedAsync += async e =>
            {
                Console.WriteLine("Connected to broker");

                await _mqttClient.SubscribeAsync("hw-data/topic");
                Console.WriteLine("Subscribed to hw-data/topic");
            };

            _mqttClient.DisconnectedAsync += async e =>
            {
                Console.WriteLine("Disconnected from broker");

                await Task.Delay(2000);

                try
                {
                    await _mqttClient.ConnectAsync(_options);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Reconnect failed: {ex.Message}");
                }
            };

            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ConnectAsync(_options);
            }

            Console.WriteLine("Subscribed to hw-data/topic");
        }

        public async Task StopAsync()
        {
            if (_mqttClient != null && _mqttClient.IsConnected)
            {
                await _mqttClient.DisconnectAsync();
            }
        }
    }
}
