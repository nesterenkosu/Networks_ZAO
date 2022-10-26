using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CookComputing.XmlRpc;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WindowsFormsApp1
{
    //----VK----
    [ServiceContract]
    [DataContractFormat]
    public interface VK_API
    {
        [WebGet(
            UriTemplate="method/wall.post?owner_id={owner_id}&message={message}&access_token={access_token}&v=5.81",
            BodyStyle=WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json
        )]
        VK_Response WallPost(string owner_id, string message, string access_token);
    }

    [DataContract]
    public class VK_Response
    {
        [DataMember]
        public VK_Response_inner response;

        [DataMember]
        public VK_Error error;
    }

    [DataContract]
    public class VK_Response_inner
    {
        [DataMember]
        public int post_id;
    }

    [DataContract]
    public class VK_Error
    {
        [DataMember]
        public int error_code;
        [DataMember]
        public string error_msg;
    }

        //----------
        public struct LJPostEventArg
    {
        public string username;
        public string password;
        [XmlRpcMember("event")]
        public string ljevent;
        public string lineendings;
        public string subject;
        public int year;
        public int mon;
        public int day;
        public int hour;
        public int min;
    }
    public struct LJRetVal
    {
        public int itemid;
        public int anum;
        public string url;
    }

    [XmlRpcUrl("http://www.livejournal.com/interface/xmlrpc")]
    public interface ILJService
    {
        [XmlRpcMethod("LJ.XMLRPC.postevent")]
        LJRetVal postevent(LJPostEventArg posteventarg);
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Проверка правописания с помощью веб-сервиса YandexSpeller
            MyYandexSpeller.SpellService ys = new MyYandexSpeller.SpellService();
            MyYandexSpeller.SpellError[] errors = ys.checkText(textBox2.Text, "ru", 0, "");

            //если есть ошибки правописания
            if (errors.Length > 0)
            {
                //вывод информации об ошибках и возможных вариантов исправления
                string message = ""; string variants = "";
                foreach(MyYandexSpeller.SpellError error in errors)
                {
                    foreach (string variant in error.s)
                        variants += variant + "\n";
                    message += "Слово [" + error.word + "] содержит ошибки\nВозможные варианты:\n" + variants;
                }
                MessageBox.Show(message);

                //предотвращение публикации записи с ошибками в правописании
                return;
            }

            //Публикация в социальной сети "Живой журнал" (www.livejournal.com)
            ILJService service = XmlRpcProxyGen.Create<ILJService>();

            LJPostEventArg arg = new LJPostEventArg();

            arg.day = DateTime.Now.Day;
            arg.hour = DateTime.Now.Hour;
            arg.min = DateTime.Now.Minute;
            arg.mon = DateTime.Now.Month;
            arg.year = DateTime.Now.Year;

            arg.username = "inf_study";
            arg.password = "do520xLpim4";
            arg.lineendings = "pc";

            arg.subject = textBox1.Text;
            arg.ljevent = textBox2.Text;

            try
            {
               LJRetVal rv= service.postevent(arg);
                MessageBox.Show("Успешно опубликовано в ЖЖ ["+rv.url+"]");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Публикация в социальной сети ВКонтакте (vk.com)
            ChannelFactory<VK_API> vk_api = new ChannelFactory<VK_API>("VK_ENDPOINT");
            var proxy = vk_api.CreateChannel();

			//Руководство по получению id страницы, а также токена
			//см. в файле Readme.pdf, нахоящемся в папке с данным проектом
            VK_Response resp = proxy.WallPost(
                "укажите_здесь_id_страницы", 
                textBox2.Text,
                "укажите_здесь_токен"
            );

            if(resp.error == null)
            {
                MessageBox.Show("Успешно опубликовано в ВК [" + resp.response.post_id + "]");
            }
            else
            {
                MessageBox.Show("Ошибка код=["+ resp.error.error_code.ToString()+ "] сообщение=["+ resp.error.error_msg+ "]");
            }
        }
    }
}
