using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GameSample_Server.NetworkApiServer
{
    class MailslotApiServer : INetworkApiServer
    {
        const string MailSlotServer1_Name = @"\\.\mailslot\MyMailSlotServer1";
        const string MailSlotServer2_Name = @"\\.\mailslot\MyMailSlotServer2";
        const string MailSlotClient1_Name = @"\\.\mailslot\MyMailSlotClient1";
        const string MailSlotClient2_Name = @"\\.\mailslot\MyMailSlotClient2";

        //Создание нового мэйлслота
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateMailslot(string lpName, uint nMaxMessageSize, uint nMaxInstances, IntPtr lpSecurityAttributes);
        //Подключение к существующему мэйлслоту
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, IntPtr lpOverlapped);


        IntPtr[] mailslot_server = new IntPtr[2];
        IntPtr[] mailslot_client = new IntPtr[2];

        public async Task AcceptClient(int client_id)
        {
            //Принятие условного "запроса на подключение" от клиента
            byte[] data = new byte[255];
            await ReceiveAsync(client_id,data);

            //Подключение к мэйлслоту этого клиента
            await Task.Run(() =>
            {
                string MailSlotClient_Name = @"\\.\mailslot\MyMailSlotClient" + (client_id+1).ToString();
                //Console.WriteLine(MailSlotClient_Name);
                //MessageBox.Show(MailSlotClient_Name);

                mailslot_client[client_id] = CreateFile(MailSlotClient_Name, 0x40000000, 0, IntPtr.Zero, 3, 0, IntPtr.Zero);
            });
        }

        public Task ReceiveAsync(int client_id, byte[] data, int length = 0)
        {
            byte[] buffer = new byte[512];
            uint bytesRead;

            // Асинхронное чтение из мэйлслота
            return Task.Run(() =>
            {
                while (!ReadFile(mailslot_server[client_id], data, (uint)buffer.Length, out bytesRead, IntPtr.Zero))
                    System.Threading.Thread.Sleep(100);
                
            });
        }

        public Task SendAsync(int client_id, byte[] data, int length = 0)
        {
            
            uint bytesWritten;

            // Асинхронная отправка сообщения
            return Task.Run(() =>
            {
                WriteFile(mailslot_client[client_id], data, (uint)data.Length, out bytesWritten, IntPtr.Zero);
                
               
            });
        }

        public void Start()
        {
            mailslot_server[0] = CreateMailslot(MailSlotServer1_Name, 0, 0, IntPtr.Zero);
            if (mailslot_server[0] == IntPtr.Zero)
            {
                throw new Exception("Ошибка создания 1-го серверного мэйлслота.");                
            }

            mailslot_server[1] = CreateMailslot(MailSlotServer2_Name, 0, 0, IntPtr.Zero);
            if (mailslot_server[1] == IntPtr.Zero)
            {
                throw new Exception("Ошибка создания 2-го серверного мэйлслота.");                
            }            
        }
    }
}
