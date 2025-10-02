using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSample_Server.NetworkApiServer
{
    interface INetworkApiServer
    {
        void Start();
        Task AcceptClient(int client_id);
        Task SendAsync(int client_id, byte[] data, int length = 0);
        Task ReceiveAsync(int client_id, byte[] data, int length = 0);
    }
}
