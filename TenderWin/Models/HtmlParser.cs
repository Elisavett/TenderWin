using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenderWin.Models
{
    public class HtmlParser
    {
        public HtmlDocument html { get; set; }
        public HtmlParser(string htmlSting)
        {
            html = new HtmlDocument();
            html.LoadHtml(htmlSting);
        }
        public string getPlace()
        {
            return html.DocumentNode.QuerySelectorAll(".informationAboutCustomer__informationPurchase-infoBlock")
                .Where(div => div.QuerySelector(".infoBlock__label").InnerText == "Место поставки:")
                .SingleOrDefault()
                ?.QuerySelector("p").InnerText;
        }
        public List<string> getParamLotList(string paramName)
        {
            return html.DocumentNode.QuerySelectorAll(".informationAboutCustomer__resultBlock-outputResults .outputResults__oneResult p")
                .Where(p => p.QuerySelector("span")?.InnerText == paramName)
                .Select(el => el.LastChild.InnerText)
                .ToList();

        }
    }
}