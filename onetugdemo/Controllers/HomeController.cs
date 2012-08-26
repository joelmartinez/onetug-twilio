using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using onetugdemo.Models;

namespace onetugdemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Random()
        {
            CallOptions call = new CallOptions
            {
                From = Demo.GetRandom().Number,
                To = Demo.GetRandom().Number
            };

            var twilio = new TwilioRestClient(Constants.SID, Constants.AuthToken);
            twilio.InitiateOutboundCall(call);
            return View();
        }

        public ActionResult broadcast(string message)
        {
            try
            {
                var numbers = Demo.GetAll();

                foreach (var num in numbers)
                {
                    var twilio = new TwilioRestClient(Constants.SID, Constants.AuthToken);
                    var msg = twilio.SendSmsMessage("+14072199940", num.Number, message);
                }

                return Json(new { success = true, users = numbers }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, reason = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
