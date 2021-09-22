using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TenderWin.Models;
using Fizzler.Systems.HtmlAgilityPack;

namespace TenderWin.Controllers
{
    public class SiteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(int tenderId)
        {
            Tender tender = ConnectToTenderWin.GetDataFromServer(tenderId.ToString());
            return View();
        }

    }
}