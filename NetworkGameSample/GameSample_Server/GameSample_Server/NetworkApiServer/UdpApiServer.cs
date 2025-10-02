using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GameSample_Server.NetworkApiServer
{
    class UdpApiServer : INetworkApiServer
    {
        UdpClient[] server = new UdpClient[2];
        IPEndPoint[] client_addr = new IPEndPoint[2];

        public async Task AcceptClient(int client_id)
        {
            //return Task.CompletedTask;

            //throw new NotImplementedException();

            UdpReceiveResult result;
            result = await server[client_id].ReceiveAsync();            
            client_addr[client_id] = result.RemoteEndPoint;
            
        }

        public async Task ReceiveAsync(int client_id, byte[] data, int length = 0)
        {
            UdpReceiveResult result;
            result = await server[client_id].ReceiveAsync();
            result.Buffer.CopyTo(data,0);
           
        }

        public async Task SendAsync(int client_id, byte[] data, int length = 0)
        {
            await server[client_id].SendAsync(data, data.Length, client_addr[client_id]);
        }

        public void Start()
        {
            server[0] = new UdpClient(8888);
            server[1] = new UdpClient(8889);
        }
    }
}
