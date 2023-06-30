using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenderWin.Models
{
    //Находит необходиме данные в html-файле
    public class HtmlParser
    {
        public HtmlDocument Html { get; set; }
        public HtmlParser(string htmlSting)
        {
            Html = new HtmlDocument();
            Html.LoadHtml(htmlSting);
        }
        //Получение места поставки
        public string GetPlace()
        {
            //Вырираем текст элемента параграфа (p) по классу родительсого элемента "informationAboutCustomer__informationPurchase-infoBlock"
            //и тексту смежных элементов label с классом "infoBlock__label"
            return Html.DocumentNode.QuerySelectorAll(".informationAboutCustomer__informationPurchase-infoBlock")
                .Where(div => div.QuerySelector(".infoBlock__label").InnerText == "Место поставки:")
                .SingleOrDefault()
                ?.QuerySelector("p").InnerText;
        }
        //Получение списка данных о лотах по обозначению категории данных

        public List<string> GetParamLotList(string paramName)
        {
            //Вырираем текст каждого последнего дочернего элемента по классу родительских элементов
            //и тексту смежных элементов span
            return Html.DocumentNode.QuerySelectorAll(".informationAboutCustomer__resultBlock-outputResults .outputResults__oneResult p")
                .Where(p => p.QuerySelector("span")?.InnerText == paramName)
                .Select(el => el.LastChild.InnerText)
                .ToList();

        }
    }
}