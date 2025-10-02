using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.IO.Pipes;
using FromBytesToBytes;
using GameSample_Server.NetworkApiServer;

namespace GameSample_Server
{
    struct GameMove
    {
        public int x, y;
    }
    public partial class Form1 : Form
    {
        /*NetworkStream network_stream_1, network_stream_2;

        NamedPipeServerStream pipe_server_1;
        NamedPipeServerStream pipe_server_2;

        StreamReader pipe_reader;
        StreamWriter pipe_writer;

        TcpListener tcpListenter;
        TcpClient client;*/

        INetworkApiServer network_api_server;
        //TcpApiServer network_api_server = new TcpApiServer();

        byte[] buffer = new byte[255];

        //Игровое поле
        byte[] game_board;

        public Form1()
        {
            InitializeComponent();

            //Инициализация игрового поля
            game_board = new byte[9];

            //100 - пустая клетка
            //0 - "нолик"
            //1 - "крестик"

            //Заполнение поля пустыми клетками
            for (int i = 0; i < 9; i++)
                game_board[i] = 100;



            
        }

        public async void WaitForClients()
        {
            //byte[] game_turn = new byte[1];
            //Цикл ожидания подключения клиентов
            //while (true)
            //{
                //Ожидание подключения первого игрока
                /*client = await tcpListenter.AcceptTcpClientAsync();
                network_stream_1 = client.GetStream();*/
                await network_api_server.AcceptClient(0);

                write_log("Подключился первый игрок");

                //Ожидание подключения второго игрока
                /*client = await tcpListenter.AcceptTcpClientAsync();
                network_stream_2 = client.GetStream();*/
                await network_api_server.AcceptClient(1);

                write_log("Подключился второй игрок");

                //Отправка игровой очереди и роли 1му игроку
                buffer[0]= 0;
                //await network_stream_1.WriteAsync(buffer, 0, 1);
                //game_turn[0] = 0;
                await network_api_server.SendAsync(0, buffer, 1);

                write_log("Отправлена игровая роль 1му игроку");

                //Отправка игровой очереди и роли 2му игроку
                buffer[0] = 1;
                //await network_stream_2.WriteAsync(buffer, 0, 1);
                //game_turn[0] = 1;
                await network_api_server.SendAsync(1, buffer, 1);

                write_log("Отправлена игровая роль 2му игроку");
                write_log("Игра начата");

                //Ожидание ходов игроков
                WaitForMovesAsync();
            //}
        }


        public async void WaitForMovesAsync()
        {         

            //Главный цикл обработки сетевых сообщений
           while (true)
            {
                //Ожидание хода игрока 1
                //await network_stream_1.ReadAsync(buffer, 0, buffer.Length);
                //Console.WriteLine("Ожидание данных...");
                await network_api_server.ReceiveAsync(0,buffer);
                //Console.WriteLine("Данные приняты.");

                InvokeMove(MyEncoder.fromBytes<GameMove>(buffer), 0);

                //Отправка обновлённого состояния поля игроку 2
                //await network_stream_2.WriteAsync(game_board, 0, game_board.Length);
                await network_api_server.SendAsync(1, game_board, game_board.Length);

                //Ожидание хода игрока 2
                //await network_stream_2.ReadAsync(buffer, 0, buffer.Length);
                await network_api_server.ReceiveAsync(1, buffer);
                InvokeMove(MyEncoder.fromBytes<GameMove>(buffer), 1);

                //Отправка обновлённого состояния поля игроку 1
                //await network_stream_1.WriteAsync(game_board, 0, game_board.Length);
                await network_api_server.SendAsync(0,game_board,game_board.Length);
            }
        }

       
        void InvokeMove(GameMove move, byte figure)
        {
            write_log($"{figure} | {move.x} | {move.y}");
            if (game_board[move.x * 3 + move.y] == 100)
                game_board[move.x * 3 + move.y] = figure;

            byte ret = check_win();

            if (ret != 100)
                write_log($"Победил {ret}");
        }

        void write_log(string message)
        {
            lb_logs.Items.Add(message);
        }

        //Кнопка запуска TCP-сервера
        private void button1_Click(object sender, EventArgs e)
        {
            //Запуск TCP-сервера

            /*tcpListenter = new TcpListener(IPAddress.Any, 8888);
            tcpListenter.Start();*/

            network_api_server = new TcpApiServer();

            network_api_server.Start();

            lb_logs.Items.Add("TCP-сервер запущен " + Dns.GetHostName());

            //Ожидание подключений клиентов
            WaitForClients();
        }

        //Кнопка запуска Pipe-сервера
        private void button2_Click(object sender, EventArgs e)
        {
            network_api_server = new PipeApiServer();

            network_api_server.Start();

            lb_logs.Items.Add("Pipe-сервер запущен " + Dns.GetHostName());

            //Ожидание подключений клиентов
            WaitForClients();
        }

        //public async void WaitForPipeConnections()
        //{
        //    await pipe_server.WaitForConnectionAsync();
        //    write_log("Подключился первый игрок");
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            network_api_server = new UdpApiServer();

            network_api_server.Start();

            lb_logs.Items.Add("UDP-cервер запущен " + Dns.GetHostName());

            //Ожидание подключений клиентов
            WaitForClients();
        }

        byte check_win()
        {
            int i, j, k, counter;

            //Просмотр горизонтальных линий
            for (i = 0; i < 3; i++) {
                counter = 0;
                for (j = 1; j < 3; j++) {
                    if (
                        game_board[i * 3 + j]!=100 &&
                        game_board[i * 3 + j] == game_board[i * 3 + (j - 1)]
                    ) 
                    counter++;
                }
                
                j--;

                if (counter == 2) return game_board[i * 3 + j];
            }

            //Просмотр вертикальных линий
            for (j = 0; j < 3; j++)
            {
                counter = 0;
                for (i = 1; i < 3; i++)
                {
                    if (game_board[i * 3 + j] != 100 && game_board[i * 3 + j] == game_board[(i - 1) * 3 + j]) counter++;
                }

                i--;                

                if (counter == 2) return game_board[i * 3 + j];
            }

            //Просмотр диагональных линий

            //просмотр первой диагонали
            counter = 0;
            for (k = 1; k < 3; k++)
                if (game_board[k * 3 + k] != 100 && game_board[k * 3 + k] == game_board[(k - 1) * 3 + (k - 1)]) counter++;

            k--;
            if (counter == 2) return game_board[k * 3 + k];

            //просмотр второй диагонали
            counter = 0;
            for (k = 1; k < 3; k++)
                if (game_board[k * 3 + (2 - k)] != 100 && game_board[k * 3 + (2-k)] == game_board[(k - 1) * 3 + (2 - k - 1)]) counter++;

            k--;
            if (counter == 2) return game_board[k * 3 + k];

            return 100;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            network_api_server = new MailslotApiServer();

            try
            {
                network_api_server.Start();

                lb_logs.Items.Add("Mailslot-cервер запущен " + Dns.GetHostName());
            }
            catch (Exception ex) {
                lb_logs.Items.Add(ex.Message);
            }

            //Ожидание подключений клиентов
            WaitForClients();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            network_api_server = new InternetApiServer(tb_global_server.Text);

            try
            {
                network_api_server.Start();

                lb_logs.Items.Add("Интернет-cервер запущен");
            }
            catch (Exception ex)
            {
                lb_logs.Items.Add(ex.Message);
            }

            //Ожидание подключений клиентов
            WaitForClients();
        }
    }
}
