using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenderWin.Models
{
    public class Tender
    {
        [Display(Name = "Номер тендера")]
        public int Id { get; set; }

        [Display(Name = "Текущий статус")]
        public string TradeStateName { get; set; }

        [Display(Name = "Наименование заказчика")]
        public string CustomerFullName { get; set; }

        [Display(Name = "Наименование тендера")]
        public string TradeName { get; set; }

        [Display(Name = "НМЦ (начальная максимальная цена)")]
        public double InitialPrice { get; set; }

        private DateTime fillingApplicationEndDate;

        [Display(Name = "Дата окончания подачи заявок")]
        public DateTime FillingApplicationEndDate 
        {
            get
            {
                return fillingApplicationEndDate;
            }

            set
            {
                //Добавление 7 часов (UTF+7)
                fillingApplicationEndDate = value.AddHours(7);
            }
        }

        private DateTime publicationDate;

        [Display(Name = "Дата публикации")]
        public DateTime PublicationDate 
        {
            get
            {
                return publicationDate;
            }

            set
            {
                //Добавление 7 часов (UTF+7)
                publicationDate = value.AddHours(7);
            }
        }

        [Display(Name = "Место поставки")]
        public string Place { get; set; }

        [Display(Name = "Список документов")]
        public List<Document> Documents { get; set; }

        [Display(Name = "Список позиций лота")]
        public List<Lot> Lots { get; set; }
    }
    public class Document
    {
        [Display(Name = "Название")]
        public string FileName { get; set; }
        public string Url { get; set; }
    }
    public class Lot
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = " Единица измерения")]
        public string Unit { get; set; }

        [Display(Name = "Количество")]
        public string Number { get; set; }

        [Display(Name = "Цена за единицу")]
        public string Price { get; set; }
    }
    //Ответ сервера
    public class Response
    {
        public int totalrecords { get; set; }
        [JsonProperty("invdata")]
        public List<Tender> Tenders { get; set; }
    }
}