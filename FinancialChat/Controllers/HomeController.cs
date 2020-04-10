using FinancialChat.Helper;
using FinancialChat.Models;
using Microsoft.AspNet.Identity;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FinancialChat.Controllers
{
    public class HomeController : Controller
    {
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
            message = "<b>" + currentUser + "</b>: " + message;
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