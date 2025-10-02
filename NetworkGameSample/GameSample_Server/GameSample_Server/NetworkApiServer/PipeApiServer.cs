using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace GameSample_Server.NetworkApiServer
{
    class PipeApiServer : INetworkApiServer
    {
        NamedPipeServerStream[] server = new NamedPipeServerStream[2];
       
        public async Task AcceptClient(int client_id)
        {
            await server[client_id].WaitForConnectionAsync();
            
        }

        public Task ReceiveAsync(int client_id, byte[] data, int length = 0)
        {
            var l = (length == 0) ? data.Length : length;
            return server[client_id].ReadAsync(data,0,l);            
        }

        public Task SendAsync(int client_id, byte[] data, int length = 0)
        {
            var l = (length == 0) ? data.Length : length;
            return server[client_id].WriteAsync(data, 0, l);
        }

        public void Start()
        {
            server[0] = new NamedPipeServerStream("testpipe_1", PipeDirection.InOut);
            server[1] = new NamedPipeServerStream("testpipe_2", PipeDirection.InOut);
        }
    }
}
