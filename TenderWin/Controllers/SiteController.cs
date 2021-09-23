using System;
using System.Web.Mvc;
using TenderWin.Models;

namespace TenderWin.Controllers
{
    public class SiteController : Controller
    {
        //Отображение формы для ввобда номера тендера
        public ActionResult Index()
        {
            return View();
        }
        //Получение и отображение данных о тендере по его идентификатору
        [HttpPost]
        public ActionResult Index(int? tenderId)
        {
            try
            {
                if(tenderId == null)
                {
                    throw new ApplicationException("Введено некорректное значение");
                }
                Tender tender = ConnectToTenderWin.GetDataFromServer(tenderId.ToString());
                return View("Tender", tender);
            }
            //Возникло исключение - вывод формы ввода номера тендера с ошибкой
            catch(Exception e)
            {
                
                ViewBag.Error = e.Message;
                return View();
            }
            
        }

    }
}