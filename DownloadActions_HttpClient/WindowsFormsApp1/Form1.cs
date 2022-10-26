using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public double exchange_rate = 20.15;
        public Form1()
        {
            InitializeComponent();
        }

        public async void getData()
        {
            
            var currencyCRUD = new CurrencyCRUD();
            currencyCRUD.CheckDate();

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(@"https://quote.rbc.ru/v5/ajax/catalog/get-tickers?type=share&sort=blue_chips&limit=200&offset=0");
            response.EnsureSuccessStatusCode();
			
			//Получение списка акций в формате JSON с веб-сайта 
            string data = await response.Content.ReadAsStringAsync();

			//Преобразование данных из формата JSON в массив объектов языка C#
            var parsedData = JsonSerializer.Deserialize<List<Action>>(data);

            //currencyCRUD.DeleteAllStocks();
			//Если сегодня акции уже были сохранены в базу данных - завершение программы
            if (!currencyCRUD.CheckDate()) return;
			
			//Сохранение полученного списка акций в базе данных
            foreach (var item in parsedData)
            {
                this.dataGridView1.Rows.Add(item.company.title,item.price,item.currency, item.price * exchange_rate);
                currencyCRUD.AddStock(item.title, item.price, item.currency, item.company.title);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.getData();

        }
    }

	//Классы для представления на языке C# данных, скачанных с веб-сайта
    public class Company
    {
        public string title { get; set; } 
    }
    public class Action
    {
        public string title { get; set; } 
        public double price { get; set; } 
        public string currency { get; set; }
        public Company company { get; set; }
    }

	//Класс для управления хранением скачанных акций в локальной базе данных
    public class CurrencyCRUD
    {
        static string connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Lect20220926\WindowsFormsApp1\WindowsFormsApp1\MyDB.mdf;Integrated Security=True";
        SqlConnection sqlConnection; 
        public CurrencyCRUD()
        {
            sqlConnection = new SqlConnection(connection_string);
            sqlConnection.Open();
        }

		//Проверка, было ли сохранение акций в базу данных сегодня
        public bool CheckDate()
        {
            SqlCommand command = new SqlCommand(@"SELECT COUNT(*) FROM [Table] WHERE download_date=@download_date", sqlConnection);
            command.Parameters.AddWithValue("@download_date", DateTime.Today.Ticks); //637997929376000258
            var count = command.ExecuteScalar();

            return !((int)count > 0);
        }
		
		//Добавление акции в базу данных
        public void AddStock(string title, double price, string currency, string company_title)
        {            
            SqlCommand command = new SqlCommand(@"INSERT INTO [Table](title,price,currency,company_title,download_date) VALUES (@title,@price,@currency,@company_title,@download_date)", sqlConnection);
            command.Parameters.AddWithValue("@title",title);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@currency", currency);
            command.Parameters.AddWithValue("@company_title", company_title);
            command.Parameters.AddWithValue("@download_date", DateTime.Today.Ticks);
            command.ExecuteNonQuery();
        }

		//Удаление из базы данных всех акций
        public void DeleteAllStocks()
        {
            SqlCommand command = new SqlCommand(@"DELETE FROM [Table]", sqlConnection);
            command.ExecuteNonQuery();
        }
    }
}
