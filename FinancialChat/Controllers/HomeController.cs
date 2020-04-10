using FinancialChat.Helper;
using FinancialChat.Models.Bot;
using Microsoft.AspNet.Identity;
using Microsoft.VisualBasic.FileIO;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace FinancialChat.Controllers
{
    public class HomeController : Controller
    {
        private IBot _bot { get; }

        public HomeController()
        {
            _bot = new Bot();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public JsonResult sendmsg(string message)
        {
            RabbitMQOperations rabbit = new RabbitMQOperations();
            IConnection con = rabbit.GetConnection();
            var currentUser = User.Identity.GetUserName();
            string quote = _bot.GetQuote(message);
            if (quote != null)
            {
                message = "<b>Bot</b>: " + quote;
            }
            else
            {
                message = "<b>" + currentUser + "</b>: " + message;
            }
            
            bool flag = rabbit.send(con, message);
            return Json("Sent");
        }

        [Authorize]
        [HttpPost]
        public JsonResult receive()
        {
            try
            {
                RabbitMQOperations rabbit = new RabbitMQOperations();
                IConnection con = rabbit.GetConnection();
                var userqueue = User.Identity.GetUserName();
                string message = rabbit.receive(con, userqueue);
                return Json(message);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}