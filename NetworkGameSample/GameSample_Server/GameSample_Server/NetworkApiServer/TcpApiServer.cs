using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GameSample_Server.NetworkApiServer
{
    class TcpApiServer : INetworkApiServer
    {
        TcpListener tcpListenter;
        TcpClient[] client = new TcpClient[2];
        NetworkStream[] network_streams = new NetworkStream[2];

        public void Start()
        {
            //Запуск TCP-сервера
            tcpListenter = new TcpListener(IPAddress.Any, 8888);
            tcpListenter.Start();
        }

        public async Task AcceptClient(int client_id)
        {
            client[client_id] = await tcpListenter.AcceptTcpClientAsync();
            network_streams[client_id] = client[client_id].GetStream();
        }

        public async Task ReceiveAsync(int client_id, byte[] data, int length = 0)
        {
            int l = (length == 0) ? data.Length : length;
            await network_streams[client_id].ReadAsync(data, 0, l);
        }

        public async Task SendAsync(int client_id, byte[] data, int length = 0)
        {
            int l = (length == 0) ? data.Length : length;            
            await network_streams[client_id].WriteAsync(data, 0, l);
        }
    }
}
