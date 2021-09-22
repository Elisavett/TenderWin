using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace TenderWin.Models
{
    //Класс для соединения с сервером
    public class ConnectToTenderWin
    {
        public static Tender GetDataFromServer(string id)
        {
            Tender tender = GetTender(id);
            tender.Lots = GetNotificationPage(id, out string place);
            tender.Place = place;
            tender.Documents = GetDocumentList(id);
            return tender;
        }
        //Получение списка документации
        private static List<Document> GetDocumentList(string id)
        {
            var responseString3 = Request(" https://api.market.mosreg.ru/api/Trade/" + id + "/GetTradeDocuments", "GET");
            return JsonConvert.DeserializeObject<List<Document>>(responseString3);
        }
        //Получение страницы извещения
        private static List<Lot> GetNotificationPage(string id, out string place)
        {
            var responseString2 = Request("https://market.mosreg.ru/Trade/ViewTrade/" + id, "GET");
            HtmlParser htmlParser = new HtmlParser(responseString2);
            place = htmlParser.getPlace();
            List<string> names = htmlParser.getParamLotList("Наименование товара, работ, услуг:");
            List<string> units = htmlParser.getParamLotList("Единицы измерения:");
            List<string> numbers = htmlParser.getParamLotList("Количество:");
            List<string> prices = htmlParser.getParamLotList("Стоимость единицы продукции ( в т.ч. НДС при наличии):");

            List<Lot> lots = new List<Lot>();
            for (int i = 0; i < names.Count; i++)
            {
                lots.Add(new Lot
                {
                    Name = names[i],
                    Number = numbers[i],
                    Unit = units[i],
                    Price = prices[i]
                });
            }
            return lots;
        }
        //Получение базового набора полей по тендеру
        private static Tender GetTender(string id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"page", "1"},
                {"itemsPerPage", "10"},
                {"Id", id}
            };
            var responseString = Request("https://api.market.mosreg.ru/api/Trade/GetTradesForParticipantOrAnonymous", "POST", parameters);
            return JsonConvert.DeserializeObject<Response>(responseString).Tenders[0];
        }
        //Запрос на сервер с необязательным аргументом параметры
        private static string Request(string Url, string method, Dictionary<string,string> parms = null)
        {
            //Создание запроса
            WebRequest req = WebRequest.Create(Url);
            req.Method = method;
            if (parms != null)
            {
                //Массив битов для передачи данных
                byte[] byteArray = ParamsToByte(parms);
                req.ContentType = "application/x-www-form-urlencoded";
                // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
                req.ContentLength = byteArray.Length;
                //записываем данные в поток запроса
                using (Stream dataStream = req.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            //Ответ с сервера
            using (WebResponse resp = req.GetResponse())
            {
                using (Stream stream = resp.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
        //Преобразование параметров из типа словарь в массив байтов
        private static byte[] ParamsToByte(Dictionary<string, string> parms)
        {
            //Построение строки
            StringBuilder data = new StringBuilder();
            var pair = parms.ElementAt(0);
            //Парамтры html пара ключ=значение, разделяются символом &
            data.Append("&").Append(pair.Key).Append("=").Append(pair.Value);
            for (int i = 1; i < parms.Count; i++)
            {
                pair = parms.ElementAt(i);
                data.Append("&").Append(pair.Key).Append("=").Append(pair.Value);
            }
            // преобразуем данные в массив байтов
            return Encoding.UTF8.GetBytes(data.ToString());
        }
    }
}