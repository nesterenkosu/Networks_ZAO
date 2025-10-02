using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GameSample.NetworkApi
{
    class TcpApi : INetworkApi
    {
        TcpClient client;
        NetworkStream network_stream;

        public void Connect(string address, int client_id=0)
        {
            client = new TcpClient();
            client.Connect(IPAddress.Parse(address), 8888);
            network_stream = client.GetStream();
        }

        public async Task ReceiveAsync(byte[] data)
        {
            await network_stream.ReadAsync(data, 0, data.Length);
        }

        public async Task SendAsync(byte[] data)
        {
            await network_stream.WriteAsync(data, 0, data.Length);
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}
