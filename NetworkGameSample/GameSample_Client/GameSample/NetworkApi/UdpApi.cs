using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GameSample.NetworkApi
{
    class UdpApi : INetworkApi
    {
        UdpClient client;
        IPEndPoint server_addr;

        public void Connect(string address, int client_id = 0)
        {
            server_addr = new IPEndPoint(IPAddress.Parse(address),8888+client_id-1);
            client = new UdpClient(8888 + client_id+10);

            byte[] data = new byte[1];
            data[0] = 1;
            client.SendAsync(data, data.Length, server_addr);

        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(byte[] data)
        {
            UdpReceiveResult result;
            result = await client.ReceiveAsync();
            result.Buffer.CopyTo(data,0);
        }

        public async Task SendAsync(byte[] data)
        {
            await client.SendAsync(data, data.Length, server_addr);
        }
    }
}
