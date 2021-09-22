using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenderWin.Models
{
    public class Tender
    {
        public int Id { get; set; }
        public string TradeStateName { get; set; }
        public string CustomerFullName { get; set; }
        public string TradeName { get; set; }
        public double InitialPrice { get; set; }
        public DateTime FillingApplicationEndDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Place { get; set; }
        public List<Document> Documents { get; set; }
        public List<Lot> Lots { get; set; }
    }
    //Ответ сервера
    public class Response
    {
        [JsonProperty("invdata")]
        public List<Tender> Tenders { get; set; }
    }
    public class Document
    {
        public string FileName { get; set; }
        public string Url { get; set; }
    }
    public class Lot
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Number { get; set; }
        public string Price { get; set; }
    }
}