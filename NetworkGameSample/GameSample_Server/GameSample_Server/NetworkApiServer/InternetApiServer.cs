using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace GameSample_Server.NetworkApiServer
{
    class InternetApiServer : INetworkApiServer
    {
        private string global_url;
        private string session_id;
        public Task AcceptClient(int client_id)
        {            
            byte[] data = new byte[1];
            return ReceiveAsync(client_id, data);
        }

        public async Task ReceiveAsync(int client_id, byte[] data, int length = 0)
        {
            int l = (length == 0) ? data.Length : length;
           // Convert.ToBase64String(data, 0, l);

            //Подключение к глобальному серверу и принятие его ответа
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            string response_as_str;
            byte[] response_as_bytes;
            do
            {
                response = await client.GetAsync($@"{global_url}?program_id={client_id}&receive&server");
                response.EnsureSuccessStatusCode();
                //Получение принятых данных в виде текстовой строки
                response_as_bytes = await response.Content.ReadAsByteArrayAsync();
                response_as_str = Encoding.UTF8.GetString(response_as_bytes);

                Thread.Sleep(100);
                
            } while (response_as_str.Trim() == "");

            //преобразование принятых данных из base64 в поток байт
            //data = Convert.FromBase64String(response_as_str);

            byte[] temp = Convert.FromBase64String(response_as_str);
            Array.Copy(temp, data, temp.Length);

        }

        public async Task SendAsync(int client_id, byte[] data, int length = 0)
        {
            int l = (length == 0) ? data.Length : length;           

            HttpClient client = new HttpClient();

            StringContent content = new StringContent(Convert.ToBase64String(data, 0, l));

            Console.Write("Отправлено byte: ");
            Console.WriteLine(data[0]);
            Console.Write("Отправлено string: ");
            Console.WriteLine(Convert.ToBase64String(data, 0, l));

            // определяем данные запроса
            var request = new HttpRequestMessage(HttpMethod.Post, $@"{global_url}?program_id={client_id}&send");
            
            // установка отправляемого содержимого
            request.Content = content;
            
            // отправляем запрос
            var response = await client.SendAsync(request);

            // получаем ответ
            /*string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);*/
        }

        public void Start()
        {
            //throw new NotImplementedException();
        }

        public InternetApiServer(string global_url)
        {
            this.global_url = global_url;
        }
    }
}
