using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace FromScratch
{
    //Объявление прокси-интерфейса, через методы которого
    //будут вызываться методы REST API социальной сети ВКонтакте
    [ServiceContract]
    [DataContractFormat]
    public interface VK_API
    {
        [WebGet(UriTemplate = "method/wall.post?owner_id={owner_id}&message={message}&access_token={access_token}&v=5.81", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        VK_Response WallPost(string owner_id,string message,string access_token);
    }

    //Контракты данных для представления значений, возвращённых REST API
    [DataContract]
    public class VK_Response
    {
        //Ответ об успешном выполнении метода
        [DataMember]
        public VK_Response_inner response;

        //Ответ об ошибке выполнения метода
        [DataMember]
        public VK_Error_inner error;
    }

    [DataContract]
    public class VK_Response_inner
    {
        [DataMember]
        public string post_id;
    }

    [DataContract]
    public class VK_Error_inner
    {
        [DataMember]
        public string error_code;
        [DataMember]
        public string error_msg;
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Создание прокси-объекта на основе прокси-интерфейса
            ChannelFactory<VK_API> vk_api = new ChannelFactory<VK_API>("VK_ENDPOINT");
            var proxy = vk_api.CreateChannel();
          
            //Отправка сообщения на стену социальной сети ВКонтакте
			//при помощи метода WallPost.
			//Руководство по получению id страницы, а также токена
			//см. в файле Readme.pdf, нахоящемся в папке с данным проектом
            VK_Response response = proxy.WallPost(
                "укажите_здесь_id_страницы", 
                "укажите_здесь_сообщение_для_отправки", 
                "укажите_здесь_токен");

            //Обработка статуса ответа метода WallPost
            if (response.error != null)
                Console.WriteLine("Ошибка ["+response.error.error_msg+"]");
            else
                Console.WriteLine("Успех post_id=["+ response.response.post_id+"]");

            Console.ReadLine();
               
            ((IDisposable)vk_api).Dispose();
        }
    }
}
